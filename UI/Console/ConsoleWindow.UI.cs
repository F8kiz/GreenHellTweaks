using UnityEngine;

namespace GHTweaks.UI.Console
{
    internal partial class ConsoleWindow : MonoBehaviour
	{
		const string TEXT_INPUT_NAME = "cw-text-input";

        private readonly ScrollInfo scrollInfo = new ScrollInfo();

        private readonly ScrollInfo commandSuggestionScrollInfo = new ScrollInfo();

        private readonly CommandSuggestion commandSuggestion = new CommandSuggestion();

        private float contentHeight = 0;

		private bool updateCursorPosition = false;



        public static void Destroy()
		{
			if (instance == null)
				return;

			LogWriter.Write($"Destroy {nameof(ConsoleWindow)}");
			var go = GreenHellGame.Instance.GetComponent<ConsoleWindow>();
			if (go != null)
				Destroy(go);

			instance = null;
			lineBuffer.Clear();
		}

        public void Show()
        {
            enabled = true;
            Mod.Instance.PauseGame(true);
        }

		public void Hide()
        {
            enabled = false;
            Mod.Instance.PauseGame(false);
        }

		public void Toggle()
		{
			if (enabled)
				Hide();
			else
				Show();

			LogWriter.Write($"Toggle console: {enabled}");
		}

		//private void Awake()
		//{

		//}

		private void OnGUI()
		{
			if (!enabled)
				return;

            RenderConsole();
            commandSuggestion.DrawCommandSuggestion(strUserInput);

            if (Event.current.type == EventType.KeyUp)
			{
				var keyCode = Event.current.keyCode;
				if (keyCode == KeyCode.Return || keyCode == KeyCode.KeypadEnter)
                {
					if (commandSuggestion.IsVisible && commandSuggestion.SuggestionIndex > -1)
					{
                        SetUserInput(commandSuggestion.GetSelectedSuggestion());
                        return;
					}

                    ProcessInput(strUserInput.Trim());
					strUserInput = string.Empty;
				}
				else if (keyCode == KeyCode.UpArrow && commandSuggestion.IsVisible)
				{
                    commandSuggestion.SelectPreviousSuggestion();
                }
				else if (keyCode == KeyCode.DownArrow && commandSuggestion.IsVisible)
				{
                    commandSuggestion.SelectNextSuggestion();
                }
				else if (keyCode == KeyCode.PageUp)
				{
					var cmd = GetPreviousExecutedCommand();
					if (cmd != null)
						SetUserInput(cmd.RawInput);
				}
				else if (keyCode == KeyCode.PageDown) 
				{
                    var cmd = GetNextExecutedCommand();
                    if (cmd != null)
                        SetUserInput(cmd.RawInput);
                }
				else if (keyCode == KeyCode.F8)
				{
					Hide();
				}
			}
		}

		private void RenderConsole()
		{
			GUI.BeginGroup(Style.RectValues.Window, "GreenHell Console", Style.ConsoleWindow);
			GUILayout.BeginArea(Style.RectValues.ClientArea);

			DrawTextInput();
			DrawTextArea();
			DrawLineBufferInfo();

			GUILayout.EndArea();

            GUI.backgroundColor = Color.red;
			if (GUI.Button(Style.RectValues.CloseButton, "X"))
			{
				Hide();
				return;
			}

			GUI.EndGroup();
			GUI.FocusControl(TEXT_INPUT_NAME);
        }

		private void DrawTextInput()
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			GUI.SetNextControlName(TEXT_INPUT_NAME);
			strUserInput = GUILayout.TextField(strUserInput, GUILayout.MinWidth(Style.RectValues.TextInput.width));

			if (updateCursorPosition)
			{
				GUI.FocusControl(TEXT_INPUT_NAME);
				var editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
				if (editor != null)
				{
					editor.cursorIndex = strUserInput.Length;
					editor.selectIndex = editor.cursorIndex;
					updateCursorPosition = false;
				}
			}

            GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}

		private void DrawTextArea()
		{
            GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			using (var scrollScope = new GUILayout.ScrollViewScope(scrollInfo.ScrollPosition, GUILayout.Width(Style.RectValues.ScrollView.width)))
			{
				scrollInfo.ScrollPosition = scrollScope.scrollPosition;
                GUILayout.BeginHorizontal();

				var lines = string.Join("\n", lineBuffer);
                GUILayout.TextArea(lines, new GUIStyle() { richText = true });

				this.contentHeight = new GUISkin().textArea.CalcHeight(new GUIContent(lines), Style.RectValues.ScrollViewContent.width);

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

            // 432 / 109 = 3.963302752293578
            //this.contentHeight = contentHeight;
        }

		private void DrawLineBufferInfo()
		{
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.Label($"<color={Style.TextColor.HighLight}>LineBuffer: {lineBuffer.Count}/{lineBuffer.BufferSize}, ContentHeight: {contentHeight}, ScrollPosition: {scrollInfo.ScrollPosition.y:0}</color>");

            GUILayout.EndHorizontal();
        }


		private void SetUserInput(string newInput)
		{
			if (string.IsNullOrEmpty(newInput))
				return;

            strUserInput = newInput;
			updateCursorPosition = true;
        }
	}
}
