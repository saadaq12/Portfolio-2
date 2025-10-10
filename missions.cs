using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Quest

{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted
    {
        get; private set;
    }

    public Quest(string title, string description)
    {
        Title = title;
        Description = description;
        IsCompleted = false;
    }
    public void CompleteQuest()
    {
        IsCompleted = true;
        Console.WriteLine($"Quest '{Title}' completed!");
    }
    public void display()
    {
        Console.WriteLine($"Quest: {Title}\nDescription: {Description}\nCompleted: {IsCompleted}");
    }
}