using UnityEngine;
using UnityEngine.UI;

namespace GHTweaks.UI
{
    public class PanelSettings : MonoBehaviour
    {
        void Start() 
        {
            Mod.Instance.WriteLog("PanelSettings.Start");

            DefaultControls.Resources uiResources = new DefaultControls.Resources();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    uiResources.background = sprite;
                    break;
                }
            }

            var goPanel = DefaultControls.CreatePanel(uiResources);
            goPanel.transform.SetParent(transform, false);
        }
    }
}
