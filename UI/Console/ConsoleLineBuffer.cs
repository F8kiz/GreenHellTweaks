using System.Collections.Generic;
using System.Linq;

namespace GHTweaks.UI.Console
{
    internal sealed class ConsoleLineBuffer : List<ConsoleLine>
    {
        public int BufferSize
        {
            get => bufferSize;
            set
            {
                if (value > 0 && bufferSize != value)
                {
                    bufferSize = value;
                }
            }
        }
        private int bufferSize;


        private List<ConsoleLine> lineBuffer = null;


        public ConsoleLineBuffer(int bufferSize=100) => this.bufferSize = bufferSize;


        public new void Add(ConsoleLine line)
        {
            //if (Count >= bufferSize)
            //    RemoveAt(-1);

            //Insert(0, line);

            if (lineBuffer != null)
            {
                lineBuffer.Add(line);
                return;
            }

            if (Count >= bufferSize)
                RemoveAt(0);

            var lines = line.Text.Split('\n');
            if (lines.Length > 1)
            {
                foreach (string s in lines)
                {
                    base.Add(new ConsoleLine(s, line.Color));
                    if (Count >= bufferSize)
                        RemoveAt(0);
                }
            }
            else
            {
                base.Add(line);
            }
        }

        public void Add(string line) => Add(new ConsoleLine(line, Style.TextColor.Default));

        public void Add(string line, string color) => Add(new ConsoleLine(line, color));
        
        public void AddRange(List<ConsoleLine> lines)
        {
            if (lines == null)
                return;

            foreach(ConsoleLine line in lines)
                Add(line);
        }


        public override string ToString() => string.Join("\n", this);
    }
}
