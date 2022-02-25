internal class ProjectPlanning
{
    private readonly List<InputContributor> _contributors = new();

    public ProjectPlanning(Project project)
    {
        Project = project;
    }

    public Project Project { get; }

    public IReadOnlyList<InputContributor> Contributors
        => _contributors;

    public void AddContributor(InputContributor contributor)
        => _contributors.Add(contributor);

    public void AddContributors(List<InputContributor> contributors)
        => _contributors.AddRange(contributors);
}