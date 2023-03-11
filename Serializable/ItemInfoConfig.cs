namespace GreenHellTweaks.Serializable
{
    public class ItemInfoConfig
    {
        /*
		public float m_Health
		{
			get
			{
				return this.health;
			}
			set
			{
				if (Mod.Config.ItemInfoConfig.UnbreakableWeapons && (this.IsAxe() || this.IsBlowpipe() || this.IsBow() || this.IsKnife() || this.IsMachete() || this.IsSpear() || this.IsWeapon()) && this.health < this.m_MaxHealth)
				{
					this.health = this.m_MaxHealth;
					return;
				}
				if (Mod.Config.ItemInfoConfig.UnbreakableArmor && this.IsArmor() && this.health < this.m_MaxHealth)
				{
					this.health = this.m_MaxHealth;
					return;
				}
				this.health = value;
			}
		}
		private float health = 100f;
        */
        public bool UnbreakableWeapons { get; set; }

		public bool UnbreakableArmor { get; set; }


        public ItemInfoConfig() => UnbreakableWeapons = false;
    }
}
