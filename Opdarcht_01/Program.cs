using System;
using System.IO;
using System.Reflection;
using ConsoleTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Opdracht_01
{
    internal class Program
    {
        
        private static void Main(string[] args)
        {
            string filePath = @"C:\Users\Tuan\source\repos\Console_Application\color.txt";
            string clr = File.ReadAllText(filePath);
            Type type = typeof(ConsoleColor);

            // shows all the options for the calculator
            var calcMenu = new ConsoleMenu(args, 1)
                .Add("Return", ConsoleMenu.Close)
                .Add("+", () => Calculator("+"))
                .Add("-", () => Calculator("-"))
                .Add("*", () => Calculator("*"))
                .Add("/", () => Calculator("/"))
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> ";
                    config.WriteHeaderAction = () => Console.WriteLine("Calculator: ");
                    config.ItemForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                });

            // creates the submenu for showing or changing the saved name
            var nameMenu = new ConsoleMenu(args, 1).Add("Return", ConsoleMenu.Close)
                .Add("Create a name", Entername)
                .Add("Show saved name", ReadNameFromFile)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> ";
                    config.ItemForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                });

            var colorMenu = new ConsoleMenu(args, 0).Add("Return", ConsoleMenu.Close)
               .Add("Red", () => ColorChange("Red"))
               .Add("Green", () => ColorChange("Green"))
               .Add("Blue", () => ColorChange("Blue"))
               .Add("Yellow", () => ColorChange("Yellow"))
               .Add("Exit", () => Environment.Exit(0))
               .Configure(config => {
                   config.Selector = " >> ";
                   config.WriteHeaderAction = () => Console.WriteLine("Select the desired color: ");
                   config.ItemForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
               });

            // Creates a menu from the ConsoleMenu class so it's nice and pretty
            var menu = new ConsoleMenu(args, 0)
                .Add("Name options", nameMenu.Show)
                .Add("Calculator", () => calcMenu.Show())
                .Add("Calender", Calender)
                .Add("Change color", () => colorMenu.Show())
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> ";
                    config.WriteHeaderAction = () => Console.WriteLine("Avaiable options: ");
                    config.ItemForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, clr);
                });

            // Renders the menu 
            menu.Show();
        }

        /// <summary>
        /// Funtion to change the preferred console color 
        /// </summary>
        /// <param name="color"></param>
        private static void ColorChange(string color)
        {
            string happy = "";
            Type type = typeof(ConsoleColor);
            if (String.IsNullOrEmpty(happy))
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, color);

                Console.WriteLine("The color that you changed to is: {0}", color);
                Console.Write("Are you happy with the current color? [y/n]: ");
                happy = Console.ReadLine();

                if (happy == "y" || happy == "yes")
                {
                    //Console.Write("I prefer color: ");
                    //string ck = Console.ReadLine();
                    //Type type = typeof(ConsoleColor);
                    WriteColorToFile(color);
                    Console.WriteLine("This syteem needs to restart to see the changes");
                    Console.Write("Press any key: ");
                    Console.ReadKey();
                    var fileName = Assembly.GetExecutingAssembly().Location;
                    System.Diagnostics.Process.Start(fileName);
                    Environment.Exit(0);
                }
            }


        }

        /// <summary>
        /// Instance to write the color code to text file
        /// </summary>
        /// <param name="color"></param>
        private static void WriteColorToFile(string color)
        {
            // color text path
            var path = @"C:\Users\Tuan\source\repos\Console_Application\color.txt";
            File.WriteAllText(path, color);
        }

        /// <summary>
        /// Reads the json file and converts it to a User class
        /// </summary>
        private static void ReadNameFromFile()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            using (var file = File.OpenText(path + "json.txt"))
            using (var reader = new JsonTextReader(file))
            {
                var json = (JObject) JToken.ReadFrom(reader);
                var user = JsonConvert.DeserializeObject<User>(json.ToString());
                Console.WriteLine(user.Fname + " " + user.Lname);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Writes data to file
        /// </summary>
        /// <param name="json"></param>
        private static void WriteNameToFile(string json)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            File.WriteAllText(path + "json.txt", json);
        }

        /// <summary>
        /// Shows the screen for entering the first and last name
        /// </summary>
        private static void Entername()
        {
            Console.Clear();
            Console.WriteLine("Enter a firstname");
            var fname = Console.ReadLine();
            Console.WriteLine("And enter a lastname");
            var lname = Console.ReadLine();

            var user = new User {Fname = fname, Lname = lname};

            var json = JsonConvert.SerializeObject(user);
            WriteNameToFile(json);
            Console.WriteLine(user.Fname + " " + user.Lname + ": is saved");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows the screen for entering two numbers
        /// </summary>
        /// <param name="method"></param>
        private static void Calculator(string method)
        {
            var calculator = new Calculator();
            Console.Clear();
            Console.WriteLine("Enter Value 1:");
            var value1 = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Enter Value 2:");
            var value2 = Convert.ToInt32(Console.ReadLine());

            switch (method)
            {
                case "+":
                    calculator.Plus(value1, value2);
                    break;
                case "-":
                    calculator.Min(value1, value2);
                    break;
                case "*":
                    calculator.Mult(value1, value2);
                    break;
                case "/":
                    calculator.Div(value1, value2);
                    break;
                default:
                    Console.WriteLine("There was an error");
                    break;
            }
        }

        /// <summary>
        /// Function to the the calendar and its selected date
        /// </summary>
        private static void Calender()
        {
            Console.Clear();
            DateTime today = DateTime.Now;
            var done = false;
            var month = today.Month;
            var year = today.Year;
            while (!done)
            {
                DateTime newTime = new DateTime(year, month, today.Day);
                Console.WriteLine(newTime.ToString("Y"));

                var test = DateTime.DaysInMonth(year, month);

                var b = 0;

                for (int i = 1; i <= test; i++)
                {
                    b++;

                    if (month == today.Month)
                    {
                        Console.ForegroundColor = (today.Day == i) ? ConsoleColor.Black : ConsoleColor.White;
                        Console.BackgroundColor = (today.Day == i) ? ConsoleColor.White : ConsoleColor.Black;
                    }

                    var iString = i.ToString();
                    if (iString.Length == 1)
                    {
                        iString = iString.PadLeft(2, '0');
                    }

                    Console.Write($" {iString} ");

                    if (b == 7)
                    {
                        Console.Write("\n");
                        b = 0;
                    }
                }
                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Change the month with the left and right arrow keys");


                var arrow = Console.ReadKey().Key;
                Console.Clear();
                if (arrow == ConsoleKey.LeftArrow)
                {
                    month--;
                    if (month == 0)
                    {
                        month = 12;
                        year--;
                    }
                }
                else if (arrow == ConsoleKey.RightArrow)
                {
                    month++;
                    if (month == 13)
                    {
                        month = 1;
                        year++;
                    }
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}