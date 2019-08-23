using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#if COLOR_CONSOLE
namespace Shotgun.ColorConsole
#else
namespace Shotgun.Console
#endif
{
    public class ColorTextWriter : System.IO.TextWriter
    {
        protected TextWriter _tw = null;
        private ConsoleColor _newColor, _curColor, _newBg, _curBg;

        const int C_BG_COLOR_FLAG = 0x8000;

        public ColorTextWriter() : this(System.Console.Out) { }


        public ColorTextWriter(TextWriter _out)
        {
            _tw = _out;
        }


        public override Encoding Encoding => _tw.Encoding;

        public override void Write(char value)
        {
            if (_newColor != _curColor)
                System.Console.ForegroundColor = _curColor = _newColor;
            if (_newBg != _curBg)
                System.Console.BackgroundColor = _curBg = _newBg;
            _tw.Write(value);
        }

        /// <summary>
        /// 颜色区间嵌套级数
        /// </summary>
        public int MaxColorDept { get; set; } = 16;

        /// <summary>
        /// 前景色标识字符
        /// </summary>
        public char ForegroundColorTag { get; set; } = '$';
        /// <summary>
        /// 背景色标识字符
        /// </summary>
        public char BackgroundColorTag { get; set; } = '@';

        public override string NewLine => _tw.NewLine;


        public override void Write(char[] buffer, int index, int count)
        {
            int endIndex = index + count;
            if (buffer == null || count <= 2 || index >= buffer.Length || endIndex > buffer.Length)
            {
                base.Write(buffer, index, count); //throw Exception
                return;
            }

            lock (this)
                ColorWrite(buffer, index, endIndex);

        }


        private void ColorWrite(char[] buffer, int index, int endIndex)
        {
            var defColor = _newColor = _curColor = System.Console.ForegroundColor;
            var defBgColor = _newBg = _curColor = System.Console.BackgroundColor;

            var queue = new int[MaxColorDept];
            var queuePosition = 0;
            var typeMask = 0;
            for (var i = index; i < endIndex; i++)
            {
                var c = buffer[i];
                if (c == ']')
                {
                    queuePosition--;
                    if (queuePosition < 0)
                    {
                        queuePosition = 0;
                        Write(c);
                    }
                    else if (queuePosition < queue.Length)
                    {
                        if ((queue[queuePosition] & C_BG_COLOR_FLAG) == C_BG_COLOR_FLAG)
                            _newBg = (ConsoleColor)(queue[queuePosition] & 0x7fff);
                        else
                            _newColor = (ConsoleColor)queue[queuePosition];
                    }
                    continue;
                }
                if (c == ForegroundColorTag)
                    typeMask = 0;
                else if (c == BackgroundColorTag)
                    typeMask = C_BG_COLOR_FLAG;
                else
                {
                    Write(c);
                    continue;
                }

                var ci = GetColor(buffer, i, endIndex);
                i += ci.Skip;
                if (!ci.IsColor)
                {
                    Write(buffer[i]);
                    continue;
                }
                if (ci.IsRange)
                {
                    if (queuePosition + 1 >= queue.Length)//ColorConsole.MaxColorDept比实际小（越界），只移位，不记录
                        queuePosition++;
                    else
                    {
                        if (typeMask == C_BG_COLOR_FLAG)//在使用连颜色时因使用延后更新机制，不能记录_curColor
                            queue[queuePosition++] = (C_BG_COLOR_FLAG) | (int)_newBg;
                        else
                            queue[queuePosition++] = (int)_newColor;
                    }
                }
                if (typeMask == C_BG_COLOR_FLAG)
                    _newBg = ci.Color;
                else
                    _newColor = ci.Color;
            }

            if (defColor != _curColor)
                System.Console.ForegroundColor = defColor;
            if (defBgColor != _curBg)
                System.Console.BackgroundColor = defBgColor;
        }

        ColorInfo GetColor(char[] buffer, int index, int endIndex)
        {
            var mask = buffer[index];
            if (++index >= endIndex)
                return ColorInfo.NotColor;

            var c1 = buffer[index];
            if (c1 == mask) // =>连续一样表示转义，类似：\\=>\
                return new ColorInfo { Skip = 1 };
            if (c1 == ']') //转义输出
                return new ColorInfo { Skip = 1 };
            var ci = new ColorInfo
            {
                IsColor = true,
                Skip = 1
            };
            switch (c1)
            {
                case 'r': ci.Color = ConsoleColor.DarkRed; break;
                case 'R': ci.Color = ConsoleColor.Red; break;
                case 'g': ci.Color = ConsoleColor.DarkGreen; break;
                case 'G': ci.Color = ConsoleColor.Green; break;
                case 'b': ci.Color = ConsoleColor.DarkBlue; break;
                case 'B': ci.Color = ConsoleColor.Blue; break;
                case 'c': ci.Color = ConsoleColor.Cyan; break;
                case 'C': ci.Color = ConsoleColor.DarkCyan; break;
                case 'y': ci.Color = ConsoleColor.DarkYellow; break;
                case 'Y': ci.Color = ConsoleColor.Yellow; break;
                case 'w': ci.Color = ConsoleColor.Gray; break;// (Dark White)
                case 'W': ci.Color = ConsoleColor.White; break;
                case 'm': ci.Color = ConsoleColor.DarkMagenta; break;
                case 'M': ci.Color = ConsoleColor.Magenta; break;
                case 'a': ci.Color = ConsoleColor.DarkGray; break;
                case 'A': ci.Color = ConsoleColor.Black; break;
                default: return ColorInfo.NotColor;
            }
            if (++index > endIndex)
                return ci;
            c1 = buffer[index];
            if (c1 == ' ')//美化表达式，去掉尾巴上的一个空格
                ci.Skip = 2;
            else if (c1 == '[')
            {
                ci.IsRange = true;
                ci.Skip = 2;
            }
            return ci;

        }

        protected override void Dispose(bool disposing)
        {
            _tw.Dispose();
            base.Dispose(disposing);
        }

    }

}
