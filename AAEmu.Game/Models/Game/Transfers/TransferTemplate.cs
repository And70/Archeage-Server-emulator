﻿using System.Collections.Generic;

namespace AAEmu.Game.Models.Game.Transfers
{
    public class TransferTemplate
    {
        public uint Id { get; set; }     // TemplateId -> owner_id
        public string Name { get; set; } // comment
        public uint ModelId { get; set; }
        public double WaitTime { get; set; }
        public bool Cyclic { get; set; }
        public float PathSmoothing { get; set; }
        public List<TransferBindings> TransferBindings { get; }             // selection by owner_id
        public List<TransferPaths> TransferPaths { get; }                   // selection by owner_id
        public List<TransferRoads> TransferRoads { get; }                   // здесь список участков для конкретной модели транспорта
        public List<TransferBindingDoodads> TransferBindingDoodads { get; } // selection by owner_id
        //                  v--TemplateId
        //public Dictionary<uint, List<TransferRoads>> TransferAllRoads { get; }  // непосредственно список точек всех путей

        public TransferTemplate()
        {
            TransferBindings = new List<TransferBindings>();
            TransferPaths = new List<TransferPaths>();
            TransferBindingDoodads = new List<TransferBindingDoodads>();
            //TransferAllRoads = new Dictionary<uint, List<TransferRoads>>();
            TransferRoads = new List<TransferRoads>();
        }
    }
}