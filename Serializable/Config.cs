using System.Xml.Serialization;
using UnityEngine;

namespace GreenHellTweaks.Serializable
{
    public class Config
    {
        public ConstructionConfig ConstructionConfig { get; set; }

        public InventoryBackpackConfig InventoryBackpackConfig { get; set; }

        public PlayerConditionModuleConfig PlayerConditionModuleConfig { get; set; }

        public PlayerMovementConfig PlayerMovementConfig { get; set; }

        public LiquidContainerInfoConfig LiquidContainerInfoConfig { get; set; }

        public FoodInfoConfig FoodInfoConfig { get; set; }

        public ItemInfoConfig ItemInfoConfig { get; set; }

        public TODTimeConfig TODTimeConfig { get; set; }

        public FireCampConfig FireCampConfig { get; set; }

        public TorchConfig TorchConfig { get; set; }

        [XmlElement(IsNullable = true)]
        public SerializeVector3 PlayerHomePosition { get; set; }

        [XmlIgnore]
        public Vector3 PlayerLastPosition { get; set; }

        public bool DebugModeEnabled { get; set; }


        /*
        MainMenu : base() 
        {
		    if (!GreenHellTweaks.Mod.Config.SkipIntro)
		    {							
				this.m_FadeInDuration = 1f;
				this.m_FadeOutDuration = 1.7f;
				this.m_FadeOutSceneDuration = 3f;
			    this.m_CompanyLogoDuration = 5f;
			    this.m_GameLogoDuration = 7f;
				this.m_BlackScreenDuration = 1f;
		    }
		    else 
		    {
				this.m_FadeInDuration = 0f;
				this.m_FadeOutDuration = 0;
				this.m_FadeOutSceneDuration = 0f;
			    this.m_CompanyLogoDuration = 0f;
			    this.m_GameLogoDuration = 0f;
				this.m_BlackScreenDuration = 0f;
		    }
        }
        */
        public bool SkipIntro { get; set; }



        public Config()
        {
            ConstructionConfig = new ConstructionConfig();
            InventoryBackpackConfig = new InventoryBackpackConfig();
            PlayerConditionModuleConfig = new PlayerConditionModuleConfig();
            PlayerMovementConfig = new PlayerMovementConfig();
            LiquidContainerInfoConfig = new LiquidContainerInfoConfig();
            FoodInfoConfig = new FoodInfoConfig();
            ItemInfoConfig = new ItemInfoConfig();
            TODTimeConfig = new TODTimeConfig();
            FireCampConfig = new FireCampConfig();
            TorchConfig = new TorchConfig();
        }
    }
}
