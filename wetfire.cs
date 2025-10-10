
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Skapa ny hjälteprofil med:
public class hjälteprofil
//Username (hjältenamn)
{
    //Password (lösenord) – styrkekontroll (minst 6 tecken, 1 siffra, 1 stor bokstav, 1 specialtecken).
    //Email eller Phone för 2FA.
    public int password { get; set; }

    string inputPassword = Console.ReadLine();
    public string name { get; set; }
    public int level { get; set; }
    public hjälteprofil(string name, int level)
    {
        this.name = name;
        this.level = level;
    }
    public void displayProfile()
    {
        Console.WriteLine($"Hjältenamn: {name}, Level: {level}");
        Console.WriteLine($"Lösenord: {password}");
        Console.ReadLine();
    }




}





//Password (lösenord) – styrkekontroll (minst 6 tecken, 1 siffra, 1 stor bokstav, 1 specialtecken).
//Email eller Phone för 2FA.

//Vid inloggning:
//Ange namn/lösenord.
//Systemet skickar kod via SMS/Email (2FA) → måste anges korrekt för att komma in i guilden.
//