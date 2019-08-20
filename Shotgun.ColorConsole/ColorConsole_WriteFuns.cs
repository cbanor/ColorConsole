using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shotgun.ColorConsole
{
    partial class ColorConsole
    {
        #region 字符类输出方法
        public static void Write(char[] buffer, int index, int count) => _colorOut.Write(buffer, index, count);
        public static void Write(char[] buffer) => _colorOut.Write(buffer);
        public static void Write(string format, object arg0) => _colorOut.Write(format, arg0);
        public static void Write(string format, object arg0, object arg1) => _colorOut.Write(format, arg0, arg1);
        public static void Write(string format, object arg0, object arg1, object arg2) => _colorOut.Write(format, arg0, arg1, arg2);
        public static void Write(string format, params object[] arg) => _colorOut.Write(format, arg);
        public static void Write(object value) => _colorOut.Write(value);
        public static void WriteLine(char[] buffer) => _colorOut.WriteLine(buffer);
        public static void WriteLine(string format, object arg0) => _colorOut.WriteLine(format, arg0);
        public static void WriteLine(string format, object arg0, object arg1) => _colorOut.WriteLine(format, arg0, arg1);
        public static void WriteLine(string format, object arg0, object arg1, object arg2) => _colorOut.WriteLine(format, arg0, arg1, arg2);
        public static void WriteLine(string format, params object[] arg) => _colorOut.WriteLine(format, arg);
        public static void WriteLine(char[] buffer, int index, int count) => _colorOut.WriteLine(buffer, index, count);
        public static void WriteLine(object value) => _colorOut.WriteLine(value);
        public static void Write(string value) => _colorOut.Write(value);
        public static void WriteLine(string value) => _colorOut.WriteLine(value);
        #endregion

        #region ValueType 直接输出
        public static void WriteLine(uint value) => _consoleOut.WriteLine(value);
        public static void WriteLine(decimal value) => _consoleOut.WriteLine(value);
        public static void Write(uint value) => _consoleOut.Write(value);
        public static void Write(ulong value) => _consoleOut.Write(value);
        public static void WriteLine() => _consoleOut.WriteLine();

        public static void WriteLine(bool value) => _consoleOut.WriteLine(value);
        public static void WriteLine(char value) => _consoleOut.WriteLine(value);
        public static void WriteLine(ulong value) => _consoleOut.WriteLine(value);
        public static void WriteLine(double value) => _consoleOut.WriteLine(value);
        public static void WriteLine(int value) => _consoleOut.WriteLine(value);
        public static void WriteLine(long value) => _consoleOut.WriteLine(value);
        public static void WriteLine(float value) => _consoleOut.WriteLine(value);
        public static void Write(float value) => _consoleOut.Write(value);
        public static void Write(bool value) => _consoleOut.Write(value);
        public static void Write(decimal value) => _consoleOut.Write(value);
        public static void Write(char value) => _consoleOut.Write(value);
        public static void Write(double value) => _consoleOut.Write(value);
        public static void Write(int value) => _consoleOut.Write(value);
        public static void Write(long value) => _consoleOut.Write(value);
        #endregion

    }

}
