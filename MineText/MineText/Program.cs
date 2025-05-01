using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;

namespace MineText
{
    internal class Program
    {
        static long x, y;
        static object username;
        static long prc;
        static long HEALTH = 20;
        static int b;
        static List<object> inventory = new List<object>();
        static int choose, item_num;
        static List<object> instructions = new List<object>();
        static Random health_decrease = new Random();

        static long current_tree_num = 0;

        static long current_diamond_num = 0;
        static long current_cobblestone_num = 0;
        static long current_coal_num = 0;
        static long current_gold_num = 0;
        static long current_emerald_num = 0;
        static long current_lapis_num = 0;
        static long current_iron_num = 0;
        static long current_redstone_num = 0;

        static long current_feather_num = 0;
        static long current_raw_chicken_num = 0;
        static long current_wool_num = 0;
        static long current_raw_mutton_num = 0;
        static long current_raw_beef_num = 0;
        static long current_leather_num = 0;
        static long current_raw_pork_num = 0;

        static long current_chicken_num = 0;
        static long current_mutton_num = 0;
        static long current_beef_num = 0;
        static long current_pork_num = 0;

        static long current_plank_num = 0;
        static long current_crafting_table_num = 0;
        static long current_stick_num = 0;

        static long current_bed_num = 0;
        static long current_furnace_num = 0;

        static long current_wooden_sword_num = 0;
        static long current_wooden_pickaxe_num = 0;
        static long current_wooden_axe_num = 0;

        static long current_iron_sword_num = 0;
        static long current_iron_pickaxe_num = 0;
        static long current_iron_axe_num = 0;

        static long current_diamond_sword_num = 0;
        static long current_diamond_pickaxe_num = 0;
        static long current_diamond_axe_num = 0;

        static long current_gold_sword_num = 0;
        static long current_gold_pickaxe_num = 0;
        static long current_gold_axe_num = 0;

        static long current_stone_sword_num = 0;
        static long current_stone_pickaxe_num = 0;
        static long current_stone_axe_num = 0;

        static SoundPlayer main_menu = new SoundPlayer("C:\\Users\\Matija Prpić\\source\\repos\\MineText\\main_menu.wav");
        static Random item_num_c = new Random();
        static Random y_pos = new Random();
        static Random percent = new Random();
        static string connectionString = "Data Source=users.db;Version=3;";

        static void Start_Game()
        {

            main_menu.Play();

            Console.Clear();
            instructions.Add("Cut wood(100)");
            instructions.Add("To go mining(101) P.S. You can lose up to 50% of your health");
            instructions.Add("Kill animals for food(102)");
            instructions.Add("Craft something(103)");


            int i;
            Random x_pos = new Random();
            x = x_pos.Next(-9999, 9999);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MineText");
            Console.WriteLine("\n1 - Start"); ;
            Console.Write("Enter number what do you want to do: ");
            i = int.Parse(Console.ReadLine());


            switch (i)
            {
                case 1:
                    Loading();
                    break;

                default:
                    Start_Game();
                    break;


            }
            Console.ReadKey();
        }
        static string filePath = "savegame.json";

        class GameData
        {
            public object PlayerName { get; set; }
            public int Score { get; set; }
            public long X { get; set; }
            public long Y { get; set; }
            public long health { get; set; }

            public long CURRENT_TREE_NUM { get; set; }
            public long CURRENT_DIAMOND_NUM { get; set; }
            public long CURRENT_COBBLESTONE_NUM { get; set; }
            public long CURRENT_COAL_NUM { get; set; }
            public long CURRENT_GOLD_NUM { get; set; }
            public long CURRENT_EMERALD_NUM { get; set; }
            public long CURRENT_LAPIS_NUM { get; set; }
            public long CURRENT_IRON_NUM { get; set; }
            public long CURRENT_REDSTONE_NUM { get; set; }

            public long CURRENT_FEATHER_NUM { get; set; }
            public long CURRENT_RAW_CHICKEN_NUM { get; set; }
            public long CURRENT_WOOL_NUM { get; set; }
            public long CURRENT_RAW_MUTTON_NUM { get; set; }
            public long CURRENT_RAW_BEEF_NUM { get; set; }
            public long CURRENT_LEATHER_NUM { get; set; }
            public long CURRENT_RAW_PORK_NUM { get; set; }

            public long CURRENT_PLANK_NUM { get; set; }
            public long CURRENT_CRAFTING_TABLE_NUM { get; set; }
            public long CURRENT_STICK_NUM { get; set; }

            public long CURRENT_BED_NUM { get; set; }

            public long CURRENT_WOODEN_SWORD_NUM { get; set; }
            public long CURRENT_WOODEN_PICKAXE_NUM { get; set; }
            public long CURRENT_WOODEN_AXE_NUM { get; set; }

            public long CURRENT_IRON_SWORD_NUM { get; set; }
            public long CURRENT_IRON_PICKAXE_NUM { get; set; }
            public long CURRENT_IRON_AXE_NUM { get; set; }

            public long CURRENT_DIAMOND_SWORD_NUM { get; set; }
            public long CURRENT_DIAMOND_PICKAXE_NUM { get; set; }
            public long CURRENT_DIAMOND_AXE_NUM { get; set; }

            public long CURRENT_GOLD_SWORD_NUM { get; set; }
            public long CURRENT_GOLD_PICKAXE_NUM { get; set; }
            public long CURRENT_GOLD_AXE_NUM { get; set; }

            public long CURRENT_STONE_SWORD_NUM { get; set; }
            public long CURRENT_STONE_PICKAXE_NUM { get; set; }
            public long CURRENT_STONE_AXE_NUM { get; set; }
        }

        static void Save_Data()
        {
            GameData gameData;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                gameData = JsonConvert.DeserializeObject<GameData>(json);
                Console.WriteLine($"Welcome back, {gameData.PlayerName}! Score: {gameData.Score}, Position: ({gameData.X}, {gameData.Y})");
            }
            else
            {
                gameData = new GameData();
                gameData.PlayerName = username;
                gameData.health = HEALTH;
                gameData.X = x;
                gameData.Y = y;
                gameData.CURRENT_TREE_NUM = current_tree_num;
                gameData.CURRENT_DIAMOND_NUM = current_diamond_num;
                gameData.CURRENT_COBBLESTONE_NUM = current_cobblestone_num;
                gameData.CURRENT_COAL_NUM = current_coal_num;
                gameData.CURRENT_GOLD_NUM = current_gold_num;
                gameData.CURRENT_EMERALD_NUM = current_emerald_num;
                gameData.CURRENT_LAPIS_NUM = current_lapis_num;
                gameData.CURRENT_IRON_NUM = current_iron_num;
                gameData.CURRENT_REDSTONE_NUM = current_redstone_num;

                gameData.CURRENT_FEATHER_NUM = current_feather_num;
                gameData.CURRENT_RAW_CHICKEN_NUM = current_raw_chicken_num;
                gameData.CURRENT_WOOL_NUM = current_wool_num;
                gameData.CURRENT_RAW_MUTTON_NUM = current_raw_mutton_num;
                gameData.CURRENT_RAW_BEEF_NUM = current_raw_beef_num;
                gameData.CURRENT_LEATHER_NUM = current_leather_num;
                gameData.CURRENT_RAW_PORK_NUM = current_raw_pork_num;

                gameData.CURRENT_PLANK_NUM = current_plank_num;
                gameData.CURRENT_CRAFTING_TABLE_NUM = current_crafting_table_num;
                gameData.CURRENT_STICK_NUM = current_stick_num;

                gameData.CURRENT_BED_NUM = current_bed_num;

                gameData.CURRENT_WOODEN_SWORD_NUM = current_wooden_sword_num;
                gameData.CURRENT_WOODEN_PICKAXE_NUM = current_wooden_pickaxe_num;
                gameData.CURRENT_WOODEN_AXE_NUM = current_wooden_axe_num;

                gameData.CURRENT_IRON_SWORD_NUM = current_iron_sword_num;
                gameData.CURRENT_IRON_PICKAXE_NUM = current_iron_pickaxe_num;
                gameData.CURRENT_IRON_AXE_NUM = current_iron_axe_num;

                gameData.CURRENT_DIAMOND_SWORD_NUM = current_diamond_sword_num;
                gameData.CURRENT_DIAMOND_PICKAXE_NUM = current_diamond_pickaxe_num;
                gameData.CURRENT_DIAMOND_AXE_NUM = current_diamond_axe_num;

                gameData.CURRENT_GOLD_SWORD_NUM = current_gold_sword_num;
                gameData.CURRENT_GOLD_PICKAXE_NUM = current_gold_pickaxe_num;
                gameData.CURRENT_GOLD_AXE_NUM = current_gold_axe_num;

                gameData.CURRENT_STONE_SWORD_NUM = current_stone_sword_num;
                gameData.CURRENT_STONE_PICKAXE_NUM = current_stone_pickaxe_num;
                gameData.CURRENT_STONE_AXE_NUM = current_stone_axe_num;

            }



            // Save updated data
            string newJson = JsonConvert.SerializeObject(gameData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, newJson);

            Console.WriteLine("Game saved! Restart the console to see saved data.");

        }
        static string userFile = "users.json";
        static string worldDir = "worlds";
        static Dictionary<string, string> users = new Dictionary<string, string>();
        static string currentUser = null;
        static Dictionary<string, List<string>> currentUserWorlds = new Dictionary<string, List<string>>();

        static void Main()
        {
            Console.Clear();
            main_menu.Play();
            Console.WriteLine("MineText");
            Console.WriteLine("\n1 - Start");
            int i = int.Parse(Console.ReadLine());

            switch(i)
            {
                case 1:
                    Loading();
                    break;

                default:
                    Main();
                    break;
            }
        }

        static void Loading()
        {
            Random load_time = new Random();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Vranela");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Games\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Loading...");
            y = y_pos.Next(-9999, 9999);
            Thread.Sleep(load_time.Next(1000, 5000));
            Start();
            Console.ReadKey();
        }
        
        static void Start()
        {
            main_menu.Stop();
            Console.Clear();

            Console.WriteLine(username);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Health:{HEALTH}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nx:{x} y:{y}");


            while (true)
            {
                Keyboard_Open();
            }


        }
        static void Keyboard_Open()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.W)
                {
                    Console.Clear();
                    if (y < 10000)
                    {
                        y++;
                    }

                    if (x == 1982 && y == 2023)
                        Console.WriteLine("Remember that you are always in my heart");

                    Console.WriteLine(username);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Health:{HEALTH}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nx:{x} y:{y}");

                }
                if (key.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    if (y > -10000)
                    {
                        y--;
                    }

                    if (x == 1982 && y == 2023)
                        Console.WriteLine("Remember that you are always in my heart");


                    Console.WriteLine(username);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Health:{HEALTH}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nx:{x} y:{y}");

                }
                if (key.Key == ConsoleKey.D)
                {
                    Console.Clear();
                    if (x < 10000)
                    {
                        x++;
                    }

                    if (x == 1982 && y == 2023)
                        Console.WriteLine("Remember that you are always in my heart");


                    Console.WriteLine(username);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Health:{HEALTH}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nx:{x} y:{y}");

                }
                if (key.Key == ConsoleKey.A)
                {
                    Console.Clear();
                    if (x > -10000)
                    {
                        x--;
                        if (x == 1982 && y == 2023)
                            Console.WriteLine("Remember that you are always in my heart");


                        Console.WriteLine(username);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Health:{HEALTH}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\nx:{x} y:{y}");
                    }


                }
                if (key.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    foreach (object inventor in inventory)
                    {
                        Console.WriteLine(inventor);
                    }
                }
                if (key.Key == ConsoleKey.M)
                {
                    Instructions();
                }
            }
        }
        static void Instructions()
        {
            Console.Clear();
            if (current_crafting_table_num > 0)
            {
                instructions.Remove("Craft something on crafting table(104)");
                instructions.Add("Craft something on crafting table(104)");
            }
            if (current_furnace_num > 0)
            {
                instructions.Remove("Cook something in furnace(105)");
                instructions.Add("Cook something in furnace(105)");
            }
            foreach (object instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
            Console.Write("Enter number what do you want to do(type 1 to leave): ");
            int i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    Start();
                    break;

                case 100:
                    Chopping_Trees();
                    break;

                case 101:
                    Go_Mining();
                    break;

                case 102:
                    Killing_Animals();
                    break;

                case 103:
                    Simple_Crafts();
                    break;

                case 104:
                    if (current_crafting_table_num > 0)
                    {
                        Advanced_Crafts();
                    }
                    else
                    {
                        Instructions();
                    }
                    break;

                case 105:
                    if (current_furnace_num > 0)
                    {
                        Cook_Something();
                    }
                    else
                    {
                        Instructions();
                    }
                    break;

                default:
                    Instructions();
                    break;

            }
        }
        static void Chopping_Trees()
        {
            Console.Clear();

            Console.WriteLine("1 - Use Hand");
            if (current_wooden_axe_num > 0)
            {
                Console.WriteLine("2 - Use Wooden Axe");
            }
            if (current_stone_axe_num > 0)
            {
                Console.WriteLine("3 - Use Stone Axe");
            }
            if (current_iron_axe_num > 0)
            {
                Console.WriteLine("4 - Use Iron Axe");
            }
            if (current_gold_axe_num > 0)
            {
                Console.WriteLine("5 - Use Gold Axe");
            }
            if (current_diamond_axe_num > 0)
            {
                Console.WriteLine("5 - Use Diamond Axe");
            }
            int i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    item_num = item_num_c.Next(1, 5);
                    inventory.Remove($"Tree({current_tree_num})");
                    current_tree_num = current_tree_num + item_num;
                    inventory.Add($"Tree({current_tree_num})");
                    break;

                case 2:
                    if (current_wooden_axe_num > 0)
                    {
                        item_num = item_num_c.Next(5, 6);
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num = current_tree_num + item_num;
                        inventory.Add($"Tree({current_tree_num})");
                        current_wooden_axe_num--;
                        if (current_wooden_axe_num == 0)
                        {
                            inventory.Remove($"Wooden Axe(0)");
                        }
                    }
                    break;

                case 3:
                    if (current_stone_axe_num > 0)
                    {
                        item_num = item_num_c.Next(6, 8);
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num = current_tree_num + item_num;
                        inventory.Add($"Tree({current_tree_num})");
                        current_stone_axe_num--;
                        if (current_stone_axe_num == 0)
                        {
                            inventory.Remove($"Stone Axe(0)");
                        }
                    }
                    break;

                case 4:
                    if (current_iron_axe_num > 0)
                    {
                        item_num = item_num_c.Next(8, 11);
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num = current_tree_num + item_num;
                        inventory.Add($"Tree({current_tree_num})");
                        current_iron_axe_num--;
                        if (current_iron_axe_num == 0)
                        {
                            inventory.Remove($"Iron Axe(0)");
                        }
                    }
                    break;

                case 5:
                    if (current_gold_axe_num > 0)
                    {
                        item_num = item_num_c.Next(1, 11);
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num = current_tree_num + item_num;
                        inventory.Add($"Tree({current_tree_num})");
                        current_gold_axe_num--;
                        if (current_gold_axe_num == 0)
                        {
                            inventory.Remove($"Gold Axe(0)");
                        }
                    }
                    break;

                case 6:
                    if (current_diamond_axe_num > 0)
                    {
                        item_num = item_num_c.Next(11, 25);
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num = current_tree_num + item_num;
                        inventory.Add($"Tree({current_tree_num})");
                        current_diamond_axe_num--;
                        if (current_diamond_axe_num == 0)
                        {
                            inventory.Remove($"Diamond Axe(0)");
                        }
                    }
                    break;
            }
        }
        static void Go_Mining()
        {
            if (!(current_wooden_pickaxe_num == 0 || current_stone_pickaxe_num == 0 || current_iron_pickaxe_num == 0 || current_gold_pickaxe_num == 0 || current_diamond_pickaxe_num == 0))
            {


                Console.Clear();
                if (current_wooden_pickaxe_num > 0)
                {
                    Console.WriteLine("1 - Use Wooden Pickaxe");
                }
                if (current_stone_pickaxe_num > 0)
                {
                    Console.WriteLine("2 - Use Stone Pickaxe");
                }
                if (current_iron_pickaxe_num > 0)
                {
                    Console.WriteLine("3 - Use Iron Pickaxe");
                }
                if (current_gold_pickaxe_num > 0)
                {
                    Console.WriteLine("4 - Use Gold Pickaxe");
                }
                if (current_diamond_pickaxe_num > 0)
                {
                    Console.WriteLine("5 - Use Diamond Pickaxe");
                }

                int i = int.Parse(Console.ReadLine());
                if (i == 100)
                {
                    Start();
                }
                prc = percent.Next(1, 100);
                switch (i)
                {
                    case 1:
                        if (current_wooden_pickaxe_num > 0)
                        {
                            item_num = item_num_c.Next(10, 30);
                            inventory.Remove($"Cobblestone({current_cobblestone_num})");
                            current_cobblestone_num = current_cobblestone_num + item_num;
                            inventory.Add($"Cobblestone({current_cobblestone_num})");


                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Coal({current_coal_num})");
                            current_coal_num = current_coal_num + item_num;
                            inventory.Add($"Coal({current_coal_num})");

                            current_wooden_pickaxe_num--;
                            if (current_wooden_pickaxe_num == 0)
                            {
                                inventory.Remove("Wooden Pickaxe(0)");
                            }
                        }
                        break;

                    case 2:
                        if (current_stone_axe_num > 0)
                        {
                            item_num = item_num_c.Next(15, 35);
                            inventory.Remove($"Cobblestone({current_cobblestone_num})");
                            current_cobblestone_num = current_cobblestone_num + item_num;
                            inventory.Add($"Cobblestone({current_cobblestone_num})");

                            item_num = item_num_c.Next(1, 5);
                            inventory.Remove($"Iron ingot({current_iron_num})");
                            current_iron_num = current_iron_num + item_num;
                            inventory.Add($"Iron Ingot({current_iron_num})");

                            if (prc > 61 && prc < 91)
                            {
                                item_num = item_num_c.Next(1, 5);
                                inventory.Remove($"Lapis Lazuli({current_lapis_num})");
                                current_lapis_num = current_lapis_num + item_num;
                                inventory.Add($"Lapis Lazuli({current_lapis_num})");
                            }
                            item_num = item_num_c.Next(10, 20);
                            inventory.Remove($"Coal({current_coal_num})");
                            current_coal_num = current_coal_num + item_num;
                            inventory.Add($"Coal({current_coal_num})");

                            current_stone_pickaxe_num--;
                            if (current_stone_pickaxe_num == 0)
                            {
                                inventory.Remove("Stone Pickaxe(0)");
                            }
                        }
                        break;

                    case 3:
                        if (current_iron_pickaxe_num > 0)
                        {

                            item_num = item_num_c.Next(1, 5);
                            inventory.Remove($"Diamond({current_diamond_num})");
                            current_diamond_num = current_diamond_num + item_num;
                            inventory.Add($"Diamond({current_diamond_num})");

                            item_num = item_num_c.Next(20, 40);
                            inventory.Remove($"Cobblestone({current_cobblestone_num})");
                            current_cobblestone_num = current_cobblestone_num + item_num;
                            inventory.Add($"Cobblestone({current_cobblestone_num})");

                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Redstone({current_redstone_num})");
                            current_redstone_num = current_redstone_num + item_num;
                            inventory.Add($"Redstone({current_redstone_num})");

                            item_num = item_num_c.Next(1, 10);
                            inventory.Remove($"Iron ingot({current_iron_num})");
                            current_iron_num = current_iron_num + item_num;
                            inventory.Add($"Iron Ingot({current_iron_num})");

                            item_num = item_num_c.Next(1, 13);
                            inventory.Remove($"Gold ingot({current_gold_num})");
                            current_gold_num = current_gold_num + item_num;
                            inventory.Add($"Gold Ingot({current_gold_num})");

                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Lapis Lazuli({current_lapis_num})");
                            current_lapis_num = current_lapis_num + item_num;
                            inventory.Add($"Lapis Lazuli({current_lapis_num})");

                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Coal({current_coal_num})");
                            current_coal_num = current_coal_num + item_num;
                            inventory.Add($"Coal({current_coal_num})");

                            current_iron_pickaxe_num--;
                            if (current_iron_pickaxe_num == 0)
                            {
                                inventory.Remove("Iron Pickaxe(0)");
                            }
                        }
                        break;

                    case 4:
                        if (current_gold_pickaxe_num > 0)
                        {

                            item_num = item_num_c.Next(1, 2);
                            inventory.Remove($"Diamond({current_diamond_num})");
                            current_diamond_num = current_diamond_num + item_num;
                            inventory.Add($"Diamond({current_diamond_num})");

                            item_num = item_num_c.Next(10, 15);
                            inventory.Remove($"Cobblestone({current_cobblestone_num})");
                            current_cobblestone_num = current_cobblestone_num + item_num;
                            inventory.Add($"Cobblestone({current_cobblestone_num})");

                            item_num = item_num_c.Next(5, 10);
                            inventory.Remove($"Redstone({current_redstone_num})");
                            current_redstone_num = current_redstone_num + item_num;
                            inventory.Add($"Redstone({current_redstone_num})");

                            item_num = item_num_c.Next(1, 5);
                            inventory.Remove($"Iron ingot({current_iron_num})");
                            current_iron_num = current_iron_num + item_num;
                            inventory.Add($"Iron Ingot({current_iron_num})");

                            item_num = item_num_c.Next(1, 3);
                            inventory.Remove($"Gold ingot({current_gold_num})");
                            current_gold_num = current_gold_num + item_num;
                            inventory.Add($"Gold Ingot({current_gold_num})");

                            item_num = item_num_c.Next(5, 10);
                            inventory.Remove($"Lapis Lazuli({current_lapis_num})");
                            current_lapis_num = current_lapis_num + item_num;
                            inventory.Add($"Lapis Lazuli({current_lapis_num})");

                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Coal({current_coal_num})");
                            current_coal_num = current_coal_num + item_num;
                            inventory.Add($"Coal({current_coal_num})");

                            current_gold_pickaxe_num--;
                            if (current_gold_pickaxe_num == 0)
                            {
                                inventory.Remove("Gold Pickaxe(0)");
                            }
                        }
                        break;

                    case 5:
                        if (current_diamond_pickaxe_num > 0)
                        {

                            item_num = item_num_c.Next(5, 15);
                            inventory.Remove($"Diamond({current_diamond_num})");
                            current_diamond_num = current_diamond_num + item_num;
                            inventory.Add($"Diamond({current_diamond_num})");

                            item_num = item_num_c.Next(50, 100);
                            inventory.Remove($"Cobblestone({current_cobblestone_num})");
                            current_cobblestone_num = current_cobblestone_num + item_num;
                            inventory.Add($"Cobblestone({current_cobblestone_num})");

                            item_num = item_num_c.Next(55, 105);
                            inventory.Remove($"Redstone({current_redstone_num})");
                            current_redstone_num = current_redstone_num + item_num;
                            inventory.Add($"Redstone({current_redstone_num})");

                            item_num = item_num_c.Next(30, 50);
                            inventory.Remove($"Iron ingot({current_iron_num})");
                            current_iron_num = current_iron_num + item_num;
                            inventory.Add($"Iron Ingot({current_iron_num})");

                            item_num = item_num_c.Next(15, 30);
                            inventory.Remove($"Gold ingot({current_gold_num})");
                            current_gold_num = current_gold_num + item_num;
                            inventory.Add($"Gold Ingot({current_gold_num})");

                            item_num = item_num_c.Next(50, 100);
                            inventory.Remove($"Lapis Lazuli({current_lapis_num})");
                            current_lapis_num = current_lapis_num + item_num;
                            inventory.Add($"Lapis Lazuli({current_lapis_num})");

                            item_num = item_num_c.Next(50, 150);
                            inventory.Remove($"Coal({current_coal_num})");
                            current_coal_num = current_coal_num + item_num;
                            inventory.Add($"Coal({current_coal_num})");


                            current_diamond_pickaxe_num--;
                            if (current_diamond_pickaxe_num == 0)
                            {
                                inventory.Remove("Diamond Pickaxe(0)");
                            }
                        }
                        break;

                }

                if (prc > 90 && prc <= 100)
                {
                    HEALTH = HEALTH - health_decrease.Next(1, 10);
                }
            }
        }
        static void Killing_Animals()
        {
            Console.Clear();
            Console.WriteLine("1 - Use Hand");
            if (current_wooden_sword_num > 0)
            {
                Console.WriteLine("2 - Use Wooden Sword");
            }
            if (current_stone_sword_num > 0)
            {
                Console.WriteLine("3 - Use Stone Sword");
            }
            if (current_iron_sword_num > 0)
            {
                Console.WriteLine("4 - Use Iron Sword");
            }
            if (current_gold_sword_num > 0)
            {
                Console.WriteLine("5 - Use Gold Sword");
            }
            if (current_diamond_sword_num > 0)
            {
                Console.WriteLine("6 - Use Diamond Sword");
            }
            Console.WriteLine("Type 100 to leave");
            int i = int.Parse(Console.ReadLine());
            if (i == 100)
            {
                Start();
            }
            int prc = percent.Next(1, 100);

            switch (i)
            {
                case 1:
                    if (prc > 0 && prc <= 25)
                    {
                        item_num = 1;
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");
                    }
                    if (prc > 25 && prc <= 50)
                    {
                        item_num = 1;
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");
                    }
                    if (prc > 50 && prc <= 75)
                    {
                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");


                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");
                    }
                    if (prc > 75 && prc <= 100)
                    {
                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num = current_iron_num + item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");
                    }
                    break;

                case 2:
                    if (current_wooden_sword_num > 0)
                    {
                        item_num = 2;
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");

                        item_num = 1;
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");

                        item_num = item_num_c.Next(1, 3);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");

                        item_num = item_num_c.Next(1, 3);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");

                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num = current_iron_num + item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");

                        current_wooden_sword_num--;
                        if (current_wooden_sword_num == 0)
                        {
                            inventory.Remove("Wooden Sword(0)");
                        }
                    }
                    break;

                case 3:
                    if (current_stone_sword_num > 0)
                    {

                        item_num = 2;
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");

                        item_num = 1;
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(1, 3);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");

                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");


                        item_num = item_num_c.Next(1, 3);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");

                        item_num = item_num_c.Next(1, 6);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num = current_iron_num + item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");

                        current_stone_sword_num--;
                        if (current_stone_sword_num == 0)
                        {
                            inventory.Remove("Stone Sword(0)");
                        }
                    }
                    break;

                case 4:
                    if (current_iron_sword_num > 0)
                    {
                        item_num = 3;
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(1, 6);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");

                        item_num = 1;
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(1, 4);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");

                        item_num = item_num_c.Next(1, 3);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");

                        item_num = item_num_c.Next(1, 4);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");

                        item_num = item_num_c.Next(1, 6);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num += item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");

                        current_stone_sword_num--;
                        if (current_stone_sword_num == 0)
                        {
                            inventory.Remove("Stone Sword(0)");
                        }
                    }
                    break;

                case 5:
                    if (current_gold_sword_num > 0)
                    {
                        item_num = 2;
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(1, 4);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");

                        item_num = 1;
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(1);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");

                        item_num = item_num_c.Next(1, 4);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");

                        item_num = item_num_c.Next(1, 5);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");

                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num = current_iron_num + item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");

                        current_gold_sword_num--;
                        if (current_gold_sword_num == 0)
                        {
                            inventory.Remove("Gold Sword(0)");
                        }
                    }
                    break;

                case 6:
                    if (current_diamond_sword_num > 0)
                    {
                        item_num = item_num_c.Next(5, 10);
                        inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                        current_raw_chicken_num = current_raw_chicken_num + item_num;
                        inventory.Add($"Raw Chicken({current_raw_chicken_num})");

                        item_num = item_num_c.Next(5, 15);
                        inventory.Remove($"Feather({current_feather_num})");
                        current_feather_num = current_feather_num + item_num;
                        inventory.Add($"Feather({current_feather_num})");

                        item_num = item_num_c.Next(1, 2);
                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num = current_wool_num + item_num;
                        inventory.Add($"Wool({current_wool_num})");

                        item_num = item_num_c.Next(5, 15);
                        inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                        current_raw_mutton_num = current_raw_mutton_num + item_num;
                        inventory.Add($"Raw Mutton({current_raw_mutton_num})");

                        item_num = item_num_c.Next(10, 15);
                        inventory.Remove($"Raw Beef({current_raw_beef_num})");
                        current_raw_beef_num = current_raw_beef_num + item_num;
                        inventory.Add($"Raw Beef({current_raw_beef_num})");

                        item_num = item_num_c.Next(10, 15);
                        inventory.Remove($"Leather({current_leather_num})");
                        current_leather_num = current_leather_num + item_num;
                        inventory.Add($"Leather({current_leather_num})");

                        item_num = item_num_c.Next(10, 15);
                        inventory.Remove($"Raw Pork({current_raw_pork_num})");
                        current_raw_pork_num = current_iron_num + item_num;
                        inventory.Add($"Raw Pork({current_raw_pork_num})");

                        current_diamond_sword_num--;
                        if (current_diamond_sword_num == 0)
                        {
                            inventory.Remove("Diamond Sword(0)");
                        }
                    }
                    break;

                default:
                    Killing_Animals();
                    break;
            }
        }

        static void Simple_Crafts()
        {
            Console.Clear();
            if (current_tree_num > 0)
            {
                Console.WriteLine("100 - craft planks = 1 tree = 4 planks");
            }
            if (current_plank_num > 3)
            {
                Console.WriteLine("101 - make crafting table = 4 planks = 1 crafting table");
            }
            if (current_plank_num > 1)
            {
                Console.WriteLine("102 - make sticks = 2 planks = 4 sticks");
            }

            Console.Write("Enter a number to craft what you have in option(type 1 to leave): ");
            int craft_num = int.Parse(Console.ReadLine());

            switch (craft_num)
            {
                case 1:
                    Instructions();
                    break;

                case 100:
                    if (current_tree_num > 0)
                    {
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num--;
                        inventory.Add($"Tree({current_tree_num})");
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num += 4;
                        inventory.Add($"Plank({current_plank_num})");
                        if (current_tree_num == 0)
                        {
                            inventory.Remove("Tree(0)");
                        }
                    }
                    break;

                case 101:
                    if (current_plank_num > 3)
                    {
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 4;
                        inventory.Add($"Plank({current_plank_num})");
                        inventory.Remove($"Crafting Table({current_crafting_table_num})");
                        current_crafting_table_num += 1;
                        inventory.Add($"Crafting Table({current_crafting_table_num})");
                        if (current_plank_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                        }
                    }
                    break;

                case 102:
                    if (current_plank_num > 1)
                    {
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 2;
                        inventory.Add($"Plank({current_plank_num})");
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num += 4;
                        inventory.Add($"Stick({current_stick_num})");
                        if (current_plank_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                        }
                    }
                    break;

                default:
                    Simple_Crafts();
                    break;
            }

        }

        static void Advanced_Crafts()
        {

            Console.Clear();
            if (current_tree_num > 0)
            {
                Console.WriteLine("100 - craft planks = 1 tree = 4 planks");
            }
            if (current_plank_num > 3)
            {
                Console.WriteLine("101 - make crafting table = 4 planks = 1 crafting table");
            }
            if (current_plank_num > 1)
            {
                Console.WriteLine("102 - make sticks = 2 planks = 4 sticks");
            }
            if (current_plank_num > 2 && current_wool_num > 2)
            {
                Console.WriteLine("103 - make bed = 3 planks + 3 wool = 1 bed");
            }
            if (current_stick_num > 0 && current_plank_num > 1)
            {
                Console.WriteLine("104 - make wooden sword = 2 planks + 1 stick = 1 wooden sword");
            }
            if (current_stick_num > 0 && current_cobblestone_num > 1)
            {
                Console.WriteLine("105 - make stone sword = 2 cobblestones + 1 stick = 1 stone sword");
            }
            if (current_stick_num > 0 && current_iron_num > 1)
            {
                Console.WriteLine("106 - make iron sword = 2 iron ingots + 1 stick = 1 iron sword");
            }
            if (current_stick_num > 0 && current_gold_num > 1)
            {
                Console.WriteLine("107 - make gold sword = 2 gold ingots + 1 stick = 1 gold sword");
            }
            if (current_stick_num > 0 && current_diamond_num > 1)
            {
                Console.WriteLine("108 - make diamond sword = 2 diamonds + 1 stick = 1 diamond sword");
            }
            if (current_stick_num > 0 && current_plank_num > 2)
            {
                Console.WriteLine("109 - make wooden pickaxe = 3 planks + 2 sticks = 1 wooden pickaxe");
            }
            if (current_stick_num > 0 && current_cobblestone_num > 2)
            {
                Console.WriteLine("110 - make stone pickaxe = 3 cobblestones + 2 sticks = 1 stone pickaxe");
            }
            if (current_stick_num > 0 && current_iron_num > 2)
            {
                Console.WriteLine("111 - make iron pickaxe = 3 iron ingots + 2 sticks = 1 iron pickaxe");
            }
            if (current_stick_num > 0 && current_gold_num > 2)
            {
                Console.WriteLine("112 - make gold pickaxe = 3 gold ingots + 2 sticks = 1 gold pickaxe");
            }
            if (current_stick_num > 0 && current_diamond_num > 2)
            {
                Console.WriteLine("113 - make diamond pickaxe = 3 diamonds + 2 sticks = 1 diamond pickaxe");
            }
            if (current_stick_num > 0 && current_plank_num > 2)
            {
                Console.WriteLine("114 - make wooden axe = 3 planks + 2 sticks = 1 wooden axe");
            }
            if (current_stick_num > 0 && current_cobblestone_num > 2)
            {
                Console.WriteLine("115 - make stone axe = 3 cobblestones + 2 sticks = 1 stone axe");
            }
            if (current_stick_num > 0 && current_iron_num > 2)
            {
                Console.WriteLine("116 - make iron axe = 3 iron ingots + 2 sticks = 1 iron axe");
            }
            if (current_stick_num > 0 && current_gold_num > 2)
            {
                Console.WriteLine("117 - make gold axe = 3 gold ingots + 2 sticks = 1 gold axe");
            }
            if (current_stick_num > 0 && current_diamond_num > 2)
            {
                Console.WriteLine("118 - make diamond axe = 3 diamonds + 2 sticks = 1 diamond axe");
            }
            if (current_cobblestone_num > 7)
            {
                Console.WriteLine("119 - make furnace = 8 cobblestones = 1 furnace");
            }

            Console.Write("Enter a number to craft what you have in option(type 1 to leave): ");
            int craft_num = int.Parse(Console.ReadLine());

            switch (craft_num)
            {
                case 1:
                    Instructions();
                    break;

                case 100:

                    if (current_tree_num > 0)
                    {
                        inventory.Remove($"Tree({current_tree_num})");
                        current_tree_num--;
                        inventory.Add($"Tree({current_tree_num})");
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num += 4;
                        inventory.Add($"Plank({current_plank_num})");
                        if (current_tree_num == 0)
                        {
                            inventory.Remove("Tree(0)");
                        }
                    }
                    break;

                case 101:
                    if (current_plank_num > 3)
                    {
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 4;
                        inventory.Add($"Plank({current_plank_num})");
                        inventory.Remove($"Crafting Table({current_crafting_table_num})");
                        current_crafting_table_num += 1;
                        inventory.Add($"Crafting Table({current_crafting_table_num})");
                        if (current_plank_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                        }
                    }
                    break;

                case 102:
                    if (current_plank_num > 1)
                    {
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 2;
                        inventory.Add($"Plank({current_plank_num})");
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num += 4;
                        inventory.Add($"Stick({current_stick_num})");
                        if (current_plank_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                        }
                    }
                    break;

                case 103:
                    if (current_plank_num > 2 && current_wool_num > 2)
                    {
                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 3;
                        inventory.Add($"Plank({current_plank_num})");

                        inventory.Remove($"Wool({current_wool_num})");
                        current_wool_num -= 3;
                        inventory.Add($"Plank({current_wool_num})");

                        if (current_plank_num == 0 && current_wool_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                            inventory.Remove("Wool(0)");
                        }

                        inventory.Remove($"Bed({current_bed_num})");
                        current_bed_num++;
                        inventory.Add($"Bed({current_bed_num})");
                    }
                    break;

                case 104:
                    if (current_stick_num > 0 && current_plank_num >= 2)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num--;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 2;
                        inventory.Add($"Plank({current_plank_num})");

                        if (current_plank_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Wooden Sword({current_wooden_sword_num})");
                        current_wooden_sword_num++;
                        inventory.Add($"Wooden Sword({current_wooden_sword_num})");
                    }
                    break;

                case 105:

                    if (current_stick_num > 0 && current_cobblestone_num >= 2)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num--;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Cobblestone({current_cobblestone_num})");
                        current_cobblestone_num -= 2;
                        inventory.Add($"Cobblestone({current_cobblestone_num})");

                        if (current_cobblestone_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Cobblestone(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Stone Sword({current_stone_sword_num})");
                        current_stone_sword_num++;
                        inventory.Add($"Stone Sword({current_stone_sword_num})");
                    }
                    break;

                case 106:

                    if (current_stick_num > 0 && current_iron_num >= 2)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num--;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Iron Ingot({current_iron_num})");
                        current_iron_num -= 2;
                        inventory.Add($"Iron Ingot({current_iron_num})");

                        if (current_iron_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Iron Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Iron Sword({current_iron_sword_num})");
                        current_iron_sword_num++;
                        inventory.Add($"Iron Sword({current_iron_sword_num})");
                    }
                    break;

                case 107:

                    if (current_stick_num > 0 && current_gold_num >= 2)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num--;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Gold Ingot({current_gold_num})");
                        current_cobblestone_num -= 2;
                        inventory.Add($"Gold Ingot({current_gold_num})");

                        if (current_gold_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Gold Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Gold Sword({current_gold_sword_num})");
                        current_gold_sword_num++;
                        inventory.Add($"Gold Sword({current_gold_sword_num})");
                    }
                    break;

                case 108:

                    if (current_stick_num > 0 && current_diamond_num >= 2)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num--;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Diamond({current_diamond_num})");
                        current_diamond_num -= 2;
                        inventory.Add($"Diamond({current_diamond_num})");

                        if (current_diamond_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Diamond(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Diamond Sword({current_diamond_sword_num})");
                        current_diamond_sword_num++;
                        inventory.Add($"Diamond Sword({current_diamond_sword_num})");
                    }
                    break;

                case 109:
                    if (current_stick_num > 1 && current_plank_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 3;
                        inventory.Add($"Plank({current_plank_num})");

                        if (current_plank_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Wooden Pickaxe({current_wooden_pickaxe_num})");
                        current_wooden_pickaxe_num++;
                        inventory.Add($"Wooden Pickaxe({current_wooden_pickaxe_num})");
                    }
                    break;

                case 110:

                    if (current_stick_num > 1 && current_cobblestone_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Cobblestone({current_cobblestone_num})");
                        current_cobblestone_num -= 3;
                        inventory.Add($"Cobblestone({current_cobblestone_num})");

                        if (current_cobblestone_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Cobblestone(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Stone Pickaxe({current_stone_pickaxe_num})");
                        current_stone_pickaxe_num++;
                        inventory.Add($"Stone Pickaxe({current_stone_pickaxe_num})");
                    }
                    break;

                case 111:

                    if (current_stick_num > 1 && current_iron_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Iron Ingot({current_iron_num})");
                        current_iron_num -= 3;
                        inventory.Add($"Iron Ingot({current_iron_num})");

                        if (current_iron_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Iron Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Iron Pickaxe({current_iron_pickaxe_num})");
                        current_iron_pickaxe_num++;
                        inventory.Add($"Iron Pickaxe({current_iron_pickaxe_num})");
                    }
                    break;

                case 112:

                    if (current_stick_num > 1 && current_gold_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Gold Ingot({current_gold_num})");
                        current_gold_num -= 3;
                        inventory.Add($"Gold Ingot({current_gold_num})");

                        if (current_cobblestone_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Gold Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Gold Pickaxe({current_gold_pickaxe_num})");
                        current_gold_pickaxe_num++;
                        inventory.Add($"Gold Pickaxe({current_gold_pickaxe_num})");
                    }
                    break;

                case 113:

                    if (current_stick_num > 1 && current_diamond_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Diamond({current_diamond_num})");
                        current_diamond_num -= 3;
                        inventory.Add($"Diamond({current_diamond_num})");

                        if (current_diamond_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Diamond(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Diamond Pickaxe({current_diamond_pickaxe_num})");
                        current_diamond_pickaxe_num++;
                        inventory.Add($"Diamond Pickaxe({current_diamond_pickaxe_num})");
                    }
                    break;

                case 114:
                    if (current_stick_num > 1 && current_plank_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Plank({current_plank_num})");
                        current_plank_num -= 3;
                        inventory.Add($"Plank({current_plank_num})");

                        if (current_plank_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Plank(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Wooden Axe({current_wooden_axe_num})");
                        current_wooden_axe_num++;
                        inventory.Add($"Wooden Axe({current_wooden_axe_num})");
                    }
                    break;

                case 115:

                    if (current_stick_num > 1 && current_cobblestone_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Cobblestone({current_cobblestone_num})");
                        current_cobblestone_num -= 3;
                        inventory.Add($"Cobblestone({current_cobblestone_num})");

                        if (current_cobblestone_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Cobblestone(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Stone Axe({current_stone_axe_num})");
                        current_stone_axe_num++;
                        inventory.Add($"Stone Axe({current_stone_axe_num})");
                    }
                    break;

                case 116:

                    if (current_stick_num > 1 && current_iron_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Iron Ingot({current_iron_num})");
                        current_iron_num -= 3;
                        inventory.Add($"Iron Ingot({current_iron_num})");

                        if (current_iron_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Iron Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Iron Axe({current_iron_axe_num})");
                        current_iron_axe_num++;
                        inventory.Add($"Iron Axe({current_iron_axe_num})");
                    }
                    break;

                case 117:

                    if (current_stick_num > 1 && current_gold_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Gold Ingot({current_gold_num})");
                        current_gold_num -= 3;
                        inventory.Add($"Gold Ingot({current_gold_num})");

                        if (current_cobblestone_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Gold Ingot(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Gold Axe({current_gold_axe_num})");
                        current_gold_axe_num++;
                        inventory.Add($"Gold Axe({current_gold_axe_num})");
                    }
                    break;

                case 118:

                    if (current_stick_num > 1 && current_diamond_num >= 3)
                    {
                        inventory.Remove($"Stick({current_stick_num})");
                        current_stick_num -= 2;
                        inventory.Add($"Stick({current_stick_num})");

                        inventory.Remove($"Diamond({current_diamond_num})");
                        current_diamond_num -= 3;
                        inventory.Add($"Diamond({current_diamond_num})");

                        if (current_diamond_num == 0 && current_stick_num == 0)
                        {
                            inventory.Remove("Diamond(0)");
                            inventory.Remove("Stick(0)");
                        }

                        inventory.Remove($"Diamond Axe({current_diamond_axe_num})");
                        current_diamond_axe_num++;
                        inventory.Add($"Diamond Axe({current_diamond_axe_num})");
                    }
                    break;

                case 119:
                    if (current_cobblestone_num > 7)
                    {
                        inventory.Remove($"Cobblestone({current_cobblestone_num})");
                        current_cobblestone_num -= 8;
                        inventory.Add($"Cobblestone({current_cobblestone_num})");

                        if (current_cobblestone_num == 0)
                        {
                            inventory.Remove("Cobblestone(0)");
                        }

                        inventory.Remove($"Furnace({current_furnace_num})");
                        current_furnace_num++;
                        inventory.Add($"Furnace({current_furnace_num})");
                    }
                    break;

                default:
                    Advanced_Crafts();
                    break;

            }

        }
        static void Cook_Something()
        {
            // 1.Provjera za gorivo
            // 2.Koliko hrane treba napraviti
            // 3.Izabrati gorivo i njegov broj
            // 4.Dobiti napravljenu hranu
            if (current_furnace_num > 0)
            {
                Console.Clear();
                if (current_raw_chicken_num > 0)
                {
                    Console.WriteLine($"1.Raw Chicken({current_raw_chicken_num})");
                }
                if (current_raw_beef_num > 0)
                {
                    Console.WriteLine($"2.Raw Beef({current_raw_beef_num})");
                }
                if (current_raw_pork_num > 0)
                {
                    Console.WriteLine($"3.Raw Pork({current_raw_pork_num})");
                }
                if (current_raw_mutton_num > 0)
                {
                    Console.WriteLine($"4.Raw Mutton({current_raw_mutton_num})");
                }

                Console.Write("Enter the number what you want to cook: ");
                int i_cook = int.Parse(Console.ReadLine());
                Console.Write("Enter number of entered food that you want to be cooked: ");
                int i_cook_num = int.Parse(Console.ReadLine());

                if (current_tree_num > 0)
                {
                    Console.WriteLine($"1.Tree({current_tree_num})");
                }
                if (current_raw_beef_num > 0)
                {
                    Console.WriteLine($"2.Plank({current_plank_num})");
                }
                if (current_stick_num > 0)
                {
                    Console.WriteLine($"3.Stick({current_stick_num})");
                }
                if (current_coal_num > 0)
                {
                    Console.WriteLine($"4.Coal({current_coal_num})");
                }
                Console.Write("Enter the number that you want to use as a fuel: ");
                int i_fuel = int.Parse(Console.ReadLine());


                // Proces kuhanja
                switch (i_cook)
                {
                    case 1:
                        if (!(i_cook_num > current_raw_chicken_num || i_cook_num < 1))
                        {
                            Console.Clear();
                            Console.WriteLine("Cooking...");
                            inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                            current_raw_chicken_num -= i_cook_num;
                            inventory.Add($"Raw Chicken({current_raw_chicken_num})");
                            switch (i_fuel)
                            {
                                case 1:
                                    inventory.Remove($"Tree({current_tree_num})");
                                    current_tree_num -= i_cook_num / 4;
                                    inventory.Add($"Tree({current_tree_num})");
                                    break;

                                case 2:
                                    inventory.Remove($"Plank({current_plank_num})");
                                    current_plank_num -= i_cook_num / 2;
                                    inventory.Add($"Plank({current_plank_num})");
                                    break;

                                case 3:
                                    inventory.Remove($"Stick({current_stick_num})");
                                    current_stick_num -= i_cook_num;
                                    inventory.Add($"Stick({current_stick_num})");
                                    break;

                                case 4:
                                    inventory.Remove($"Coal({current_coal_num})");
                                    current_coal_num -= i_cook_num / 8;
                                    inventory.Add($"Coal({current_coal_num})");
                                    break;

                                default:
                                    Cook_Something();
                                    break;

                            }
                            if (current_raw_chicken_num <= 0)
                            {
                                inventory.Remove($"Raw Chicken({current_raw_chicken_num})");
                            }
                            inventory.Remove($"Cooked Chicken({current_chicken_num})");
                            current_chicken_num += i_cook_num;
                            inventory.Add($"Cooked Chicken({current_chicken_num})");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine("Cooking is done!");

                        }
                        break;

                    case 2:
                        if (!(i_cook_num > current_raw_beef_num || i_cook_num < 1))
                        {
                            Console.Clear();
                            Console.WriteLine("Cooking...");
                            inventory.Remove($"Raw Beef({current_raw_beef_num})");
                            current_raw_beef_num -= i_cook_num;
                            inventory.Add($"Raw Beef({current_raw_beef_num})");
                            switch (i_fuel)
                            {
                                case 1:
                                    inventory.Remove($"Tree({current_tree_num})");
                                    current_tree_num -= i_cook_num / 4;
                                    inventory.Add($"Tree({current_tree_num})");
                                    break;

                                case 2:
                                    inventory.Remove($"Plank({current_plank_num})");
                                    current_plank_num -= i_cook_num / 2;
                                    inventory.Add($"Plank({current_plank_num})");
                                    break;

                                case 3:
                                    inventory.Remove($"Stick({current_stick_num})");
                                    current_stick_num -= i_cook_num;
                                    inventory.Add($"Stick({current_stick_num})");
                                    break;

                                case 4:
                                    inventory.Remove($"Coal({current_coal_num})");
                                    current_coal_num -= i_cook_num / 8;
                                    inventory.Add($"Coal({current_coal_num})");
                                    break;

                                default:
                                    Cook_Something();
                                    break;

                            }
                            if (current_raw_beef_num <= 0)
                            {
                                inventory.Remove($"Raw Beef({current_raw_beef_num})");
                            }
                            inventory.Remove($"Cooked Beef({current_beef_num})");
                            current_beef_num += i_cook_num;
                            inventory.Add($"Cooked Beef({current_beef_num})");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine("Cooking is done!");
                        }
                        break;

                    case 3:
                        if (!(i_cook_num > current_raw_beef_num || i_cook_num < 1))
                        {
                            Console.Clear();
                            Console.WriteLine("Cooking...");
                            inventory.Remove($"Raw Pork({current_raw_pork_num})");
                            current_raw_pork_num -= i_cook_num;
                            inventory.Add($"Raw Pork({current_raw_pork_num})");
                            switch (i_fuel)
                            {
                                case 1:
                                    inventory.Remove($"Tree({current_tree_num})");
                                    current_tree_num -= i_cook_num / 4;
                                    inventory.Add($"Tree({current_tree_num})");
                                    break;

                                case 2:
                                    inventory.Remove($"Plank({current_plank_num})");
                                    current_plank_num -= i_cook_num / 2;
                                    inventory.Add($"Plank({current_plank_num})");
                                    break;

                                case 3:
                                    inventory.Remove($"Stick({current_stick_num})");
                                    current_stick_num -= i_cook_num;
                                    inventory.Add($"Stick({current_stick_num})");
                                    break;

                                case 4:
                                    inventory.Remove($"Coal({current_coal_num})");
                                    current_coal_num -= i_cook_num / 8;
                                    inventory.Add($"Coal({current_coal_num})");
                                    break;

                                default:
                                    Cook_Something();
                                    break;

                            }
                            if (current_raw_pork_num <= 0)
                            {
                                inventory.Remove($"Raw Pork({current_raw_pork_num})");
                            }
                            inventory.Remove($"Cooked Pork({current_pork_num})");
                            current_pork_num += i_cook_num;
                            inventory.Add($"Cooked Pork({current_pork_num})");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine("Cooking is done!");
                        }
                        break;

                    case 4:
                        if (!(i_cook_num > current_raw_beef_num || i_cook_num < 1))
                        {
                            Console.Clear();
                            Console.WriteLine("Cooking...");
                            inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                            current_raw_mutton_num -= i_cook_num;
                            inventory.Add($"Raw Mutton({current_raw_mutton_num})");
                            switch (i_fuel)
                            {
                                case 1:
                                    inventory.Remove($"Tree({current_tree_num})");
                                    current_tree_num -= i_cook_num / 4;
                                    inventory.Add($"Tree({current_tree_num})");
                                    break;

                                case 2:
                                    inventory.Remove($"Plank({current_plank_num})");
                                    current_plank_num -= i_cook_num / 2;
                                    inventory.Add($"Plank({current_plank_num})");
                                    break;

                                case 3:
                                    inventory.Remove($"Stick({current_stick_num})");
                                    current_stick_num -= i_cook_num;
                                    inventory.Add($"Stick({current_stick_num})");
                                    break;

                                case 4:
                                    inventory.Remove($"Coal({current_coal_num})");
                                    current_coal_num -= i_cook_num / 8;
                                    inventory.Add($"Coal({current_coal_num})");
                                    break;

                                default:
                                    Cook_Something();
                                    break;

                            }
                            if (current_raw_mutton_num <= 0)
                            {
                                inventory.Remove($"Raw Mutton({current_raw_mutton_num})");
                            }
                            inventory.Remove($"Cooked Mutton({current_mutton_num})");
                            current_mutton_num += i_cook_num;
                            inventory.Add($"Cooked Mutton({current_mutton_num})");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.WriteLine("Cooking is done!");
                        }
                        break;

                    default:
                        Cook_Something();
                        break;




                }

            }
        }
    }
}

