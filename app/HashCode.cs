internal class HashCode
{
    public static Output CreateOutput(Input input)
    {
        //todo: make mutable copies
        List<Project> projects = input.Projects.ToList();
        List<Contributor> contributors = input.Contributors.ToList();
        Output output = new();
        while (projects.Any())
        {
            List<Project> feasibleProjects = CalculateFeasible(projects, contributors);
            if (!feasibleProjects.Any())
            {
                break;
            }
            Project bestProject = PickBestProject(feasibleProjects);

            ProjectPlanning projectPlanning = new(bestProject);
            //todo: assign correct contributors, mostly focussing on skill improvement if possible
            projectPlanning.AddContributor(contributors[0]);

            projects.Remove(bestProject);
            UpdateContributorSkillLevel(contributors);
            output.AddProjectPlanning(projectPlanning);
        }

        return output;
    }

    /// <summary>
    /// Find all projects where required skills are available.
    /// future: filter out projects with score 0
    /// futurefuture: consider improving skills through 0 score projects
    /// </summary>
    private static List<Project> CalculateFeasible(List<Project> projects, List<Contributor> contributors)
    {
        projects = HaveRequiredSkills(projects, contributors);
        return projects;
    }

    /// <summary>
    /// calculate whether the skills are available
    /// Future work: mentorship
    /// </summary>
    private static List<Project> HaveRequiredSkills(List<Project> projects, List<Contributor> contributors) => projects;

    /// <summary>
    /// Greedily grab the project with the highest score
    /// </summary>
    private static Project PickBestProject(List<Project> feasibleProjects) => feasibleProjects.MaxBy(p => p.Score);

    /// <summary>
    /// Update skill levels when they completed a project at or below their skill level
    /// </summary>
    private static void UpdateContributorSkillLevel(List<Contributor> contributors) { return; }
}
