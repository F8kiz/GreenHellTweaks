namespace GreenHellTweaks.Serializable
{
    public class PlayerMovementConfig
    {
        /*
        private void FPPController.UpdateWantedSpeed()
        {
            // Comes right after the first if-block
            if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.WalkSpeed > 0)
			    this.m_WalkSpeed = GreenHellTweaks.Mod.Config.PlayerMovementConfig.WalkSpeed;

            if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.BackwardWalkSpeed > 0)
			    this.m_BackwardWalkSpeed = GreenHellTweaks.Mod.Config.PlayerMovementConfig.BackwardWalkSpeed;

            if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.RunSpeed > 0)
			    this.m_RunSpeed = GreenHellTweaks.Mod.Config.PlayerMovementConfig.RunSpeed;

            if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.DuckSpeedMultiplier > 0)
			    this.m_DuckSpeedMul = GreenHellTweaks.Mod.Config.PlayerMovementConfig.DuckSpeedMultiplier;

            if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.MaxOverloadSpeedMultiplier > 0)
			    this.m_MaxOverloadSpeedMul = GreenHellTweaks.Mod.Config.PlayerMovementConfig.MaxOverloadSpeedMultiplier;  
        }
        */
        public float WalkSpeed { get; set; }

        public float BackwardWalkSpeed { get; set; }

        public float RunSpeed { get; set; }

        public float DuckSpeedMultiplier { get; set; }

        public float MaxOverloadSpeedMultiplier { get; set; }

        /*
        public SwimController()
        {
		    if (GreenHellTweaks.Mod.Config.PlayerMovementConfig.MaxSwimSpeed > 0f)
		    {
			    this.m_SpeedAddMax = GreenHellTweaks.Mod.Config.PlayerMovementConfig.MaxSwimSpeed;
		    }
		    else
		    {
			    this.m_SpeedAddMax = 1f;
		    }
        }
        */
        public float MaxSwimSpeed { get; set; }



        public PlayerMovementConfig()
        {
            WalkSpeed = 4f;
            BackwardWalkSpeed = 2f;
            RunSpeed = 8f;
            DuckSpeedMultiplier = 0.5f;
            MaxOverloadSpeedMultiplier = 0.3f;
            MaxSwimSpeed = 1f;
        }
    }
}
