using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGuildTerminal
{
    public class GuildTerminal
    {
        private Hero _hero;
        private List<Quest> _quests = new();

        public void Start()
        {
            Console.WriteLine("⚔️ Welcome to the Quest Guild Terminal ⚔️");
            Console.WriteLine("1. Create Hero Profile");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. View Quests");
            Console.WriteLine("4. Exit");

            while (true)
            {
                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateHeroProfile();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        ViewQuests();
                        break;
                    case "4":
                        Console.WriteLine("👋 Exiting...");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void CreateHeroProfile()
        {
            Console.Write("Enter hero name: ");
            string name = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            _hero = new Hero(name, password);
            Console.WriteLine($"🧝 Hero profile created for {_hero.Name}!");
        }

        private void Login()
        {
            if (_hero == null)
            {
                Console.WriteLine("⚠️ No hero profile found. Please create one first.");
                return;
            }

            Console.Write("Enter hero name: ");
            string name = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (name == _hero.Name && password == _hero.Password)
                Console.WriteLine($"✅ Welcome back, {_hero.Name}!");
            else
                Console.WriteLine("❌ Incorrect name or password.");
        }

        private void ViewQuests()
        {
            if (_hero == null)
            {
                Console.WriteLine("⚠️ You must log in first!");
                return;
            }

            if (_quests.Count == 0)
            {
                Console.WriteLine("📜 No quests available yet.");
                return;
            }

            Console.WriteLine("\n--- QUEST LIST ---");
            foreach (var quest in _quests)
                Console.WriteLine($"🗺️ {quest.Title} - {(quest.IsCompleted ? "Completed" : "In progress")}");
        }
    }

    // Simple models for Hero and Quest
    public class Hero
    {
        public string Name { get; }
        public string Password { get; }

        public Hero(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }

    public class Quest
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
