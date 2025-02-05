﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj.Static;
using AAEmu.Game.Models.Game.Items;
using AAEmu.Game.Models.Game.Items.Actions;
using AAEmu.Game.Models.Game.Mate;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Utils.DB;

using NLog;

namespace AAEmu.Game.Core.Managers
{
    public class MateManager : Singleton<MateManager>
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private Regex _nameRegex;

        private Dictionary<uint, NpcMountSkills> _slaveMountSkills;
        private Dictionary<uint, Mount> _activeMates; // ownerObjId, Mount

        public Mount GetActiveMate(uint ownerObjId)
        {
            return _activeMates.ContainsKey(ownerObjId) ? _activeMates[ownerObjId] : null;
        }

        public Mount GetActiveMateByTlId(uint tlId)
        {
            foreach (var mate in _activeMates.Values)
            {
                if (mate.TlId == tlId)
                {
                    return mate;
                }
            }

            return null;
        }

        public Mount GetActiveMateByMateObjId(uint mateObjId)
        {
            foreach (var mate in _activeMates.Values)
            {
                if (mate.ObjId == mateObjId)
                {
                    return mate;
                }
            }

            return null;
        }

        public Mount GetIsMounted(uint objId)
        {
            foreach (var mate in _activeMates.Values)
            {
                if (mate.Attached1 == objId || mate.Attached2 == objId)
                {
                    return mate;
                }
            }

            return null;
        }

        public void ChangeStateMate(GameConnection connection, uint tlId, byte newState)
        {
            var owner = connection.ActiveChar;
            var mateInfo = GetActiveMate(owner.ObjId);
            if (mateInfo?.TlId != tlId)
            {
                return;
            }

            mateInfo.UserState = newState; // TODO - Maybe verify range
            //owner.BroadcastPacket(new SCMateStatePacket(), );
        }

        public void ChangeTargetMate(GameConnection connection, uint tlId, uint objId)
        {
            var owner = connection.ActiveChar;
            var mateInfo = GetActiveMateByTlId(tlId);
            if (mateInfo == null)
            {
                return;
            }

            mateInfo.CurrentTarget = objId > 0 ? WorldManager.Instance.GetUnit(objId) : null;
            owner.BroadcastPacket(new SCTargetChangedPacket(mateInfo.ObjId, mateInfo.CurrentTarget?.ObjId ?? 0), true);

            _log.Debug("ChangeTargetMate. tlId: {0}, objId: {1}, targetObjId: {2}", mateInfo.TlId, mateInfo.ObjId, objId);
        }

        public Mount RenameMount(GameConnection connection, uint tlId, string newName)
        {
            var owner = connection.ActiveChar;
            if (string.IsNullOrWhiteSpace(newName) || newName.Length == 0 || !_nameRegex.IsMatch(newName))
            {
                return null;
            }

            var mateInfo = GetActiveMate(owner.ObjId);
            if (mateInfo == null || mateInfo.TlId != tlId)
            {
                return null;
            }

            mateInfo.Name = newName.FirstCharToUpper();
            owner.BroadcastPacket(new SCUnitNameChangedPacket(mateInfo.ObjId, newName), true);
            return mateInfo;
        }

        public void MountMate(GameConnection connection, uint tlId, AttachPoint ap, AttachUnitReason reason)
        {
            var character = connection.ActiveChar;
            var mateInfo = GetActiveMateByTlId(tlId);
            if (mateInfo == null)
            {
                return;
            }

            if (mateInfo.OwnerObjId != character.ObjId)
            {
                if (mateInfo.Attached2 > 0)
                {
                    return;
                }

                character.BroadcastPacket(new SCUnitAttachedPacket(character.ObjId, ap, reason, mateInfo.ObjId), true);
                mateInfo.Attached2 = character.ObjId;
                mateInfo.Reason2 = reason;
            }
            else
            {
                if (mateInfo.Attached1 > 0)
                {
                    return;
                }

                character.BroadcastPacket(new SCUnitAttachedPacket(character.ObjId, ap, reason, mateInfo.ObjId), true);
                mateInfo.Attached1 = character.ObjId;
                mateInfo.Reason1 = reason;
            }

            _log.Debug("MountMate. mountTlId: {0}, att1: {1}, att2 {2}, attachPoint{3}, reason: {4}", mateInfo.TlId, mateInfo.Attached1, mateInfo.Attached2, ap, reason);
        }

        public void UnMountMate(Character character, uint tlId, AttachPoint ap, AttachUnitReason reason)
        {
            var mateInfo = GetActiveMateByTlId(tlId);
            if (mateInfo == null)
            {
                return;
            }

            var unMounted = 0;
            Character targetObj = null;
            if (mateInfo.Attached1 == character.ObjId && ap == AttachPoint.Driver)
            {
                targetObj = character;
                mateInfo.Attached1 = 0;
                mateInfo.Reason1 = 0;
                unMounted = 1;
            }
            else if (ap == AttachPoint.Passenger0)
            {
                targetObj = WorldManager.Instance.GetCharacterByObjId(mateInfo.Attached2);
                mateInfo.Reason2 = 0;
                mateInfo.Attached2 = 0;
                unMounted = 2;
            }

            if ((unMounted != 1 && unMounted != 2) || targetObj == null)
            {
                return;
            }

            character.BroadcastPacket(new SCUnitDetachedPacket(targetObj.ObjId, reason), true);
            _log.Debug("UnMountMate. mountTlId: {0}, objId: {1}, att: {2}, attachPoint{3}, reason: {4}", mateInfo.TlId, targetObj.ObjId, unMounted, ap, reason);
        }

        public void AddActiveMateAndSpawn(Character owner, Mount mount, Item item)
        {
            if (_activeMates.ContainsKey(owner.ObjId))
            {
                owner.Mates.DespawnMate(_activeMates[owner.ObjId].TlId);
                return;
            }

            _activeMates.Add(owner.ObjId, mount);

            owner.SendPacket(new SCItemTaskSuccessPacket(ItemTaskType.UpdateSummonMateItem, new List<ItemTask> { new ItemUpdate(item) },
                new List<ulong>())); // TODO - maybe update details
            owner.SendPacket(new SCMateSpawnedPacket(mount));
            mount.Spawn();

            _log.Debug("Mount spawned. ownerObjId: {0}, tlId: {1}, mateObjId: {2}", owner.ObjId, mount.TlId, mount.ObjId);
        }

        public void RemoveActiveMateAndDespawn(Character owner, uint tlId)
        {
            if (!_activeMates.ContainsKey(owner.ObjId))
            {
                return;
            }

            var mateInfo = _activeMates[owner.ObjId];
            if (mateInfo.TlId != tlId)
            {
                return;
            }

            if (mateInfo.Attached1 > 0)
            {
                UnMountMate((Character)WorldManager.Instance.GetUnit(mateInfo.Attached1), tlId, AttachPoint.Driver, AttachUnitReason.MountMateLeft); // TODO reason unmount
            }

            if (mateInfo.Attached2 > 0)
            {
                UnMountMate((Character)WorldManager.Instance.GetUnit(mateInfo.Attached2), tlId, AttachPoint.Passenger0, AttachUnitReason.MountMateLeft); // TODO reason unmount
            }

            _activeMates[owner.ObjId].Delete();
            _activeMates.Remove(owner.ObjId);
            ObjectIdManager.Instance.ReleaseId(mateInfo.ObjId);
            TlIdManager.Instance.ReleaseId(mateInfo.TlId);

            _log.Debug("Mount removed. ownerObjId: {0}, tlId: {1}, mateObjId: {2}", owner.ObjId, mateInfo.TlId, mateInfo.ObjId);
        }

        public List<uint> GetMateSkills(uint id)
        {
            var template = new List<uint>();

            foreach (var value in _slaveMountSkills.Values)
            {
                if (value.NpcId == id && !template.Contains(value.MountSkillId))
                {
                    template.Add(value.MountSkillId);
                }
            }

            return template;
        }

        public void Load()
        {
            _nameRegex = new Regex(AppConfiguration.Instance.CharacterNameRegex, RegexOptions.Compiled);
            _slaveMountSkills = new Dictionary<uint, NpcMountSkills>();
            _activeMates = new Dictionary<uint, Mount>();

            #region SQLite

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npc_mount_skills";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var step = 0u;
                        while (reader.Read())
                        {
                            var template = new NpcMountSkills
                            {
                                //template.Id = reader.GetUInt32("id"); // there is no such field in the database for version 3030
                                Id = step++,
                                NpcId = reader.GetUInt32("npc_id"),
                                MountSkillId = reader.GetUInt32("mount_skill_id")
                            };
                            _slaveMountSkills.Add(template.Id, template);
                        }
                    }
                }
            }

            #endregion
        }
    }
}
