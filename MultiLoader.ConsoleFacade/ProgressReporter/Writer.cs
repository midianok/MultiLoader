using System;

namespace MultiLoader.ConsoleFacade.ProgressReporter
{
    // see if I can get rid of all usages of this class
    // require all usages to go through Console
    // will dramatically simplify a new user learning about what 
    // this library does!

    internal class Writer : IConsole
    {
        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            var foreground = ForegroundColor;
            try
            {
                ForegroundColor = color;
                WriteLine(format, args);
            }
            finally
            {
                ForegroundColor = foreground;
            }
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void Write(ConsoleColor color, string format, params object[] args)
        {
            var foreground = ForegroundColor;
            try
            {
                ForegroundColor = color;
                Write(format, args);
            }
            finally
            {
                ForegroundColor = foreground;
            }
        }



        public ConsoleState State
        {
            get => new ConsoleState(Console.ForegroundColor,Console.BackgroundColor, Console.CursorTop, Console.CursorLeft, Console.CursorVisible );
            set
            {
                Console.ForegroundColor = value.ForegroundColor;
                Console.BackgroundColor = value.BackgroundColor;
                Console.CursorTop = value.Top;
                Console.CursorLeft = value.Left;
            }
        }

        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;

        public int CursorLeft
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }


        public Colors Colors
        {
            get => new Colors(ForegroundColor, BackgroundColor);
            set
            {
                ForegroundColor = value.Foreground;
                BackgroundColor = value.Background;
            }
        }
        public int CursorTop
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }

        public XY Xy
        {
            get => new XY(Console.CursorLeft, Console.CursorTop);

            set
            {
                Console.CursorLeft = value.X;
                Console.CursorTop = value.Y;
            }
        }
        
        public int Y
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }

        public int X
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft= value;
        }

        /// <summary>
        /// Run command and preserve the state, i.e. restore the console state after running command.
        /// </summary>
        public void DoCommand(IConsole console, Action action)
        {
            if (console == null)
            {
                action();
                return;
            }
            var state = console.State;
            try
            {
                action();
            }
            finally
            {
                console.State = state;
            }
        }

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public ConsoleColor BackgroundColor {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }

        public void PrintAt(int x, int y, string format, params object[] args)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(format, args);            
        }

        public void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);            
        }
        public void PrintAt(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        public void ScrollUp()
        {
            // do nothing?? mmm.
        }

        public void Clear()
        {
            Console.Clear();
        }

        public IConsole BottomHalf(ConsoleColor foreground, ConsoleColor background)
        {
            throw new NotImplementedException();
        }

        public IConsole TopHalf(ConsoleColor foreground, ConsoleColor background)
        {
            throw new NotImplementedException();
        }


        public void PrintAtColor(ConsoleColor foreground, int x, int y, string text, ConsoleColor? background = null)
        {
            DoCommand(this, () =>
            {
                State = new ConsoleState(foreground, background ?? BackgroundColor, y, x, CursorVisible);
                Write(text);
            });
        }


        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop,
            char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
        }

    }
}