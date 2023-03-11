namespace GreenHellTweaks.Serializable
{
    public class TorchConfig
    {
        /*
	    private void Torch.UpdateBurning()
        {
            if (!this.m_Burning || this.m_DebugInfiniteBurn || GreenHellTweaks.Mod.Config.TorchConfig.InfiniteBurn)
		    {
			    return;
		    }
        }

        Torch.CheckIfInBackPack()
        {
            // ...
            this.Extinguish();
            if (Inventory3DManager.Get().m_ActivePocket != BackpackPocket.Left)
            {
                // ...
            }
            // ...
        }
        */
        public bool InfiniteBurn { get; set; }
    }
}
