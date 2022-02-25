internal  class ProjectPlanning
{
    private readonly List<Contributor> _contributors = new();

    public ProjectPlanning(Project project)
    {
        Project = project;
    }

    public Project Project { get; }

    public IReadOnlyList<Contributor> Contributors
        => _contributors;

    public void AddContributor(Contributor contributor)
        => _contributors.Add(contributor);
}