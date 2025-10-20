namespace QuestGuildTerminal;
public class Quest
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Quest(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
