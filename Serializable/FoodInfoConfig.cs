namespace GreenHellTweaks.Serializable
{
    public class FoodInfoConfig
    {
        /*
        protected override void FoodInfo.LoadParams(Key key)
        {
		    if (key.GetName() == "SpoilTime")
		    {
			    this.m_SpoilTime = key.GetVariable(0).FValue;
			    if (this.m_SpoilTime > 0f && Mod.Config.FoodInfoConfig.SpoilTime > 0f)
			    {
				    this.m_SpoilTime = Mod.Config.FoodInfoConfig.SpoilTime;
			    }
			    return;
		    }
        }
        */
        public float SpoilTime { get; set; }


        public FoodInfoConfig() 
        {
            SpoilTime = 0;
        }
    }
}
