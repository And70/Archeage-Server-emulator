﻿using System;

using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj.Templates;
using AAEmu.Game.Models.Game.Items.Actions;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Tasks.Doodads;

namespace AAEmu.Game.Models.Game.DoodadObj.Funcs
{
    public class DoodadFuncFinal : DoodadFuncTemplate
    {
        public int After { get; set; }
        public bool Respawn { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
        public bool ShowTip { get; set; }
        public bool ShowEndTime { get; set; }
        public string Tip { get; set; }

        public override void Use(Unit caster, Doodad owner, uint skillId)
        {
            _log.Debug("DoodadFuncFinal: skillId {0}, After {1}, Respawn {2}, MinTime {3}, MaxTime {4}, ShowTip {5}, ShowEndTime {6}, Tip {7}",
                skillId, After, Respawn, MinTime, MaxTime, ShowTip, ShowEndTime, Tip);

            var delay = Rand.Next(MinTime, MaxTime);
            var character = (Character)caster;
            if (character != null)
            {
                const int count = 1;
                var itemTemplate = ItemManager.Instance.GetItemIdsFromDoodad(owner.TemplateId);
                if (itemTemplate != null)
                {
                    foreach (var itemId in itemTemplate)
                    {
                        if (!character.Inventory.Bag.AcquireDefaultItem(ItemTaskType.AutoLootDoodadItem, itemId, count))
                        {
                            // TODO: do proper handling of insufficient bag space
                            character.SendErrorMessage(ErrorMessageType.BagFull);
                        }
                    }
                }
            }
            if (After > 0)
            {
                owner.FuncTask = new DoodadFuncFinalTask(caster, owner, skillId, Respawn);
                TaskManager.Instance.Schedule(owner.FuncTask, TimeSpan.FromMilliseconds(After)); // After ms remove the object from visibility
            }
            else
            {
                owner.Delete();
            }
        }
    }
}
