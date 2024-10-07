using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Spectre.Console;

/*
 * Tower Of Hanoi
 * 
 *~Jakob Smogawetz (Arwezom)
 * 
 * HTL Hallein
 * 
 */

class TowerOfHanoi
{
    static int selectedOption = 1;
    static int numDisks;
    static List<Stack<int>> rods = new List<Stack<int>>();
    static int delay = 1;  // Time delay in milliseconds for each move visualization



    //Draw Interface (Main Menu)
    static void DrawUi()
    {
        //Width & Height of Console
        System.Console.WindowWidth = 122;
        System.Console.WindowHeight = 42;

        //Center Anything
        int numSpaces = (Console.WindowWidth - (Console.WindowWidth / 2)) / 2;
        Console.Write(new string(' ', numSpaces));

        //Select
        string[] options =
        {
                "Set Layers",
                "Start",
                "Exit",
            };

        //Clear & Write Title
        Console.Clear();
        var title = new Table();
        title.Border = TableBorder.Heavy;
        title.Centered();
        title.AddColumn(new TableColumn("[cyan3]  _____                      ___   __   _  _               _ \r\n |_   _|____ __ _____ _ _   / _ \\ / _| | || |__ _ _ _  ___(_)\r\n   | |/ _ \\ V  V / -_) '_| | (_) |  _| | __ / _` | ' \\/ _ \\ |\r\n   |_|\\___/\\_/\\_/\\___|_|    \\___/|_|   |_||_\\__,_|_||_\\___/_|\r\n                                                             [/]").Centered());
        AnsiConsole.Write(title);

        //Write Options
        Console.WriteLine("                                                   ");
        Console.WriteLine("{0}{1} {2}                  ", "                    ", selectedOption == 1 ? "[\x1B[96mx\x1B[97m]" : "[ ]", $"{options[0]}");
        Console.WriteLine("{0}{1} {2}                  ", "                    ", selectedOption == 2 ? "[\x1B[96mx\x1B[97m]" : "[ ]", $"{options[1]}");
        Console.WriteLine("{0}{1} {2}                  ", "                    ", selectedOption == 3 ? "[\x1B[96mx\x1B[97m]" : "[ ]", $"{options[2]}");
        Console.WriteLine("                                                   ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("                                                   ");
        Console.WriteLine("                                       (Use The Arrow Keys And ENTER)");
    }

    //Select Option From Interface (Main Menu)
    static void RedirectToOption()
    {
        //Redirect From Menu to other Void
        switch (selectedOption)
        {
            case 1:
                SetLayers();
                break;

            case 2:
                Start();
                break;

            case 3:
                Exit();
                break;

            default:
                return;
        }
    }

    //Main Programm (Interface)
    static void Main()
    {

    LabelMethodEntry:
        Console.Title = "Tower Of Hanoi";
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

    LabelDrawUi:

        //Set Cursor Position
        Console.SetCursorPosition(0, 3);

        //DrawUi
        DrawUi();

    LabelReadKey:

        //Read Key
        ConsoleKey pressedKey = Console.ReadKey(true).Key;

        //Check which Key pressed
        switch (pressedKey)
        {
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;

            case ConsoleKey.DownArrow:
                selectedOption = (selectedOption + 1 <= 3) ? selectedOption + 1 : selectedOption;
                goto LabelDrawUi;

            case ConsoleKey.UpArrow:
                selectedOption = (selectedOption - 1 >= 1) ? selectedOption - 1 : selectedOption;
                goto LabelDrawUi;

            case ConsoleKey.Enter:
                RedirectToOption();
                break;

            default:
                goto LabelReadKey;
        }

        goto LabelMethodEntry;
    }

    //Set Layers 3-20
    static void SetLayers()
    {
        //Clear & Write Title
        Console.Clear();
        var title = new Table();
        title.Border = TableBorder.Heavy;
        title.Centered();
        title.AddColumn(new TableColumn("[cyan3]  _____                      ___   __   _  _               _ \r\n |_   _|____ __ _____ _ _   / _ \\ / _| | || |__ _ _ _  ___(_)\r\n   | |/ _ \\ V  V / -_) '_| | (_) |  _| | __ / _` | ' \\/ _ \\ |\r\n   |_|\\___/\\_/\\_/\\___|_|    \\___/|_|   |_||_\\__,_|_||_\\___/_|\r\n                                                             [/]").Centered());
        AnsiConsole.Write(title);
        Console.WriteLine();

        //Type In Amount Layers
        string textToEnter = "Please Write How Many Layers You Want:(3-20)";
        Console.WriteLine("                    " + textToEnter);
        Console.WriteLine();
        Console.Write("                    " + "[\u001b[96m>:\u001b[97m]");

        //Prepare Layers
        numDisks = int.Parse(Console.ReadLine());

        //Wait (Loading)
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        var Wait = new Table();
        Wait.Centered();
        Wait.Border = TableBorder.Markdown;
        Wait.AddColumn(new TableColumn("Preparing Layers...").Centered());
        AnsiConsole.Write(Wait);
        Thread.Sleep(2000);
    }
   
    static void SolveHanoi(int n, int fromRod, int toRod, int auxRod)
    {
        if (n == 1)
        {
            MoveDisk(fromRod, toRod);
            return;
        }

        SolveHanoi(n - 1, fromRod, auxRod, toRod);
        MoveDisk(fromRod, toRod);
        SolveHanoi(n - 1, auxRod, toRod, fromRod);
    }

    // Function to move a disk from one rod to another
    static void MoveDisk(int fromRod, int toRod)
    {
        // Check if we can move the disk (if destination rod is empty or larger disk is on top)
        if (rods[fromRod].Count == 0 || (rods[toRod].Count > 0 && rods[toRod].Peek() < rods[fromRod].Peek()))
        {
            Console.WriteLine("Error: Cannot move disk. Larger disks cannot be placed on smaller disks.");
            return; // Exit if the move is not allowed
        }

        // Move the disk
        int disk = rods[fromRod].Pop();
        rods[toRod].Push(disk);

        DrawRods();
        Thread.Sleep(delay); // Pause for visual representation of the move
    }

    // Function to draw the rods and disks
    static void DrawRods()
    {
        //Clear & Write Title
        Console.Clear();
        var title = new Table();
        title.Border = TableBorder.Heavy;
        title.Centered();
        title.AddColumn(new TableColumn("[cyan3]  _____                      ___   __   _  _               _ \r\n |_   _|____ __ _____ _ _   / _ \\ / _| | || |__ _ _ _  ___(_)\r\n   | |/ _ \\ V  V / -_) '_| | (_) |  _| | __ / _` | ' \\/ _ \\ |\r\n   |_|\\___/\\_/\\_/\\___|_|    \\___/|_|   |_||_\\__,_|_||_\\___/_|\r\n                                                             [/]").Centered());
        AnsiConsole.Write(title);
        Console.WriteLine();

        int rodHeight = numDisks + 1;

        // Initialize the display array with empty strings
        string[,] display = new string[rodHeight, 3];

        // Fill the display array with empty spaces
        for (int j = 0; j < rodHeight; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                display[j, i] = " "; // Initialize each cell with an empty string
            }
        }

        // Fill in rod levels with disks or empty space
        for (int i = 0; i < 3; i++)
        {
            var rod = rods[i];
            var rodArr = rod.ToArray();
            int diskIndex = rodArr.Length - 1;

            for (int j = rodHeight - 2; j >= 0; j--)
            {
                if (diskIndex >= 0)
                {
                    display[j, i] = GetDiskString(rodArr[diskIndex]);
                    diskIndex--;
                }
                else
                {
                    display[j, i] = "|"; // Rod placeholder
                }
            }
        }

        // Print the rods and the disks centered in the console
        int consoleWidth = Console.WindowWidth; // Get the width of the console
        int rodWidth = numDisks * 2 + 3; // Width for each rod

        for (int j = 0; j < rodHeight; j++)
        {
            // Create a line for the rods
            string line = "";
            for (int i = 0; i < 3; i++)
            {
                line += display[j, i].PadLeft(rodWidth);
            }
            Console.WriteLine(line.PadLeft((consoleWidth + line.Length) / 2)); // Centering the line
        }

    }

    // Function to get a graphical representation of the disk
    static string GetDiskString(int diskSize)
    {
        return new string('=', diskSize * 2 - 1).PadLeft(numDisks); // Center the disks based on numDisks
    }

    //Go Back to Main
    static void BackToMain()
    {
        Console.ReadKey();
    }

    //Exit Application
    static void Exit()
    {
        Environment.Exit(0);
    }
}
