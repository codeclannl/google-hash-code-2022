internal class Project
{
    private readonly List<Skill> _skillRequirements = new();

    public Project(string name, int duration, int score, int bestBeforeDay)
    {
        Name = name;
        Duration = duration;
        Score = score;
        BestBeforeDay = bestBeforeDay;
    }

    public string Name { get; }
    public int Duration { get; }
    public int Score { get; }
    public int BestBeforeDay { get; }

    public int PossibleScore => Score - Math.Max(Duration - BestBeforeDay, 0);

    public IReadOnlyList<Skill> SkillRequirements
        => _skillRequirements;

    public void AddSkill(string name, int level)
        => _skillRequirements.Add(new Skill(name, level));
}