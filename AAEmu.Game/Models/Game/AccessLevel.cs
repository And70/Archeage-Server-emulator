using System.Collections.Generic;

using AAEmu.Commons.Network;

namespace AAEmu.Game.Models.Game
{
    public class AccessLevel : PacketMarshaler
    {
        public static List<Command> CMD = new List<Command>();

        public static int getLevel(string commandstr)
        {
            Command result = CMD.Find(o => o.command == commandstr);
            if (result != null)
            {
                return result.level;
            }

            return 0;
        }

        public static void Load()
        {
            //Console.WriteLine("LOADING COMMAND STUFF");
            //CMD.Add(new Command{ command = "spawn", level = 80 });
            //CMD.Add(new Command{ command = "test", level = 80 });
            //CMD.Add(new Command{ command = "kill", level = 80 });
        }
    }

    public class Command : PacketMarshaler
    {

        public string command { get; set; }
        public int level { get; set; }

    }

}