
Color Console V1.0
Published simultaneously at Shotgun.Libaray V1.2

http://the-x.cn?from=colorconsole

Example:
	Shotgun.ColorConsole.ColorConsole.WriteLine("$R The foreground color is red");

	Equivalent:
		var old=Console.ForegroundColor;
		Console.ForegroundColor=Console.Red;
		Console.WriteLine("The foreground color is red");
		Console.ForegroundColor=old;

Replacing System.Console with Injection:
	Console.WriteLine("$R sets the foreground color to red, which is invalid before injection!");
	Shotgun.ColorConsole.ColorConsole.Inject();
	Console.WriteLine("$R After injection, it takes effect!Like this!");
	Console.WriteLine("@@G @G sets the background color to green.");

Color Mapping:
	r => ConsoleColor.DarkRed
	R => ConsoleColor.Red
	g => ConsoleColor.DarkGreen
	G => ConsoleColor.Green
	b => ConsoleColor.DarkBlue
	B => ConsoleColor.Blue
	c => ConsoleColor.Cyan
	C => ConsoleColor.DarkCyan
	y => ConsoleColor.DarkYellow
	Y => ConsoleColor.Yellow
	w => ConsoleColor.Gray
	W => ConsoleColor.White
	m => ConsoleColor.DarkMagenta
	M => ConsoleColor.Magenta
	a => ConsoleColor.DarkGray
	A => ConsoleColor.Black

*The lowercase letter stands for Dark Color , e.g. r is DarkRed and R is Red.


Full Demo:
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
