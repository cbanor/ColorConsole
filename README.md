# Color Console V1.0
C# Console Color Plugin.
http://the-x.cn?from=colorconsole

##Nuget - Package Manager 
>Install-Package Shotgun.ColorConsole

##Example
```csharp
Shotgun.ColorConsole.ColorConsole.WriteLine("$R The foreground color is red");
```
Equivalent:
```csharp
var old=Console.ForegroundColor;
Console.ForegroundColor=Console.Red;
Console.WriteLine("The foreground color is red");
Console.ForegroundColor=old;
```

##Replacing System.Console with Injection
```csharp
Console.WriteLine("$R sets the foreground color to red, which is invalid before injection!");
Shotgun.ColorConsole.ColorConsole.Inject();
Console.WriteLine("$R After injection, it takes effect!Like this!");
Console.WriteLine("@@G @G sets the background color to green.");
```

##Color Mapping
```csharp
r => ConsoleColor.DarkRed
R => ConsoleColor.Red
g => ConsoleColor.DarkGreen
G => ConsoleColor.Green
b => ConsoleColor.DarkBlue
B => ConsoleColor.Blue
c => ConsoleColor.DarkCyan
C => ConsoleColor.Cyan
y => ConsoleColor.DarkYellow
Y => ConsoleColor.Yellow
m => ConsoleColor.DarkMagenta
M => ConsoleColor.Magenta
w => ConsoleColor.Gray (Dark White)
W => ConsoleColor.White
a => ConsoleColor.DarkGray
A => ConsoleColor.Black
```
*The lowercase letter stands for Dark Color , e.g. r is DarkRed and R is Red.
