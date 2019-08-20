using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shotgun.ColorConsole
{
    /// <summary>
    /// 支持颜色标签的控制台
    /// </summary>
    public static partial class ColorConsole
    {
        static TextWriter _consoleOut;
        static ColorTextWriter _colorOut;

        static bool _isInjected;
        static ColorConsole()
        {
            _consoleOut = System.Console.Out;
            _colorOut = new ColorTextWriter(_consoleOut);
        }


        /// <summary>
        /// 注入System.Console，以便在Console中使用颜色标记
        /// </summary>
        public static void Inject()
        {
            if (_isInjected) return;
            _isInjected = true;
            System.Console.SetOut(_colorOut);
        }


        /// <summary>
        /// 注入System.Console，以便在Console中使用颜色标记
        /// </summary>
        /// <param name="foregroundColorTag">前景色标识字符</param>
        /// <param name="backgroundColorTag">背景色标识字符</param>
        /// <param name="maxColorDept">颜色区间嵌套级数</param>
        public static void Inject(char foregroundColorTag, char backgroundColorTag, int maxColorDept)
        {
            if (_isInjected) return;
            _isInjected = true;

            System.Console.SetOut(new ColorTextWriter(_consoleOut)
            {
                BackgroundColorTag = backgroundColorTag,
                ForegroundColorTag = foregroundColorTag,
                MaxColorDept = maxColorDept
            });
        }

        /// <summary>
        /// 颜色区间嵌套级数
        /// </summary>
        public static int MaxColorDept { get => _colorOut.MaxColorDept; set => _colorOut.MaxColorDept = value; }

        /// <summary>
        /// 前景色标识字符
        /// </summary>
        public static char ForegroundColorTag { get => _colorOut.ForegroundColorTag; set => _colorOut.ForegroundColorTag = value; }
        /// <summary>
        /// 背景色标识字符
        /// </summary>
        public static char BackgroundColorTag { get => _colorOut.BackgroundColorTag; set => _colorOut.BackgroundColorTag = value; }


        /// <summary>
        /// 还原Console，停止解释在Console的颜色标记
        /// </summary>
        public static void Reset()
        {
            if (!_isInjected) return;
            _isInjected = false;
            System.Console.SetOut(_consoleOut);
        }

    }
}
