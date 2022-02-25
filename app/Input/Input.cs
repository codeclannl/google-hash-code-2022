internal class Input
{
    private readonly List<InputContributor> _contributors = new();
    private readonly List<Project> _projects = new();

    public IReadOnlyList<InputContributor> Contributors
        => _contributors;

    public IReadOnlyList<Project> Projects
        => _projects;

    public void AddContributer(InputContributor contributer)
        => _contributors.Add(contributer);

    public void AddProject(Project project)
        => _projects.Add(project);
}
