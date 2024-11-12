using GHTweaks.UI.Helper;

using System;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace GHTweaks.UI
{
    public class GHTweaksSettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly Button button;

        private bool isMouseOver = false;

        private const int MARGIN_LEFT = 22;

        private const int MARGIN_TOP = 2; 



        public GHTweaksSettingsButton()
        {
            ColorBlock menuButtonColors = MainMenuOptions.FindObjectOfType<Button>().colors;
            button = UIHelper.CreateButton("GHTweaks", this.transform, menuButtonColors);


        }

        void Start()
        {
            var buttonSize = UIHelper.GetButtonSize(button);
            var menuButtons = MainMenuOptions.FindObjectsOfType<Button>();
            var quitPosition = (menuButtons.FirstOrDefault(x => x.name == "CreditsPvE") ?? menuButtons.Last()).transform.position;

            button.transform.position = new Vector3(quitPosition.x + MARGIN_LEFT - (buttonSize.Width / 2), quitPosition.y - buttonSize.Height - MARGIN_TOP);
            button.onClick.AddListener(() => {
                Mod.Instance.WriteLog("GHTweaksSettingsButton: Set GHTweeksSettingsMenu as active screen");

                var manager = MainMenuManager.Get();
                var activeScreen = manager.GetScreen(manager.GetActiveScreen());
                activeScreen.Hide();

                var menu = gameObject.GetComponentInParent<RectTransform>().AddComponentWithEvent<GHTweaksSettingsMenu>();
                menu.Show();

                menu.OnCloseEvent += (s, e) =>
                {
                    Mod.Instance.WriteLog("OnClose event handler");

                    try
                    {
                        Mod.Instance.WriteLog("Destroy menu");
                        menu.Destroy();

                        Mod.Instance.WriteLog("Show MainMenuOptions");
                        manager.SetActiveScreen(typeof(MainMenuOptions), false);

                        activeScreen = manager.GetScreen(manager.GetActiveScreen());
                        Mod.Instance.WriteLog($"Active screen is: {activeScreen?.name ?? "NULL"}");

                        activeScreen.gameObject.SetActive(true);
                        activeScreen.Show();
                    }
                    catch(Exception ex)
                    {
                        Mod.Instance.WriteLog($"GHTweaksSettingsButton: {ex.Message}", LogType.Exception);
                    }
                };
            });
        }

        void Update()
        {
            if (isMouseOver)
                CursorManager.Get().SetCursor(CursorManager.TYPE.MouseOver);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerEnter.transform.parent == button.transform)
            {
                isMouseOver = true;
                UIHelper.ChangeTextColor(button.gameObject, button.colors.highlightedColor);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter.transform.parent == button.transform)
            {
                isMouseOver = false;
                UIHelper.ChangeTextColor(button.gameObject, Color.gray);
            }
        }
    }
}
