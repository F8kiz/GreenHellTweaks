﻿using GHTweaks.Serializable;

using System.Xml.Serialization;
using UnityEngine;

namespace GHTweaks.Serializable
{
    public class Config
    {
        public ConstructionConfig ConstructionConfig { get; set; }

        public PocketGridConfig PocketGridConfig { get; set; }

        public InventoryBackpackConfig InventoryBackpackConfig { get; set; }

        public PlayerConfig PlayerConfig { get; set; }

        public PlayerConditionModuleConfig PlayerConditionModuleConfig { get; set; }

        public PlayerMovementConfig PlayerMovementConfig { get; set; }

        public LiquidContainerInfoConfig LiquidContainerInfoConfig { get; set; }

        public FoodInfoConfig FoodInfoConfig { get; set; }

        public ItemInfoConfig ItemInfoConfig { get; set; }

        public TODTimeConfig TODTimeConfig { get; set; }

        public FireCampConfig FireCampConfig { get; set; }

        public TorchConfig TorchConfig { get; set; }

        public SkillConfig SkillConfig { get; set; }

        [XmlElement(IsNullable = true)]
        public SerializeVector3 PlayerHomePosition { get; set; }

        [XmlIgnore]
        public Vector3 PlayerLastPosition { get; set; }

        public bool DebugModeEnabled { get; set; }

        public bool SkipIntro { get; set; }



        public Config()
        {
            ConstructionConfig = new ConstructionConfig();
            PocketGridConfig = new PocketGridConfig();
            InventoryBackpackConfig = new InventoryBackpackConfig();
            PlayerConfig = new PlayerConfig();
            PlayerConditionModuleConfig = new PlayerConditionModuleConfig();
            PlayerMovementConfig = new PlayerMovementConfig();
            LiquidContainerInfoConfig = new LiquidContainerInfoConfig();
            FoodInfoConfig = new FoodInfoConfig();
            ItemInfoConfig = new ItemInfoConfig();
            TODTimeConfig = new TODTimeConfig();
            FireCampConfig = new FireCampConfig();
            TorchConfig = new TorchConfig();
            SkillConfig = new SkillConfig();
        }
    }
}
