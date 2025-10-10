using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGuildTerminal
{
    public class GuildTerminal
    {
        private Hero _hero;
        private List<Quest> _quests = new();

        public void AddQuest()
        {
            _quests.Add(new Quest { Title = "Defeat the Dragon", IsCompleted = false });
            _quests.Add(new Quest { Title = "Rescue the Princess", IsCompleted = true });
            _quests.Add(new Quest { Title = "Find the Lost Sword", IsCompleted = false });
        }

        public void ShowAllQuests()
        {
            if (_quests.Count == 0)
            {
                Console.WriteLine("No quests found.");
                return;
            }

            foreach (var quest in _quests)
            {
                Console.WriteLine($"Quest: {quest.Title}, Completed: {quest.IsCompleted}");
            }
        }

        public void CompleteQuest(string title)
        {
            var quest = _quests.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (quest != null)
            {
                quest.IsCompleted = true;
                Console.WriteLine($"✅ Quest '{title}' marked as completed.");
            }
            else
            {
                Console.WriteLine($"❌ Quest '{title}' not found.");
            }
        }

        public void UpdateQuest(string oldTitle, string newTitle)
        {
            var quest = _quests.FirstOrDefault(q => q.Title.Equals(oldTitle, StringComparison.OrdinalIgnoreCase));
            if (quest != null)
            {
                quest.Title = newTitle;
                Console.WriteLine($"✏️ Quest title updated from '{oldTitle}' to '{newTitle}'.");
            }
            else
            {
                Console.WriteLine($"❌ Quest '{oldTitle}' not found.");
            }
        }

        // ===== MAIN MENU =====
        public void Start()
        {
            AddQuest(); // Lägg till exempelquests i början

            Console.WriteLine("⚔️ Welcome to the Quest Guild Terminal ⚔️");

            while (true)
            {
                Console.WriteLine("\n1. Create Hero Profile");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. View Quests");
                Console.WriteLine("4. Complete a Quest");
                Console.WriteLine("5. Update a Quest Title");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

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
                        Console.Write("Enter quest title to complete: ");
                        string title = Console.ReadLine();
                        CompleteQuest(title);
                        break;

                    case "5":
                        Console.Write("Enter old quest title: ");
                        string oldTitle = Console.ReadLine();
                        Console.Write("Enter new quest title: ");
                        string newTitle = Console.ReadLine();
                        UpdateQuest(oldTitle, newTitle);
                        break;

                    case "6":
                        Console.WriteLine("👋 Exiting...");
                        return;

                    default:
                        Console.WriteLine("❌ Invalid option. Please try again.");
                        break;
                }
            }
        }

        // ===== AUTHENTICATION =====
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
            {
                Console.WriteLine($"✅ Welcome back, {_hero.Name}!");
                TwoFactorCheck();
            }
            else
            {
                Console.WriteLine("❌ Incorrect name or password.");
            }
        }

        // ===== 2FA (Two-Factor Authentication) =====
        private void TwoFactorCheck()
        {
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString(); // 6-siffrig kod
            Console.WriteLine($"📱 Your 2FA code is: {code}");
            Console.Write("Enter the code: ");
            string entered = Console.ReadLine();

            if (entered == code)
                Console.WriteLine("✅ Two-Factor Authentication successful!");
            else
                Console.WriteLine("❌ Invalid code. Access denied.");
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
                Console.WriteLine($"🗺️ {quest.Title} - {(quest.IsCompleted ? "✅ Completed" : "🕓 In progress")}");
        }
    }

    // Models
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
