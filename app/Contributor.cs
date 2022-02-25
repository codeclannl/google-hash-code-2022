internal class Contributor
{
    private readonly List<Skill> _skills = new();
    public Contributor(InputContributor inputContributor)
    {
        Name = inputContributor.Name;
        _skills.AddRange(inputContributor.Skills.Select(s => new Skill(s)));
    }

    public string Name { get; }

    public IReadOnlyList<Skill> Skills
        => _skills;

    private void AddSkill(string skillName, int skillLevel)
        => _skills.Add(new Skill(skillName, skillLevel));
}
