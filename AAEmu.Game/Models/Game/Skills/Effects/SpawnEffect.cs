﻿using System;

using AAEmu.Game.Models.Game.Skills.Templates;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.Skills.Effects
{
    public class SpawnEffect : EffectTemplate
    {
        public uint OwnerTypeId { get; set; }
        public uint SubType { get; set; }
        public uint PosDirId { get; set; }
        //public float PosAngle { get; set; } // there is no such field in the database for version 3030
        public float PosDistanceMax { get; set; }
        public float PosDistanceMin { get; set; }
        public uint OriDirId { get; set; }
        public float OriAngle { get; set; }
        public bool UseSummonerFaction { get; set; }
        public float LifeTime { get; set; }
        public bool DespawnOnCreatorDeath { get; set; }

        public bool UseSummoneerAggroTarget { get; set; }
        // TODO 1.2 // public uint MateStateId { get; set; }

        public override bool OnActionTime => false;

        public override void Apply(Unit caster, SkillCaster casterObj, BaseUnit target, SkillCastTarget targetObj, CastAction castObj,
            Skill skill, SkillObject skillObject, DateTime time)
        {
            _log.Debug("SpawnEffect");
        }
    }
}
