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
            var menu = new ConsoleMenu(args, 0).Add("Enter a name", () => Entername())
                .Add("Show saved name", () => ReadNameFromFile())
                .Add("Calculator", () => submenu.Show())
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => { config.Selector = " ==> "; });

            // Renders the menu 
            menu.Show();
        }

        // Reads the json file and converts it to a User class
        static void ReadNameFromFile()
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
        static void WriteToFile(string json)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            File.WriteAllText(path + "json.txt", json);
        }

        //Shows the screen for entering the first and last name
        static void Entername()
        {
            Console.Clear();
            Console.WriteLine("Enter a firstname");
            var fname = Console.ReadLine();
            Console.WriteLine("And enter a lastname");
            var lname = Console.ReadLine();

            var user = new User {Fname = fname, Lname = lname};

            var json = JsonConvert.SerializeObject(user);
            WriteToFile(json);
            Console.WriteLine(user.Fname + " " + user.Lname + ": is saved");
            Console.ReadKey();
        }

        // Shows the screen for entering two numbers
       static void Calculator(string method)
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
    }
}