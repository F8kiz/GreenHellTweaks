namespace GHTweaks.Models
{
    internal class VicinityItem
    {
        public Trigger Trigger { get; private set; }

        public int ForcedLayer { get; private set; }


        public VicinityItem(Trigger trigger) 
        {
            Trigger = trigger;
            ForcedLayer = trigger.m_ForcedLayer;
        }
    }
}
