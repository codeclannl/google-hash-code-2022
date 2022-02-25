internal class Input
{
    private readonly List<Contributor> _contributors = new();
    private readonly List<Project> _projects = new();

    public IReadOnlyList<Contributor> Contributors
        => _contributors;

    public IReadOnlyList<Project> Projects
        => _projects;

    public void AddContributer(Contributor contributer)
        => _contributors.Add(contributer);

    public void AddProject(Project project)
        => _projects.Add(project);
}
