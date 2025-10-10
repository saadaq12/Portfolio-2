using System;
using System.Text.RegularExpressions;

public class HjalteProfil
{
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string EmailOrPhone { get; private set; }
    public int Level { get; private set; }

    // Simulerad lagrad hjälte
    private static HjalteProfil _registeredHero = null;

    public HjalteProfil(string name, string password, string emailOrPhone, int level)
    {
        Name = name;
        Password = password;
        EmailOrPhone = emailOrPhone;
        Level = level;
    }

    // Skapa ny hjälteprofil
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
                Console.WriteLine("⚠️ Lösenordet är för svagt. Det måste ha minst 6 tecken, 1 stor bokstav, 1 siffra och 1 specialtecken.");
        }

        Console.Write("Ange email eller telefon för 2FA: ");
        string emailOrPhone = Console.ReadLine();

        _registeredHero = new HjalteProfil(name, password, emailOrPhone, 1);
        Console.WriteLine($"🧝 Hjälteprofil skapad för {name}!");
    }

    // Inloggning med 2FA
    public static bool Login()
    {
        if (_registeredHero == null)
        {
            Console.WriteLine("⚠️ Ingen hjälteprofil hittades. Vänligen skapa en först.");
            return false;
        }

        Console.Write("Ange hjältenamn: ");
        string name = Console.ReadLine();
        Console.Write("Ange lösenord: ");
        string password = Console.ReadLine();

        if (name == _registeredHero.Name && password == _registeredHero.Password)
        {
            Console.WriteLine($"✅ Välkommen tillbaka, {_registeredHero.Name}!");
            return TwoFactorAuth();
        }
        else
        {
            Console.WriteLine("❌ Fel namn eller lösenord.");
            return false;
        }
    }

    // Tvåfaktorsautentisering
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

    // Lösenords-styrkekontroll
    private static bool IsStrongPassword(string password)
    {
        if (password.Length < 6) return false;
        if (!Regex.IsMatch(password, @"[A-Z]")) return false;  // minst en stor bokstav
        if (!Regex.IsMatch(password, @"[0-9]")) return false;  // minst en siffra
        if (!Regex.IsMatch(password, @"[\W_]")) return false;  // minst ett specialtecken
        return true;
    }

    // Visa hjälteprofil
    public void DisplayProfile()
    {
        Console.WriteLine($"\n🧝 Hjälteprofil:");
        Console.WriteLine($"Namn: {Name}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"2FA-kontakt: {EmailOrPhone}");
    }
}
