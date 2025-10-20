using System;
using System.Text.RegularExpressions;
namespace QuestGuildTerminal;
public class Hero
{
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string EmailOrPhone { get; private set; }
    public string Race { get; private set; }
    public string HeroClass { get; private set; } // 🧙‍♂️ Ny!
    public int Level { get; private set; }

    private static Hero? _registeredHero = null;

    public Hero(string name, string password, string emailOrPhone, string race, string heroClass, int level)
    {
        Name = name;
        Password = password;
        EmailOrPhone = emailOrPhone;
        Race = race;
        HeroClass = heroClass;
        Level = level;
    }

    // 🧙‍♂️ Registrering
    public static void Register()
    {
        Console.Write("Ange hjältenamn: ");
        string name = Console.ReadLine();

        string password;
        while (true)
        {
            Console.Write("Ange lösenord: ");
            password = Console.ReadLine();
            if (IsStrongPassword(password))
                break;
            else
                Console.WriteLine("⚠️ Svagt lösenord. Måste ha minst 6 tecken, 1 stor bokstav, 1 siffra och 1 specialtecken.");
        }

        Console.Write("Ange email eller telefon för 2FA: ");
        string emailOrPhone = Console.ReadLine();

        string race = ChooseRace();
        string heroClass = ChooseClass();

        _registeredHero = new Hero (name, password, emailOrPhone, race, heroClass, 1);
        Console.WriteLine($"🧝 Hjälteprofil skapad för {name}, den stolta {race} {heroClass}!");
    }

    // 🧝‍♂️ Rasval
    private static string ChooseRace()
    {
        string[] races = { "Human", "Elf", "Dwarf", "Orc", "Undead" };

        Console.WriteLine("\nVälj din ras:");
        for (int i = 0; i < races.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {races[i]}");
        }

        while (true)
        {
            Console.Write("👉 Ditt val (1–5): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= races.Length)
                return races[choice - 1];

            Console.WriteLine("❌ Ogiltigt val, försök igen.");
        }
    }

    // ⚔️ Klassval
    private static string ChooseClass()
    {
        string[] classes = { "Assassin", "Mage", "Fighter", "Ranger", "Necromancer" };

        Console.WriteLine("\nVälj din klass:");
        for (int i = 0; i < classes.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {classes[i]}");
        }

        while (true)
        {
            Console.Write("👉 Ditt val (1–5): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= classes.Length)
                return classes[choice - 1];

            Console.WriteLine("❌ Ogiltigt val, försök igen.");
        }
    }

    // 🧩 Inloggning
    public static bool Login()
    {
        if (_registeredHero == null)
        {
            Console.WriteLine("⚠️ Ingen hjälteprofil hittades. Skapa en först.");
            return false;
        }

        Console.Write("Ange hjältenamn: ");
        string name = Console.ReadLine();
        Console.Write("Ange lösenord: ");
        string password = Console.ReadLine();

        if (name == _registeredHero.Name && password == _registeredHero.Password)
        {
            Console.WriteLine($"✅ Välkommen tillbaka, {_registeredHero.Name} av släktet {_registeredHero.Race} — {_registeredHero.HeroClass}!");
            return TwoFactorAuth();
        }
        else
        {
            Console.WriteLine("❌ Fel namn eller lösenord.");
            return false;
        }
    }

    // 🧩 Tvåfaktorsautentisering
    private static bool TwoFactorAuth()
    {
        Random random = new Random();
        string code = random.Next(100000, 999999).ToString();
        Console.WriteLine($"📱 Kod skickad till: {_registeredHero.EmailOrPhone}");
        Console.WriteLine($"(För test: din 2FA-kod är {code})");
        Console.Write("Ange 2FA-kod: ");
        string input = Console.ReadLine();

        if (input == code)
        {
            Console.WriteLine("✅ 2FA verifierad!");
            return true;
        }
        else
        {
            Console.WriteLine("❌ Fel 2FA-kod.");
            return false;
        }
    }

    // 🔐 Lösenordsstyrka
    private static bool IsStrongPassword(string password)
    {
        if (password.Length < 6) return false;
        if (!Regex.IsMatch(password, @"[A-Z]")) return false;
        if (!Regex.IsMatch(password, @"[0-9]")) return false;
        if (!Regex.IsMatch(password, @"[\W_]")) return false;
        return true;
    }

    // 🪪 Visa profil
    public void DisplayProfile()
    {
        Console.WriteLine($"\n🧙‍♂️ Hjälteprofil:");
        Console.WriteLine($"Namn: {Name}");
        Console.WriteLine($"Ras: {Race}");
        Console.WriteLine($"Klass: {HeroClass}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"2FA-kontakt: {EmailOrPhone}");
    }
}
