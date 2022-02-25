internal class Skill
{
    public Skill(Skill skill)
    {
        Name = skill.Name;
        Level = skill.Level;
    }

    public Skill(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public string Name { get; }
    public int Level { get; set; }
}