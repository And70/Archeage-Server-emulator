﻿using AAEmu.Game.Models.Game.DoodadObj.Templates;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.DoodadObj.Funcs
{
    public class DoodadFuncConsumeChangerModel : DoodadFuncTemplate
    {
        //public string Name { get; set; } // there is no such field in the database for version 3030

        public override void Use(Unit caster, Doodad owner, uint skillId)
        {
            _log.Debug("DoodadFuncConsumeChangerModel");
        }
    }
}