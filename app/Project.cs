internal class Project
{
    private readonly List<Skill> _skills = new();

    public Project(string name, int days, int score, int bestBeforeDay)
    {
        Name = name;
        Days = days;
        Score = score;
        BestBeforeDay = bestBeforeDay;
    }

    public string Name { get; }
    public int Days { get; }
    public int Score { get; }
    public int BestBeforeDay { get; }

    public void AddSkill(string name, int level)
        => _skills.Add(new Skill(name, level));
}