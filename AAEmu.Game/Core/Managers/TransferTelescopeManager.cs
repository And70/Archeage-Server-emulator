﻿using System;
using System.Collections.Generic;
using System.Numerics;

using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Tasks;
using AAEmu.Game.Models.Tasks.Telescopes;
using AAEmu.Game.Utils;

using NLog;

namespace AAEmu.Game.Core.Managers
{
    public class TransferTelescopeManager : Singleton<TransferTelescopeManager>
    {
        protected static Logger _log = LogManager.GetCurrentClassLogger();
        private Task transferTelescopeTickStartTask { get; set; }
        private const double Delay = 250;
        private Character owner { get; set; }

        public void TransferTelescopeStart(Character character)
        {
            owner = character;
            _log.Warn("TransferTelescopeTickStart: Started");

            transferTelescopeTickStartTask = new TransferTelescopeTickStartTask();
            //TaskManager.Instance.Schedule(transferTelescopeTickStartTask, TimeSpan.FromMilliseconds(Delay), TimeSpan.FromMilliseconds(Delay));
            TaskManager.Instance.Schedule(transferTelescopeTickStartTask, TimeSpan.FromMilliseconds(Delay));
        }

        internal void TransferTelescopeTick()
        {
            const int MaxCount = 10;
            var transfers = TransferManager.Instance.GetMoveTransfers();
            var transfers2 = new List<Transfer>();
            var ownerPos = new Vector3(owner.Position.X, owner.Position.Y, owner.Position.Z);
            foreach (var t in transfers)
            {
                var trPos = new Vector3(t.Position.X, t.Position.Y, t.Position.Z);
                if (!(MathF.Abs(MathUtil.GetDistance(ownerPos, trPos)) < 1000f)) { continue; }

                transfers2.Add(t);
            }
            transfers = transfers2.ToArray();
            for (var i = 0; i < transfers.Length; i += MaxCount)
            {
                var last = transfers.Length - i <= MaxCount;
                var temp = new Transfer[last ? transfers.Length - i : MaxCount];
                Array.Copy(transfers, i, temp, 0, temp.Length);
                owner?.BroadcastPacket(new SCTransferTelescopeUnitsPacket(last, temp), true);
            }
            TaskManager.Instance.Schedule(transferTelescopeTickStartTask, TimeSpan.FromMilliseconds(Delay));
        }

        public async void StopTransferTelescopeTick()
        {
            if (transferTelescopeTickStartTask == null) { return; }

            await transferTelescopeTickStartTask.Cancel();
            transferTelescopeTickStartTask = null;
            owner?.BroadcastPacket(new SCTransferTelescopeToggledPacket(false, 0), true);
            owner = null;
        }
    }
}
