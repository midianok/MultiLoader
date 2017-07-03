﻿
namespace MultiLoader.ConsoleFacade.ProgressReporter
{
    public enum PbStyle {  SingleLine, DoubleLine }
    public class ProgressBar : IProgressBar
    {

        private readonly IProgressBar _bar;

        public int Y => _bar.Y;
        public string Line1 => _bar.Line1; 
        public string Line2 => _bar.Line2;

        public ProgressBar(int max)                                                 : this(max, null,'#', PbStyle.SingleLine, new Writer()) { }
        public ProgressBar(int max, int textWidth)                                  : this(max, textWidth, '#', PbStyle.SingleLine, new Writer()) { }
        public ProgressBar(int max, int textWidth, char character)                  : this(max, textWidth, character, PbStyle.SingleLine, new Writer()) { }
        public ProgressBar(PbStyle style, int max)                                  : this(max, null, '#', style, new Writer()) { }
        public ProgressBar(PbStyle style, int max, int textWidth)                   : this(max, textWidth, '#', style, new Writer()) { }
        public ProgressBar(PbStyle style, int max, int textWidth, char character)   : this(max, textWidth, character, style, new Writer()) { }

        public ProgressBar(IConsole console, int max)                                                 : this(max, null,'#', PbStyle.SingleLine, console) { }
        public ProgressBar(IConsole console, int max, int textWidth)                                  : this(max, textWidth, '#', PbStyle.SingleLine, console) { }
        public ProgressBar(IConsole console, int max, int textWidth, char character)                  : this(max, textWidth, character, PbStyle.SingleLine, console) { }
        public ProgressBar(IConsole console, PbStyle style, int max)                                  : this(max, null, '#', style, console) { }
        public ProgressBar(IConsole console, PbStyle style, int max, int textWidth)                   : this(max, textWidth, '#', style, console) { }
        public ProgressBar(IConsole console, PbStyle style, int max, int textWidth, char character)   : this(max, textWidth, character, style, console) { }

        private ProgressBar(int max, int? textWidth, char character, PbStyle style, IConsole console)
        {
            switch (style)
            {
                case PbStyle.DoubleLine:
                    _bar = new ProgressBarTwoLine(max, textWidth, character, console);
                    break;
                case PbStyle.SingleLine:
                    _bar = new ProgressBarSlim(max,textWidth,character,console);
                    break;
            }
        }

        public int Max
        {
            get => _bar.Max;
            set => _bar.Max = value;
        }

        public void Refresh(int current, string format, params object[] args)
        {
            _bar.Refresh(current,format, args);
        }

        public void Refresh(int current, string item)
        {
            _bar.Refresh(current,item);
        }

        public void Next(string item)
        {
            _bar.Next(item);
        }

    }
}