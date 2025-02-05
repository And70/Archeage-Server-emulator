﻿using System;
using System.Collections.Generic;

using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Expeditions;
using AAEmu.Game.Models.Game.Items;
using AAEmu.Game.Models.Game.Skills;
using AAEmu.Game.Models.Game.World;
using AAEmu.Game.Models.Tasks;
using AAEmu.Game.Models.Tasks.Skills;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Models.Game.Units
{
    public class Unit : BaseUnit
    {
        private Task _regenTask;
        public uint ModelId { get; set; }
        public byte Level { get; set; }
        public int Hp { get; set; }
        public virtual int MaxHp { get; set; }
        public virtual int HpRegen { get; set; }
        public virtual int PersistentHpRegen { get; set; } = 30;
        public int HighAbilityRsc { get; set; }
        public int Mp { get; set; }
        public virtual int MaxMp { get; set; }
        public virtual int MpRegen { get; set; }
        public virtual int PersistentMpRegen { get; set; } = 30;
        public virtual float LevelDps { get; set; }
        public virtual int Dps { get; set; }
        public virtual int DpsInc { get; set; }
        public virtual int OffhandDps { get; set; }
        public virtual int RangedDps { get; set; }
        public virtual int RangedDpsInc { get; set; }
        public virtual int MDps { get; set; }
        public virtual int MDpsInc { get; set; }
        public virtual int HDps { get; set; }
        public virtual int HDpsInc { get; set; }
        public virtual int Armor { get; set; }
        public virtual int MagicResistance { get; set; }
        public BaseUnit CurrentTarget { get; set; }
        public virtual byte RaceGender => 0;
        public virtual UnitCustomModelParams ModelParams { get; set; }
        public byte ActiveWeapon { get; set; }
        public bool IdleStatus { get; set; }
        public bool ForceAttack { get; set; }
        public bool Invisible { get; set; }
        public uint OwnerId { get; set; }
        public SkillTask SkillTask { get; set; }
        public SkillTask AutoAttackTask { get; set; }
        public Dictionary<uint, List<Bonus>> Bonuses { get; set; }
        public Expedition Expedition { get; set; }
        public bool IsInBattle { get; set; }
        public bool IsInPatrol { get; set; } // so as not to run the route a second time
        public int SummarizeDamage { get; set; }
        public bool IsAutoAttack = false;
        public uint SkillId;
        public ushort TlId { get; set; }
        public ItemContainer Equipment { get; set; }
        public GameConnection Connection { get; set; }
        private readonly object _doDieLock = new object();
        public Dictionary<uint, DateTime> CooldownsSkills { get; set; }
        public Dictionary<uint, DateTime> CooldownsBuffs { get; set; }

        public Unit()
        {
            Bonuses = new Dictionary<uint, List<Bonus>>();
            IsInBattle = false;
            // TODO 1.2 Equipment.ContainerSize = 28, at 3.0.3.0 Equipment.ContainerSize = 29
            Equipment = new ItemContainer(null, SlotType.Equipment, true)
            {
                ContainerSize = 29
            };
            WorldPos = new WorldPos();
            Position = new Point();
            CooldownsSkills = new Dictionary<uint, DateTime>();
            CooldownsBuffs = new Dictionary<uint, DateTime>();
        }

        public virtual void ReduceCurrentHp(Unit attacker, int value)
        {
            Hp = Math.Max(Hp - value, 0);
            if (Hp <= 0)
            {
                StopRegen();
                DoDie(attacker);
                return;
            }
            StartRegen();
            BroadcastPacket(new SCUnitPointsPacket(ObjId, Hp, Hp > 0 ? Mp : 0, Hp > 0 ? HighAbilityRsc : 0), true);
        }

        public virtual void ReduceCurrentMp(Unit attacker, int value)
        {
            if (Hp <= 0) // если юнит мертв, то не надо менять MP
            {
                return;
            }

            Mp = Math.Max(Mp - value, 0);
            if (Mp >= MaxMp)
            {
                //StopRegen(); // нельзя останавливать реген, в этот момент может быть 0 < HP < MaxHp
                Mp = MaxMp;
                return;
            }
            StartRegen();
            BroadcastPacket(new SCUnitPointsPacket(ObjId, Hp, Hp > 0 ? Mp : 0, Hp > 0 ? HighAbilityRsc : 0), true);
        }

        public virtual void DoDie(Unit killer)
        {
            lock (_doDieLock)
            {
                Effects.RemoveEffectsOnDeath();
                killer.BroadcastPacket(new SCUnitDeathPacket(ObjId, 1, killer), true);
                var lootDropItems = ItemManager.Instance.CreateLootDropItems(ObjId);
                if (lootDropItems.Count > 0)
                {
                    killer.BroadcastPacket(new SCLootableStatePacket(ObjId, true), true);
                }

                if (CurrentTarget != null)
                {
                    killer.BroadcastPacket(new SCAiAggroPacket(killer.ObjId, 0), true);
                    killer.SummarizeDamage = 0;

                    killer.BroadcastPacket(new SCCombatClearedPacket(killer.CurrentTarget.ObjId), true);
                    killer.BroadcastPacket(new SCCombatClearedPacket(killer.ObjId), true);
                    killer.StartRegen();
                    killer.BroadcastPacket(new SCTargetChangedPacket(killer.ObjId, 0), true);

                    if (killer is Character character)
                    {
                        character.StopAutoSkill(character);
                        character.IsInBattle = false; // we need the character to be "not in battle"
                    }
                    else if (killer.CurrentTarget is Character character2)
                    {
                        character2.StopAutoSkill(character2);
                        character2.IsInBattle = false; // we need the character to be "not in battle"
                        character2.DeadTime = DateTime.UtcNow;
                    }

                    killer.CurrentTarget = null;
                }
                else
                {
                    killer.BroadcastPacket(new SCAiAggroPacket(killer.ObjId, 0), true);
                    killer.SummarizeDamage = 0;
                    killer.BroadcastPacket(new SCCombatClearedPacket(killer.ObjId), true);
                    killer.StartRegen();
                    killer.BroadcastPacket(new SCTargetChangedPacket(killer.ObjId, 0), true);
                }
            }
        }

        private async void StopAutoSkill(Unit character)
        {
            if (!(character is Character) || character.AutoAttackTask == null)
            {
                return;
            }

            await character.AutoAttackTask.Cancel();
            character.AutoAttackTask = null;
            character.IsAutoAttack = false; // turned off auto attack
            character.BroadcastPacket(new SCSkillEndedPacket(character.TlId), true);
            character.BroadcastPacket(new SCSkillStoppedPacket(character.ObjId, character.SkillId), true);
            TlIdManager.Instance.ReleaseId(character.TlId);
        }

        public void StartRegen()
        {
            if (_regenTask != null || Hp >= MaxHp && Mp >= MaxMp || Hp == 0)
            {
                return;
            }
            _regenTask = new UnitPointsRegenTask(this);
            TaskManager.Instance.Schedule(_regenTask, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        public async void StopRegen()
        {
            if (_regenTask == null)
            {
                return;
            }
            await _regenTask.Cancel();
            _regenTask = null;
        }

        public void SetInvisible(bool value)
        {
            Invisible = value;
            BroadcastPacket(new SCUnitInvisiblePacket(ObjId, Invisible), true);
        }

        public void SetForceAttack(bool value)
        {
            ForceAttack = value;
            BroadcastPacket(new SCForceAttackSetPacket(ObjId, ForceAttack), true);
        }

        public override void AddBonus(uint bonusIndex, Bonus bonus)
        {
            var bonuses = Bonuses.ContainsKey(bonusIndex) ? Bonuses[bonusIndex] : new List<Bonus>();
            bonuses.Add(bonus);
            Bonuses[bonusIndex] = bonuses;
        }

        public override void RemoveBonus(uint bonusIndex, UnitAttribute attribute)
        {
            if (!Bonuses.ContainsKey(bonusIndex))
            {
                return;
            }
            var bonuses = Bonuses[bonusIndex];
            foreach (var bonus in new List<Bonus>(bonuses))
            {
                if (bonus.Template != null && bonus.Template.Attribute == attribute)
                {
                    bonuses.Remove(bonus);
                }
            }
        }

        public List<Bonus> GetBonuses(UnitAttribute attribute)
        {
            var result = new List<Bonus>();
            if (Bonuses == null)
            {
                return result;
            }
            foreach (var bonuses in new List<List<Bonus>>(Bonuses.Values))
            {
                foreach (var bonus in new List<Bonus>(bonuses))
                {
                    if (bonus.Template != null && bonus.Template.Attribute == attribute)
                    {
                        result.Add(bonus);
                    }
                }
            }
            return result;
        }
        public void SendPacket(GamePacket packet)
        {
            Connection?.SendPacket(packet);
        }

        public void SendErrorMessage(ErrorMessageType type)
        {
            SendPacket(new SCErrorMsgPacket(type, 0, true));
        }

        public float GetDistanceTo(BaseUnit baseUnit, bool includeZAxis = false)
        {
            if (Position == baseUnit.Position)
                return 0.0f;
            
            var rawDist = MathUtil.CalculateDistance(this.Position, baseUnit.Position, includeZAxis);

            //rawDist -= ModelManager.Instance.GetActorModel(ModelId)?.Radius ?? 0 * Scale;
            //if (baseUnit is Unit unit)
            //    rawDist -= ModelManager.Instance.GetActorModel(unit.ModelId)?.Radius ?? 0 * unit.Scale;
            
            return Math.Max(rawDist, 0);
        }
    }
}
