﻿using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Housing;
using AAEmu.Game.Models.Game.NPChar;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSChangeTargetPacket : GamePacket
    {
        public CSChangeTargetPacket() : base(CSOffsets.CSChangeTargetPacket, 5)
        {
        }

        public override void Read(PacketStream stream)
        {
            var targetId = stream.ReadBc();
            Connection
                    .ActiveChar
                    .CurrentTarget = targetId > 0 ? WorldManager.Instance.GetUnit(targetId) : null;

            Connection
                .ActiveChar
                .BroadcastPacket(
                    new SCTargetChangedPacket(Connection.ActiveChar.ObjId,
                        Connection.ActiveChar.CurrentTarget?.ObjId ?? 0), true);

            if (Connection.ActiveChar.CurrentTarget == null)
            {
                return;
            }

            if (Connection.ActiveChar.CurrentTarget is Npc npc)
            {
                Connection.ActiveChar.SendMessage("ObjId: {0}, Npc TemplateId: {1}", targetId, npc.TemplateId);
            }
            else if (Connection.ActiveChar.CurrentTarget is Mount mount)
            {
                Connection.ActiveChar.SendMessage("ObjId: {0}, Mount TemplateId: {1}", targetId, mount.TemplateId);
            }
            else if (Connection.ActiveChar.CurrentTarget is Transfer transfer)
            {
                Connection.ActiveChar.SendMessage("ObjId: {0}, Transfer TemplateId: {1}", targetId, transfer.TemplateId);
            }
            else if (Connection.ActiveChar.CurrentTarget is House house)
            {
                Connection.ActiveChar.SendMessage("ObjId: {0}, House TemplateId: {1}", targetId, house.Id);
            }
            else if (Connection.ActiveChar.CurrentTarget is Character character)
            {
                Connection.ActiveChar.SendMessage("ObjId: {0}, CharacterId: {1}", targetId, character.Id);
            }
        }
    }
}
