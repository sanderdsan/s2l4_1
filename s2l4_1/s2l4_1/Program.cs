using Newtonsoft.Json;
using System;
using System.IO;

namespace s2l4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter star name:");
            string SName = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Enter '1' to add a planet, or '0' to leave:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Good_Ending();
                        break;
                    case "0":
                        Console.WriteLine("Leaving, bye bye:)");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine($"The star name is: {Star.Id(SName)}");
                Console.WriteLine($"There are {CountPlanets("planets.json")} planets in our star system.");
                Console.WriteLine($"There are {Satelite.satenum} satelites in our star system.");
            }
        }
        static void Good_Ending()
        {
            Console.WriteLine("Enter the name of the planet:");
            string planetName = Console.ReadLine();
            Planet[] planets = new Planet[1];
            planets[0] = new Planet(planetName);
            SaveToJson(planets, "planets.json");
            Console.WriteLine($"Planet succesfully added:3");
        }


        static void SaveToJson(Planet[] newPlanets, string fileName)
        {
            Planet[] existingPlanets = new Planet[0];
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                existingPlanets = JsonConvert.DeserializeObject<Planet[]>(json);
            }
            Planet[] allPlanets = existingPlanets.Concat(newPlanets).ToArray();
            string jsonPlanets = JsonConvert.SerializeObject(allPlanets);
            File.WriteAllText(fileName, jsonPlanets);
        }
        static int CountPlanets(string fileName)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string json = streamReader.ReadToEnd();
                    Planet[] planets = JsonConvert.DeserializeObject<Planet[]>(json);
                    int count = planets.Length;
                    return count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while counting planets: {e.Message}");
                return 0;
            }
        }
    }

    class Star
    {
        public static string SName { get; set; }
        public Star(string _sname)
        {
            SName = _sname;
        }
        public static string Id(string SName)
        {
            string straname = SName;
            Random random = new Random();
            int number = random.Next(1000, 9999);
            string lastly = straname + number.ToString();
            return lastly;
        }
    }


    class Planet
    {
        public static Planet[] planetList = new Planet[0];
        public string Name { get; set; }
        public Planet(string _name)
        {
            Name = _name;
            Array.Resize(ref planetList, planetList.Length + 1);
            planetList[planetList.Length - 1] = this;
        }
    }

    class Satelite
    {
        public static int satenum = 328;
    }
}
