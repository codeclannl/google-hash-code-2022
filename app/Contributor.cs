internal class Contributor
{
    private readonly List<Skill> _skills = new();
    /// <summary>
    /// Ordered list of periods that this contributor is already working on a project.
    /// </summary>
    private List<Period> claimedPeriods = new();

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

    /// <summary>
    /// Tries to claim this contributor for the given period.
    /// </summary>
    public void ClaimForPeriod(Period period)
    {
        if (IsAvailableForPeriod(period))
        {
            // insert the period at the right place
            claimedPeriods = claimedPeriods
                .TakeWhile(p => p.end < period.start) // claimed periods before period
                .Append(period)
                .Concat(claimedPeriods.SkipWhile(p => p.end < period.start)).ToList(); // all other claimed periods 
        }
    }

    /// <summary>
    /// Checks whether the contributor is already assigned to a project during this period.
    /// </summary>
    /// <param name="period">Period that we want to claim the contributor</param>
    /// <returns></returns>
    public bool IsAvailableForPeriod(Period period)
    {
        if (claimedPeriods.Count == 0)
        {
            return true;
        }
        // skip claimed periods that lie before the period
        int periodIndex = 0;
        while (periodIndex < claimedPeriods.Count && claimedPeriods[periodIndex].end < period.start)
        {
            periodIndex++;
        }
        // the next claimed period, if any, either overlaps with `period` or not, determining whether `period` can be claimed
        if (periodIndex == claimedPeriods.Count)
        {
            return true; // all claimed periods lie before `period`
        } 
        else
        {
            // Only if the next claimed period (and by ordering all following claimed periods) starts after
            // period.end, we can safely claim this interval.
            return claimedPeriods[periodIndex].start > period.end;
        }
    }
}
