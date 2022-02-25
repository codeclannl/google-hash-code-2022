internal class HashCode
{
    public static Output CreateOutput(Input input)
    {
        //todo: make mutable copies
        List<Project> projects = input.Projects.ToList();
        List<Contributor> contributors = input.Contributors.ToList();
        List<Project> completedProjects = new();
        while (projects.Any())
        {
            List<Project> feasibleProjects = CalculateFeasible(projects, contributors);
            if (!feasibleProjects.Any())
            {
                break;
            }
            Project bestProject = PickBestProject(feasibleProjects);
            projects.Remove(bestProject);
            UpdateContributors(contributors);
            completedProjects.Add(bestProject);
        }

        return new Output();
    }

    private static List<Project> CalculateFeasible(List<Project> projects, List<Contributor> contributors)
    {
        //todo: projectassignees
        projects = HaveRequiredSkills(projects, contributors);
        return projects;
    }

    private static List<Project> HaveRequiredSkills(List<Project> projects, List<Contributor> contributors) => projects;
    //Greedy
    private static Project PickBestProject(List<Project> feasibleProjects) => feasibleProjects.MaxBy(p => p.Score);
    private static void UpdateContributors(List<Contributor> contributors) { return; }
}
