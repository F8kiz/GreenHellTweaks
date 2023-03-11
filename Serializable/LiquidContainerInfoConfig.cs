namespace GreenHellTweaks.Serializable
{
    public class LiquidContainerInfoConfig
    {
        /*
	    public float m_Capacity
	    {
		    get
		    {
			    return this.capacity;
		    }
		    set
		    {
			    if (this.m_LiquidType >= LiquidType.Water && this.m_LiquidType <= LiquidType.DirtyWater && GreenHellTweaks.Mod.Config.LiquidContainerInfoConfig.MinCapacity > 0f)
			    {
				    this.capacity = GreenHellTweaks.Mod.Config.LiquidContainerInfoConfig.MinCapacity;
				    return;
			    }
			    this.capacity = value;
		    }
	    }
	    private float capacity = 100f; 
        */
        public float MinCapacity
        {
            get => minCapacity;
            set
            {
                minCapacity = value;
            }
        }
        private float minCapacity;

        public LiquidContainerInfoConfig()
        {
            minCapacity = 100f;
        }
    }
}
