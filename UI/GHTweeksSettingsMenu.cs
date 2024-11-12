
using GHTweaks.UI.Helper;

using System;

using UnityEngine;

namespace GHTweaks.UI
{
    internal class GHTweaksSettingsMenu : MenuScreen
    {
        public PanelSettings panelSettings;


        public GHTweaksSettingsMenu() : base() 
        {
            Mod.Instance.WriteLog("GHTweaksSettingsMenu.ctr");
        }


        public event EventHandler OnCloseEvent;

        protected override void Awake()
        {
            Mod.Instance.WriteLog("GHTweeksSettingsMenu.Awake");

            if (panelSettings == null)
            {
                Mod.Instance.WriteLog("Instantiate new PanelSettings");

                panelSettings = gameObject.GetComponent<RectTransform>().AddComponentWithEvent<PanelSettings>();

                Mod.Instance.WriteLog($"Has PanelSettings instance: {panelSettings != null}");
            }
        }

        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Mod.Instance.WriteLog("GHTweaksSettingsMenu.Update: Show previous screen");

                OnCloseEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
