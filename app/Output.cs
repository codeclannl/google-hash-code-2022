internal class Output
{
    private readonly List<ProjectPlanning> _planning = new();

    public IReadOnlyList<ProjectPlanning> Planning
        => _planning;

    public void AddProjectPlanning(ProjectPlanning projectPlanning)
        => _planning.Add(projectPlanning);
}