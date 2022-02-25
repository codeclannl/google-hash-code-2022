internal class Contributor
{
    private readonly List<Skill> _skills = new();
    public Contributor(InputContributor inputContributor, List<string> allSkills)
    {
        Name = inputContributor.Name;
        _skills.AddRange(inputContributor.Skills.Select(s => new Skill(s)));
        SkillLevel = allSkills.ToDictionary(s => s, s => 0);
        _skills.ForEach(s => SkillLevel[s.Name] = s.Level);
    }

    public string Name { get; }

    public IReadOnlyList<Skill> Skills
        => _skills;

    public Dictionary<string, int> SkillLevel { get; set; }

    private void AddSkill(string skillName, int skillLevel)
        => _skills.Add(new Skill(skillName, skillLevel));
}
