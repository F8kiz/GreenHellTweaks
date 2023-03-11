namespace GreenHellTweaks.Serializable
{
    public class PlayerConditionModuleConfig
    {
        /*
		public float PlayerConditionModule.m_HP
		{
			get
			{
				return this.m_HPProp;
			}
			set
			{
				float minHP = GreenHellTweaks.Mod.Config.PlayerPlayerConditionModuleConfig.MinHealthPoints;
				this.m_HPProp = minHP > -1 && value < minHP ? minHP : value;
			}
		}
		*/
        public float MinHealthPoints { get; set; }

        // PlayerConditionModule.UpdateDirtiness()
        // this.m_Dirtiness -= GreenHellTweaks.Mod.Config.PlayerPlayerConditionModuleConfig.DecreaseDirtinessOnRainValue * Time.deltaTime;
        public float DecreaseDirtinessOnRainValue { get; set; }

        // PlayerConditionModule.UpdateDirtiness()
        // this.m_Dirtiness -= GreenHellTweaks.Mod.Config.PlayerPlayerConditionModuleConfig.DecreaseDirtinessOnSwimValue * Time.deltaTime;
        public float DecreaseDirtinessOnSwimValue { get; set; }

        // PlayerConditionModule.UpdateDirtiness()
        // this.m_Dirtiness -= GreenHellTweaks.Mod.Config.PlayerPlayerConditionModuleConfig.DecreaseDirtinessOnDiveValue * Time.deltaTime;
        public float DecreaseDirtinessOnDiveValue { get; set; }

        // Fat
        /*
        public float m_NutritionFat
		{
			get
			{
				return this.nutritionFat;
			}
			set
			{
				if (Mod.Config.PlayerPlayerConditionModuleConfig.NutritionFatConsumptionThreshold > 0f && (this.nutritionFat - value > Mod.Config.PlayerPlayerConditionModuleConfig.NutritionFatConsumptionThreshold))
				{
					this.nutritionFat -= Mod.Config.PlayerPlayerConditionModuleConfig.NutritionFatConsumptionThreshold;
					return;
				}
				this.nutritionFat = value;
			}
		}
		private float nutritionFat = 0;
        */
        public float NutritionFatConsumptionThreshold
        {
            get => nutritionFatConsumptionThreshold;
            set
            {
                if (value > -1 && value < 1)
                    nutritionFatConsumptionThreshold = value;
            }
        }
        private float nutritionFatConsumptionThreshold = 0;

        // Carbohydrates
        /*
        public float m_NutritionCarbo
		{
			get
			{
				return this.nutritionCarbo;
			}
			set
			{
				if (Mod.Config.PlayerPlayerConditionModuleConfig.NutritionCarbohydratesConsumptionThreshold > 0f && (this.nutritionCarbo - value > Mod.Config.PlayerPlayerConditionModuleConfig.NutritionCarbohydratesConsumptionThreshold))
				{
					this.nutritionCarbo -= Mod.Config.PlayerPlayerConditionModuleConfig.NutritionCarbohydratesConsumptionThreshold;
					return;
				}
				this.nutritionCarbo = value;
			}
		}
		private float nutritionCarbo = 0;
        */
        public float NutritionCarbohydratesConsumptionThreshold
        {
			get => nutritionCarbohydratesConsumptionThreshold;
			set
			{
				if (value > -1 && value < 1)
                    nutritionCarbohydratesConsumptionThreshold = value;
			}
		}
		private float nutritionCarbohydratesConsumptionThreshold = 0;

        // Protein
        /*
        public float m_NutritionProteins
	    {
		    get
		    {
			    return this.nutritionProteins;
		    }
		    set
		    {
			    if (Mod.Config.PlayerPlayerConditionModuleConfig.NutritionProteinsConsumptionThreshold > 0f && (this.nutritionProteins - value > Mod.Config.PlayerPlayerConditionModuleConfig.NutritionProteinsConsumptionThreshold))
			    {
				    this.nutritionProteins -= Mod.Config.PlayerPlayerConditionModuleConfig.NutritionProteinsConsumptionThreshold;
				    return;
			    }
			    this.nutritionProteins = value;
		    }
	    }
		private float nutritionProteins = 0;
        */
        public float NutritionProteinsConsumptionThreshold
        {
			get => nutritionProteinsConsumptionThreshold;
			set
			{
                if (value > -1 && value < 1)
                    nutritionProteinsConsumptionThreshold = value;
            }
		}
		private float nutritionProteinsConsumptionThreshold = 0;

        // Hydration
        /*
        public float m_Hydration
	    {
		    get
		    {
			    return this.hydration;
		    }
		    set
		    {
			    if (Mod.Config.PlayerPlayerConditionModuleConfig.HydrationConsumptionThreshold > 0f && (this.hydration - value > Mod.Config.PlayerPlayerConditionModuleConfig.HydrationConsumptionThreshold))
			    {
				    this.hydration -= Mod.Config.PlayerPlayerConditionModuleConfig.HydrationConsumptionThreshold;
				    return;
			    }
			    this.hydration = value;
		    }
	    }
		private float hydration = 0;
        */
        public float HydrationConsumptionThreshold
        {
			get => hydrationConsumptionThreshold;
			set
			{
                if (value > -1 && value < 1)
                    hydrationConsumptionThreshold = value;
            }
		}
		private float hydrationConsumptionThreshold = 0;




        public PlayerConditionModuleConfig() 
        {
            MinHealthPoints = -1;
            DecreaseDirtinessOnRainValue = 3f;
            DecreaseDirtinessOnSwimValue = 10f;
            DecreaseDirtinessOnDiveValue = 30f;
            NutritionCarbohydratesConsumptionThreshold = 0f;
            NutritionFatConsumptionThreshold = 0f;
            NutritionProteinsConsumptionThreshold = 0f;
            HydrationConsumptionThreshold = 0f;
        }
    }
}
