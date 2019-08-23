using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if COLOR_CONSOLE
namespace Shotgun.ColorConsole
#else
namespace Shotgun.Console
#endif
{
    /// <summary>
    /// 支持颜色标签的控制台
    /// </summary>
    public static partial class ColorConsole
    {
        static TextWriter _consoleOut, _colorOut;
        static ColorTextWriter _colorTW;

        static bool _isInjected;
        static ColorConsole()
        {
            _consoleOut = System.Console.Out;
            _colorTW = new ColorTextWriter(_consoleOut);
            _colorOut = TextWriter.Synchronized(_colorTW);
        }


        /// <summary>
        /// 注入System.Console，以便在Console中使用颜色标记
        /// </summary>
        public static void Inject()
        {
            if (System.Threading.Volatile.Read(ref _isInjected)) return;
            System.Threading.Volatile.Write(ref _isInjected, true);

            if (!InjectByReflection(_colorOut))
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
            if (System.Threading.Volatile.Read(ref _isInjected)) return;
            System.Threading.Volatile.Write(ref _isInjected, true);

            TextWriter tw = new ColorTextWriter(_consoleOut)
            {
                BackgroundColorTag = backgroundColorTag,
                ForegroundColorTag = foregroundColorTag,
                MaxColorDept = maxColorDept
            };
            tw = TextWriter.Synchronized(tw);
            if (!InjectByReflection(tw))
                System.Console.SetOut(tw);
        }

        /// <summary>
        /// 用反射方式，直接注入
        /// </summary>
        /// <returns></returns>
        private static bool InjectByReflection(TextWriter tw)
        {
            var type = typeof(Console);
            var filed = type.GetField("s_out",  // net core 2.0
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (filed == null)
            {
                filed = type.GetField("_out", //Framework 4.5
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            }
            if (filed == null)
                return false;
            filed.SetValue(null, tw);
            return true;
        }

        /// <summary>
        /// 颜色区间嵌套级数
        /// </summary>
        public static int MaxColorDept { get => _colorTW.MaxColorDept; set => _colorTW.MaxColorDept = value; }

        /// <summary>
        /// 前景色标识字符
        /// </summary>
        public static char ForegroundColorTag { get => _colorTW.ForegroundColorTag; set => _colorTW.ForegroundColorTag = value; }
        /// <summary>
        /// 背景色标识字符
        /// </summary>
        public static char BackgroundColorTag { get => _colorTW.BackgroundColorTag; set => _colorTW.BackgroundColorTag = value; }


        /// <summary>
        /// 还原Console，停止解释在Console的颜色标记
        /// </summary>
        public static void Reset()
        {
            if (!System.Threading.Volatile.Read(ref _isInjected)) return;
            System.Threading.Volatile.Write(ref _isInjected, false);

            if (!InjectByReflection(_consoleOut))
                System.Console.SetOut(_consoleOut);

        }

    }
}
