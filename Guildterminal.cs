using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestGuildTerminal;

public class GuildTerminal
{
    private List<Hero> _heroes = new List<Hero>();
    private Hero? _hero;
    private QuestManager _questManager = new QuestManager();

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("⚔️ Välkommen till Quest Guild Terminal ⚔️");

        while (true)
        {
            ShowMainMenu();
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1": CreateHeroProfile(); break;
                case "2": LoginHero(); break;
                case "3": _questManager.ShowAll(); break;
                case "4": CompleteQuest(); break;
                case "5": UpdateQuest(); break;
                case "6": AddNewQuest(); break;
                case "7":
                    Console.WriteLine("👋 Hejdå!");
                    return;
                default:
                    Console.WriteLine("❌ Ogiltigt val.");
                    break;
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\n=== Quest Guild Terminal ===");
        Console.WriteLine("1. Skapa hjälteprofil");
        Console.WriteLine("2. Logga in");
        Console.WriteLine("3. Visa quests");
        Console.WriteLine("4. Slutför quest");
        Console.WriteLine("5. Uppdatera quest-titel");
        Console.WriteLine("6. Lägg till ny quest");
        Console.WriteLine("7. Avsluta");
        Console.Write("Välj alternativ: ");
    }

  private void CreateHeroProfile()
{
    Console.Write("Ange hjältenamn: ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Ange nivå: ");
    if (!int.TryParse(Console.ReadLine(), out int level) || level < 1)
    {
        Console.WriteLine("❌ Ogiltig nivå.");
        return;
    }

    Console.Write("Skapa lösenord (min 6 tecken, 1 siffra, 1 stor bokstav, 1 specialtecken): ");
    string password = Console.ReadLine() ?? string.Empty;

    if (!IsValidPassword(password))
    {
        Console.WriteLine("❌ Lösenordet uppfyller inte krav.");
        return;
    }

    // === 🧬 RAS & KLASS ===
    Console.WriteLine("\nVälj ras:");
    Console.WriteLine("1. 🧝 Alv");
    Console.WriteLine("2. 🧔 Människa");
    Console.WriteLine("3. 🧌 Ork");
    Console.WriteLine("4. 🐉 Drakblod");
    Console.Write("Skriv numret för din ras: ");

    string raceChoice = Console.ReadLine() ?? string.Empty;
    string race = raceChoice switch
    {
        "1" => "Alv",
        "2" => "Människa",
        "3" => "Ork",
        "4" => "Drakblod",
        _ => "Okänd"
    };

    Console.WriteLine("\nVälj klass:");
    Console.WriteLine("1. ⚔️ Krigare");
    Console.WriteLine("2. 🧙 Magiker");
    Console.WriteLine("3. 🏹 Jägare");
    Console.WriteLine("4. 🕵️ Lönnmördare");
    Console.Write("Skriv numret för din klass: ");

    string classChoice = Console.ReadLine() ?? string.Empty;
    string heroClass = classChoice switch
    {
        "1" => "Krigare",
        "2" => "Magiker",
        "3" => "Jägare",
        "4" => "Lönnmördare",
        _ => "Okänd"
    };

    // Skapa hjälten
    _hero = new Hero(name, password, string.Empty, race, heroClass, level);
    _heroes.Add(_hero);

    Console.WriteLine($"\n✅ Hjälteprofil skapad för '{name}'!");
    Console.WriteLine($"🧬 Ras: {race}");
    Console.WriteLine($"🎯 Klass: {heroClass}");
}

    // Removed duplicate LoginHero method to resolve ambiguity.

    private void CompleteQuest()
    {
        Console.Write("Ange questtitel att slutföra: ");
        string title = Console.ReadLine() ?? string.Empty;
        _questManager.Complete(title);
    }

    private void UpdateQuest()
    {
        Console.Write("Ange nuvarande questtitel: ");
        string oldTitle = Console.ReadLine() ?? string.Empty;
        Console.Write("Ange ny questtitel: ");
        string newTitle = Console.ReadLine() ?? string.Empty;
        _questManager.Update(oldTitle, newTitle);
    }

    private void AddNewQuest()
    {
        Console.Write("Ange questtitel: ");
        string title = Console.ReadLine() ?? string.Empty;
        Console.Write("Ange questbeskrivning: ");
        string description = Console.ReadLine() ?? string.Empty;

        _questManager.AddQuest(title, description);
    }

    private bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password)
               && password.Length >= 6
               && password.Any(char.IsDigit)
               && password.Any(char.IsUpper)
               && password.Any(ch => !char.IsLetterOrDigit(ch));
    }
private void LoginHero()
{
    if (_hero == null)
    {
        Console.WriteLine("⚠️ Skapa först en hjälteprofil.");
        return;
    }

    Console.Write("Ange lösenord: ");
    string password = Console.ReadLine() ?? string.Empty;

    if (_hero.Password == password)
    {
        Console.WriteLine("✅ Lösenord korrekt.");

        // === 🔐 Steg 2: Tvåstegsverifiering ===
        var random = new Random();
        int code = random.Next(100000, 999999); // genererar t.ex. 532841

        Console.WriteLine($"📩 Din verifieringskod är: {code}");
        Console.Write("Ange koden: ");
        string input = Console.ReadLine() ?? string.Empty;

        if (input == code.ToString())
        {
            Console.WriteLine($"✅ Inloggad som {_hero.Name}! 🦸");
        }
        else
        {
            Console.WriteLine("❌ Fel verifieringskod. Inloggning misslyckades.");
        }
    }
    else
    {
        Console.WriteLine("❌ Fel lösenord.");
    }
}

}

