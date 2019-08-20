using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shotgun.ColorConsole
{
    class ColorInfo
    {
        /// <summary>
        /// 是否颜色标记
        /// </summary>
        public bool IsColor { get; set; }
        public ConsoleColor Color { get; set; }
        /// <summary>
        /// 标记长度
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// 区间颜色,使用了[]语法
        /// </summary>
        public bool IsRange { get; set; }

        internal static ColorInfo NotColor = new ColorInfo();

    }
}
