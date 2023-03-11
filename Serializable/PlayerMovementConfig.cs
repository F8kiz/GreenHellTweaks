namespace GreenHellTweaks.Serializable
{
    public class PlayerMovementConfig
    {
        public float WalkSpeed { get; set; }

        public float BackwardWalkSpeed { get; set; }

        public float RunSpeed { get; set; }

        public float DuckSpeedMultiplier { get; set; }

        public float MaxOverloadSpeedMultiplier { get; set; }

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
