using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace GHTweaks.UI.Console
{
    internal class ScrollInfo
    {
        public event EventHandler OnScroll;

        public float PositionY 
        { 
            get => ScrollPosition.y;
            set => SetBackingfield(ref scrollPosition.y, value);
        }

        public Vector2 ScrollPosition { 
            get => scrollPosition;
            set => SetBackingfield(ref scrollPosition.y, value.y);
        }
        private Vector2 scrollPosition = Vector2.zero;

        public Vector2 LastScrollPosition { get; private set; } = Vector2.zero;

        public DateTime LastUpdate { get; private set; } = default;

        public DateTime LastScroll { get; private set; } = default;


        public void LineDown(int count = 1)
        {
            if (count < 0)
                return;

            LogWriter.Write($"Current PositionY: {PositionY}");

            PositionY += (ConsoleLine.LINE_HEIGHT * count);

            LogWriter.Write($"New PositionY: {PositionY}");
        }

        public void LineUp(int count=1)
        {
            if (count < 0)
                return;

            LogWriter.Write($"Current PositionY: {PositionY}");

            //PositionY = (ConsoleLine.LINE_HEIGHT * count);
            PositionY = Math.Max(0, PositionY - (ConsoleLine.LINE_HEIGHT * count));
            LogWriter.Write($"New PositionY: {PositionY}");
        }

        public void ScrollToTop() => ScrollPosition = Vector2.zero;
        

        public bool WasScrolledSince(int seconds = 1)
        {
            if (seconds < 1 || LastScroll == default)
                return false;

            return DateTime.Now.Subtract(LastScroll).Seconds < seconds;
        }


        private void SetBackingfield(ref float backingfield, float newValue, [CallerMemberName] string propertyName="")
        {
            if (backingfield == newValue)
                return;

            backingfield = newValue;
            LastUpdate = DateTime.Now;
            if (backingfield.IsLowerOrGreaterThan(ConsoleLine.LINE_HEIGHT))
            {
                LastScroll = DateTime.Now;
                OnScroll?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
