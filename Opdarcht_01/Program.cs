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
            var submenu = new ConsoleMenu(args, 1).Add("Return", ConsoleMenu.Close)
                .Add("+", () => Calculator("+"))
                .Add("-", () => Calculator("-"))
                .Add("*", () => Calculator("*"))
                .Add("/", () => Calculator("/"))
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> "; });

            // Creates a menu from the ConsoleMenu class so it's nice and pretty
            var menu = new ConsoleMenu(args, 0).Add("Enter a name", Entername)
                .Add("Show saved name", ReadNameFromFile)
                .Add("Calculator", () => submenu.Show())
                .Add("Calender", Calender)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> "; });

            // Renders the menu 
            menu.Show();
        }

        // Reads the json file and converts it to a User class
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

        // Writes data to file
        private static void WriteNameToFile(string json)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            File.WriteAllText(path + "json.txt", json);
        }

        //Shows the screen for entering the first and last name
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

        // Shows the screen for entering two numbers
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