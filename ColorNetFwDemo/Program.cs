using Shotgun.ColorConsole;
using System;

namespace ColorNetcoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorConsole.WriteLine("\t\t\t@M[ Hello ]@a[ $MC$Go$Bl$Yo$cr ]@R$G  Console! ");
            ColorConsole.WriteLine();
            char[] tags = new[] {
                'A', 'b', 'g', 'c', 'r', 'm', 'y', 'w',
                'a', 'B', 'G', 'C', 'R', 'M', 'Y', 'W' };

            // Get an array with the values of ConsoleColor enumeration members.
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            // Save the current background and foreground colors.
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            // Display all foreground colors except the one that matches the background.
            Console.WriteLine("All the foreground colors except {0}, the background color:",
                              currentBackground);
            foreach (var color in colors)
            {
                if (color == currentBackground)
                    continue;
                ColorConsole.WriteLine("   {0}{0}{1} => {0}{1}The foreground color is {2}. ",
                        ColorConsole.ForegroundColorTag, tags[(int)color], color.ToString());
            }
            Console.WriteLine();

            // Display each background color except the one that matches the current foreground color.
            Console.WriteLine("All the background colors except {0}, the foreground color:",
                              currentForeground);
            foreach (var color in colors)
            {
                if (color == currentForeground) continue;

                ColorConsole.WriteLine("   {0}{0}{1} => {0}{1}The background color is {2}. ",
                    ColorConsole.BackgroundColorTag, tags[(int)color], color.ToString());

            }
            Console.WriteLine(string.Empty.PadRight(20, '-'));

            Console.WriteLine("$R sets the foreground color to red, which is invalid before injection!");
            ColorConsole.Inject();
            Console.WriteLine("$R After injection, it takes effect!Like this!");
            Console.WriteLine("@@R @R sets the background color to red.");
            Console.ReadKey();
        }
    }
}
