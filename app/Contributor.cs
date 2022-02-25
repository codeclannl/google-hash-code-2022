// See https://aka.ms/new-console-template for more information
internal class Contributor
{
    private readonly List<Skill> _skills = new();

    public Contributor(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<Skill> Skills
        => _skills;

    public void AddSkill(string skillName, int skillLevel)
        => _skills.Add(new Skill(skillName, skillLevel));
}