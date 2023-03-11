namespace GreenHellTweaks.Serializable
{
    public class TODTimeConfig
    {
        /*
        TOD_Time.UpdateTime()
        {
		    if (GreenHellTweaks.Mod.Config.TODTimeConfig.DayLengthInMinutes > 0)
                this.m_DayLengthInMinutes = GreenHellTweaks.Mod.Config.TODTimeConfig.DayLengthInMinutes;

            if (GreenHellTweaks.Mod.Config.TODTimeConfig.NightLengthInMinutes > 0)
                this.m_NightLengthInMinutes = GreenHellTweaks.Mod.Config.TODTimeConfig.NightLengthInMinutes;
        }
        */
        public float DayLengthInMinutes { get; set; }

        public float NightLengthInMinutes { get; set; }



        public TODTimeConfig() 
        {
            DayLengthInMinutes = 20;
            NightLengthInMinutes = 10;
        }
    }
}
