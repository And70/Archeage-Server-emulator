﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Models.Game.AI.Static;
using AAEmu.Game.Models.Game.Transfers.Paths;
using AAEmu.Game.Models.Game.Units.Route;
using AAEmu.Game.Models.Game.World;
using AAEmu.Game.Utils;
using NLog;

namespace AAEmu.Game.Models.Game.NPChar
{
    public class NpcSpawner : Spawner<Npc>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private List<Npc> _spawned;
        private Npc _lastSpawn;
        private int _scheduledCount;
        private int _spawnCount;

        public uint Count { get; set; }

        public NpcSpawner()
        {
            _spawned = new List<Npc>();
            Count = 1;
        }

        public List<Npc> SpawnAll()
        {
            var list = new List<Npc>();
            for (var num = _scheduledCount; num < Count; num++)
            {
                var npc = Spawn(0);
                if (npc != null)
                {
                    list.Add(npc);
                }
            }

            return list;
        }

        public override Npc Spawn(uint objId)
        {
            var npc = NpcManager.Instance.Create(objId, UnitId);
            if (npc == null)
            {
                _log.Warn("Npc {0}, from spawn not exist at db", UnitId);
                return null;
            }
            
            npc.Spawner = this;
            npc.Position = Position.Clone();
            npc.Pos = new WorldPos(Helpers.ConvertLongX(Position.X), Helpers.ConvertLongY(Position.Y), Position.Z);
            npc.Rot = new Quaternion(Helpers.ConvertDirectionToRadian(Position.RotationX), Helpers.ConvertDirectionToRadian(Position.RotationY), Helpers.ConvertDirectionToRadian(Position.RotationZ), 1f);
            npc.Vel = new Vector3();
            npc.AngVel = new Vector3();
            
            if (npc.Position == null)
            {
                _log.Error("Can't spawn npc {1} from spawn {0}", Id, UnitId);
                return null;
            }

            // see NpcAi
            //if (npc.Template.AiFileId == AiFilesType.Roaming ||
            //    npc.Template.AiFileId == AiFilesType.BigMonsterRoaming ||
            //    npc.Template.AiFileId == AiFilesType.ArcherRoaming ||
            //    npc.Template.AiFileId == AiFilesType.WildBoarRoaming)
            //{
            //    npc.Patrol = new Roaming { Interrupt = true, Loop = true, Abandon = false };
            //    npc.IsInBattle = false;
            //    npc.Patrol.Pause(npc);
            //    npc.Patrol.LastPatrol = npc.Patrol;
            //}

            // использование путей из логов с помощью файла npc_paths.json
            //if (
            //    npc.TemplateId == 11999
            //    || npc.TemplateId == 8172
            //    || npc.TemplateId == 8176
            //    || npc.TemplateId == 3576
            //    || npc.TemplateId == 918
            //    || npc.TemplateId == 3626
            //    || npc.TemplateId == 7660
            //    || npc.TemplateId == 12143
            //    )
            {
                if (!npc.IsInPatrol)
                {
                    var path = new SimulationNpc(npc);
                    // организуем последовательность "Дорог" для следования "Гвардов"
                    var lnpp = new List<NpcsPathPoint>();
                    foreach (var np in NpcsPath.NpcsPaths.Where(np => np.ObjId == npc.ObjId))
                    {
                        lnpp.AddRange(np.Pos);
                        path.NpcsRoutes.TryAdd(npc.TemplateId, lnpp);
                        break;
                    }

                    var go = true;
                    if (path.NpcsRoutes.Count == 0)
                    {
                        go = false;
                    }
                    else
                    {
                        if (path.NpcsRoutes.Any(route => route.Value.Count < 2)) // TODO == 0
                        {
                            go = false;
                        }
                    }
                    //if (path.Routes2.Count != 0)
                    if (go)
                    {
                        path.LoadNpcPathFromNpcsRoutes(npc.TemplateId); // начнем с самого начала
                        //_log.Warn("TransfersPath #" + transfer.TemplateId);
                        //_log.Warn("First spawn myX=" + transfer.Position.X + " myY=" + transfer.Position.Y + " myZ=" + transfer.Position.Z + " rotZ=" + transfer.Rot.Z + " rotationZ=" + transfer.Position.RotationZ);
                        npc.IsInPatrol = true; // so as not to run the route a second time

                        //path.GoToPath(npc, true);
                        npc.SimulationNpc = path;
                        npc.SimulationNpc.FollowPath = true;
                    }
                    else
                    {
                        //_log.Warn("No path found for Npc: " + npc.TemplateId + " ...");
                    }
                }
            }


            npc.Spawn();
            _lastSpawn = npc;
            _spawned.Add(npc);
            _scheduledCount--;
            _spawnCount++;

            return npc;
        }

        public override void Despawn(Npc npc)
        {
            npc.Delete();
            if (npc.Respawn == DateTime.MinValue)
            {
                _spawned.Remove(npc);
                ObjectIdManager.Instance.ReleaseId(npc.ObjId);
                _spawnCount--;
            }

            if (_lastSpawn == null || _lastSpawn.ObjId == npc.ObjId)
            {
                _lastSpawn = _spawned.Count != 0 ? _spawned[_spawned.Count - 1] : null;
            }
        }

        public void DecreaseCount(Npc npc)
        {
            _spawnCount--;
            _spawned.Remove(npc);
            if (RespawnTime > 0 && (_spawnCount + _scheduledCount) < Count)
            {
                npc.Respawn = DateTime.Now.AddSeconds(RespawnTime);
                SpawnManager.Instance.AddRespawn(npc);
                _scheduledCount++;
            }

            npc.Despawn = DateTime.Now.AddSeconds(DespawnTime);
            SpawnManager.Instance.AddDespawn(npc);
        }
    }
}
