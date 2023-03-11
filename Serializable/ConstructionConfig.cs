namespace GreenHellTweaks.Serializable
{
    public class ConstructionConfig
    {
        public bool PlaceEveryWhereEnabled { get; set; }

        public bool InstantBuild { get; set; }

        public bool DestroyConstructionWithoutItems { get; set; }

        public bool OneShotConstructions { get; set; }


        public ConstructionConfig()
        {
            PlaceEveryWhereEnabled = true;
        }
    }
}
