// See https://aka.ms/new-console-template for more information
internal class InputContributor
{
    private readonly List<Skill> _skills = new();

    public InputContributor(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<Skill> Skills
        => _skills;

    public void AddSkill(string skillName, int skillLevel)
        => _skills.Add(new Skill(skillName, skillLevel));
}