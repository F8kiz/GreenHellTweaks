using GHTweaks.Configuration.Core;
using GHTweaks.Models;

using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace GHTweaks.Configuration
{
    public class Config
    {
        [PatchCategory(PatchCategory.Construction)]
        public ConstructionConfig ConstructionConfig { get; set; }

        [PatchCategory(PatchCategory.PocketGrid)]
        public PocketGridConfig PocketGridConfig { get; set; }

        [PatchCategory(PatchCategory.InventoryBackpack)]
        public InventoryBackpackConfig InventoryBackpackConfig { get; set; }

        [PatchCategory(PatchCategory.Player)]
        public PlayerConfig PlayerConfig { get; set; }

        [PatchCategory(PatchCategory.PlayerConditionModul)]
        public PlayerConditionModuleConfig PlayerConditionModuleConfig { get; set; }

        [PatchCategory(PatchCategory.PlayerMovement)]
        public PlayerMovementConfig PlayerMovementConfig { get; set; }

        [PatchCategory(PatchCategory.LiquidContainer)]
        public LiquidContainerConfig LiquidContainerConfig { get; set; }

        [PatchCategory(PatchCategory.FoodInfo)]
        public FoodInfoConfig FoodInfoConfig { get; set; }

        [PatchCategory(PatchCategory.ItemInfo)]
        public ItemInfoConfig ItemInfoConfig { get; set; }

        [PatchCategory(PatchCategory.TimeOfDay)]
        public TODTimeConfig TODTimeConfig { get; set; }

        [PatchCategory(PatchCategory.FireCamp)]
        public FireCampConfig FireCampConfig { get; set; }

        [PatchCategory(PatchCategory.Torch)]
        public TorchConfig TorchConfig { get; set; }

        [PatchCategory(PatchCategory.Skill)]
        public SkillConfig SkillConfig { get; set; }

        [PatchCategory(PatchCategory.DestroyFallingObjects)]
        public DestroyableFallingObjectConfig DestroyableFallingObjectConfig { get; set; }

        [PatchCategory(PatchCategory.AISoundModule)]
        public AISoundModuleConfig AISoundModuleConfig { get; set; }

        [PatchCategory(PatchCategory.BirdHouse)]
        public BirdHouseConfig BirdHouseConfig { get; set; }

        [XmlElement(IsNullable = true)]
        public SerializableVector3 PlayerHomePosition { get; set; }

        [XmlIgnore]
        public Vector3 PlayerLastPosition { get; set; }

        public bool DebugModeEnabled { get; set; }

        public bool SkipIntro { get; set; }

        public bool ConsumeKeyStrokes { get; set; }

        public bool CheatsEnabled { get; set; }

        public List<KeyBinding> KeyBindings { get; set; }


        public Config()
        {
            ConstructionConfig = new ConstructionConfig();
            PocketGridConfig = new PocketGridConfig();
            InventoryBackpackConfig = new InventoryBackpackConfig();
            PlayerConfig = new PlayerConfig();
            PlayerConditionModuleConfig = new PlayerConditionModuleConfig();
            PlayerMovementConfig = new PlayerMovementConfig();
            LiquidContainerConfig = new LiquidContainerConfig();
            FoodInfoConfig = new FoodInfoConfig();
            ItemInfoConfig = new ItemInfoConfig();
            TODTimeConfig = new TODTimeConfig();
            FireCampConfig = new FireCampConfig();
            TorchConfig = new TorchConfig();
            SkillConfig = new SkillConfig();
            DestroyableFallingObjectConfig = new DestroyableFallingObjectConfig();
            AISoundModuleConfig = new AISoundModuleConfig();
            BirdHouseConfig = new BirdHouseConfig();
            ConsumeKeyStrokes = true;
            KeyBindings = new List<KeyBinding>();
        }
    }
}
