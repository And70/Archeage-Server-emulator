﻿using System;

using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game.Skills.Static;
using AAEmu.Game.Models.Game.Skills.Templates;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.Skills.Effects
{
    public class DispelEffect : EffectTemplate
    {
        public int DispelCount { get; set; }
        public int CureCount { get; set; }
        public uint BuffTagId { get; set; }

        public override bool OnActionTime => false;

        public override void Apply(Unit caster, SkillCaster casterObj, BaseUnit target, SkillCastTarget targetObj, CastAction castObj,
            Skill skill, SkillObject skillObject, DateTime time)
        {
            _log.Debug("DispelEffect");

            if (BuffTagId > 0 && !target.Effects.CheckBuffs(SkillManager.Instance.GetBuffsByTagId(BuffTagId)))
            {
                return;
            }

            if (DispelCount > 0)
            {
                target.Effects.RemoveBuffs(BuffKind.Good, DispelCount); //TODO ....
            }

            if (CureCount > 0)
            {
                target.Effects.RemoveBuffs(BuffKind.Bad, CureCount);
            }
        }
    }
}
