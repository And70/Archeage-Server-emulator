using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCMineAmountPacket : GamePacket
    {
        private readonly int _amount;

        public SCMineAmountPacket(int amount) : base(SCOffsets.SCMineAmountPacket, 5)
        {
            _amount = amount;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_amount);
            return stream;
        }
    }
}
