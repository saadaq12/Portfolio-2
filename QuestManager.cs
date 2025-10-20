using System;
using System.Collections.Generic;

namespace QuestGuildTerminal;

public class QuestManager
{
    private List<Quest> _quests = new();

    public void AddQuest(string title, string description)
    {
        _quests.Add(new Quest(title, description));
        Console.WriteLine($"✅ Lagt till quest: {title}");
    }

    public void ShowAll()
    {
        if (_quests.Count == 0)
        {
            Console.WriteLine("📭 Inga quests tillgängliga.");
            return;
        }

        Console.WriteLine("\n=== Lista över quests ===");
        foreach (var quest in _quests)
        {
            Console.WriteLine($"• {quest.Title} - {quest.Description}");
        }
    }

    public void Complete(string title)
    {
        var quest = _quests.Find(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (quest != null)
        {
            Console.WriteLine($"🏁 Quest '{title}' markerad som klar!");
            _quests.Remove(quest);
        }
        else
        {
            Console.WriteLine("❌ Hittade ingen quest med det namnet.");
        }
    }

    public void Update(string oldTitle, string newTitle)
    {
        var quest = _quests.Find(q => q.Title.Equals(oldTitle, StringComparison.OrdinalIgnoreCase));
        if (quest != null)
        {
            quest.Title = newTitle;
            Console.WriteLine($"✏️ Quest uppdaterad till '{newTitle}'!");
        }
        else
        {
            Console.WriteLine("❌ Questen kunde inte hittas.");
        }
    }
}
