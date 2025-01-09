using System.Linq;

using UnityEngine;
using UnityEngine.UI;


namespace GHTweaks.UI.Helper
{
    internal static class UIHelper
    {
        public static Button CreateButton(string text, Transform parent, ColorBlock colors)
        {
            var bgColor = Color.white;
            bgColor.a = 0;
            return CreateButton(text, parent, colors, bgColor);
        }

        public static Button CreateButton(string text, Transform parent, ColorBlock colors, Color backgroundColor)
        {
            var uiResources = new DefaultControls.Resources();
            uiResources.standard = GetSpriteByName(SpriteNames.UISprite);

            var goButton = DefaultControls.CreateButton(uiResources);
            goButton.GetComponent<Image>().color = backgroundColor;

            var btnText = goButton.GetComponentInChildren(typeof(Text)) as Text;
            btnText.text = text;
            btnText.color = colors.normalColor;

            goButton.transform.SetParent(parent, false);
            var button = goButton.GetComponent<Button>();
            button.colors = colors;

            return button;
        }

        public static void ChangeTextColor(GameObject go, Color color)
        {
            var text = go.GetComponentInChildren(typeof(Text)) as Text;
            if (text != null)
                text.color = color;
        }

        public static System.Drawing.SizeF GetButtonSize(Button gameObject)
        {
            var rect = gameObject.GetComponent<RectTransform>();
            if (rect == null)
                return default;

            return new System.Drawing.SizeF(rect.sizeDelta.x, rect.sizeDelta.y);
        }

        public static Sprite GetSpriteByName(string spriteName) 
            => Resources.FindObjectsOfTypeAll<Sprite>().FirstOrDefault(x => x.name == spriteName);
        
        public static Sprite[] GetAllSpriteResources() 
            => Resources.FindObjectsOfTypeAll<Sprite>();
    }
}
