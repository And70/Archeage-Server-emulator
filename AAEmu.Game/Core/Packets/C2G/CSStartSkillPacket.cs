﻿using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Skills;
using AAEmu.Game.Models.Game.Skills.Static;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSStartSkillPacket : GamePacket
    {
        public CSStartSkillPacket() : base(CSOffsets.CSStartSkillPacket, 5)
        {
        }

        public override void Read(PacketStream stream)
        {
            var skillId = stream.ReadUInt32();

            var skillCasterType = stream.ReadByte(); // кто применяет
            var skillCaster = SkillCaster.GetByType((EffectOriginType)skillCasterType);
            skillCaster.Read(stream);

            var skillCastTargetType = stream.ReadByte(); // на кого применяют
            var skillCastTarget = SkillCastTarget.GetByType((SkillCastTargetType)skillCastTargetType);
            skillCastTarget.Read(stream);

            var flag = stream.ReadByte();
            var flagType = flag & 15;
            var skillObject = SkillObject.GetByType((SkillObjectType)flagType);
            if (flagType > 0)
            {
                skillObject.Read(stream);
            }

            _log.Debug("StartSkill: Id {0}, flag {1}", skillId, flag);

            if (SkillManager.Instance.IsDefaultSkill(skillId) || SkillManager.Instance.IsCommonSkill(skillId) && !(skillCaster is CasterEffectBuff))
            {
                var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId)); // TODO переделать...
                skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            }
            else if (skillCaster is CasterEffectBuff)
            {
                var item = Connection.ActiveChar.Inventory.GetItemById(((CasterEffectBuff)skillCaster).ItemId);
                if (item == null || skillId != item.Template.UseSkillId)
                {
                    return;
                }

                Connection.ActiveChar.Quests.OnItemUse(item);
                var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId));
                skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            }
            else if (Connection.ActiveChar.Skills.Skills.ContainsKey(skillId))
            {
                var skill = Connection.ActiveChar.Skills.Skills[skillId];
                skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            }
            else if (skillId > 0 && Connection.ActiveChar.Skills.IsVariantOfSkill(skillId))
            {
                var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId));
                skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            }
            else
            {
                _log.Warn("StartSkill: Id {0}, undefined use type", skillId);
            }
        }
    }
}
