﻿using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Models.Game.Units;

using NLog;

namespace AAEmu.Game.Models.Game.DoodadObj
{
    public class DoodadFunc
    {

        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        public uint GroupId { get; set; }
        public uint FuncId { get; set; }
        public uint FuncKey { get; set; }
        public string FuncType { get; set; }
        public int NextPhase { get; set; }
        public uint SoundId { get; set; }
        public uint SkillId { get; set; }
        public uint PermId { get; set; }
        public int Count { get; set; }

        //This acts as an interface/relay for doodad function chain
        public async void Use(Unit caster, Doodad owner, uint skillId)
        {
            var template = DoodadManager.Instance.GetFuncTemplate(FuncId, FuncType);

            if (template == null)
            {
                return;
            }
            //_log.Debug("relaying to: " + FuncType);
            template.Use(caster, owner, skillId);
        }
    }
}
