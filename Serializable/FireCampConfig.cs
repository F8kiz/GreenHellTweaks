namespace GreenHellTweaks.Serializable
{
    public class FireCampConfig
    {
        /*
        FireCamp.UpdateFireLevel() 
        {
            // ...
            if (this.m_EndlessFire || Mod.Config.FireCampConfig.EndlessFire)
		    {
			    this.m_FireLevel = 1f;
			    return;
		    }
            // ...
        } 
        */
        public bool EndlessFire { get; set; }
    }
}
