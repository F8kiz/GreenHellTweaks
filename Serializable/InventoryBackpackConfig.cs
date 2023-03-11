namespace GreenHellTweaks.Serializable
{
    public class InventoryBackpackConfig
    {
        /*
        private void Awake()
        {
            InventoryBackpack.s_Instance = this; // <= Just a reminder to remind where the place to go.
            this.m_MaxWeight = GreenHellTweaks.Mod.Config.InventoryBackpackConfig.MaxWeight;
        }
        */
        public float MaxWeight 
        { 
            get => maxWeigth;
            set
            {
                if (value > 0)
                    maxWeigth = value;
            }
        }
        private float maxWeigth = 50f;

        /*
        public override bool ItemSlotStack.IsOccupied()
	    {
		    return !GreenHellTweaks.Mod.Config.InventoryBackpackConfig.UnlimitedItemStackSize && this.m_Items.Count >= this.m_StackDummies.Count;
	    }
        */
        /*
        Inventory3DManager.TryAttractCarriedItemToSlot() 
        {
            if (this.m_SelectedSlot.IsStack())
		    {
			    ItemSlotStack itemSlotStack = (ItemSlotStack)this.m_SelectedSlot;
			    int index = System.Math.Min(itemSlotStack.m_StackDummies.Count, itemSlotStack.m_Items.Count) - 1;
			    this.m_CarriedItem.transform.position = itemSlotStack.m_StackDummies[index].transform.position;
			    if (itemSlotStack.m_AdjustRotation)
			    {
				    this.m_CarriedItem.transform.rotation = itemSlotStack.m_StackDummies[index].transform.rotation;
			    }
		    }
        }

        private void HUDItemSlot.UpdateSlots(SlotData data)
        {
            //...
        	Color color;
		    if (Inventory3DManager.Get().m_SelectedSlot == data.slot || data.slot.m_InventoryStackSlot)
		    {
			    color = this.m_SelectedColor;
		    }
            //...
        }
        */
        public bool UnlimitedItemStackSize { get; set; }
    }
}
