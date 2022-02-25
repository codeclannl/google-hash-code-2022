internal class HashCode
{
    public static Output CreateOutput(Input input)
    {
        List<Project> todoProjects = input.Projects.ToList();

        List<Contributor> contributors = input.Contributors.Select(c => new Contributor(c)).ToList();
        Output output = new();
        while (todoProjects.Any())
        {
            List<Project> feasibleProjects = CalculateFeasible(todoProjects, contributors);
            if (!feasibleProjects.Any())
            {
                break;
            }
            Project bestProject = PickBestProject(feasibleProjects);

            ProjectPlanning projectPlanning = new(bestProject);

            List<Contributor> bestCandidates = SelectBestCandidates(projectPlanning, contributors);
            projectPlanning.AddContributors(bestCandidates.Select(c => input.Contributors.First(ic => ic.Name == c.Name)).ToList());

            todoProjects.Remove(bestProject);
            UpdateContributorSkillLevel(projectPlanning, bestCandidates);
            output.AddProjectPlanning(projectPlanning);
        }

        return output;
    }

    private static List<Contributor> SelectBestCandidates(ProjectPlanning projectPlanning, List<Contributor> contributors)
    {
        List<Contributor> contributorsForProject = new();
        projectPlanning.Project.SkillRequirements.ToList().ForEach(sr =>
        {
            Contributor cont = contributors.Find(c => c.Skills.Any(s => s.Name == sr.Name && s.Level >= sr.Level && !contributorsForProject.Any(cfp => cfp.Name == c.Name)))!;
            contributorsForProject.Add(cont);
        });
        return contributorsForProject;
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
    /// TODO: parallel projects
    /// </summary>
    private static List<Project> HaveRequiredSkills(List<Project> projects, List<Contributor> contributors)
    {
        List<Project> projectsWithSkills = new();
        projects.ForEach(project =>
        {
            List<Contributor> contributorsForProject = new();
            bool unmatchable = false;
            project.SkillRequirements.ToList().ForEach(sr =>
            {
                //Greedy
                Contributor? cont = contributors.Find(c => c.Skills.Any(s => s.Name == sr.Name && s.Level >= sr.Level && !contributorsForProject.Any(cfp => cfp.Name == c.Name)));
                if (cont is null)
                {
                    unmatchable = true;
                    return;
                }
                contributorsForProject.Add(cont);
            });

            if (!unmatchable)
            {
                projectsWithSkills.Add(project);
            }
        });
        return projectsWithSkills;
    }

    /// <summary>
    /// Greedily grab the project with the highest score
    /// </summary>
    private static Project PickBestProject(List<Project> feasibleProjects) => feasibleProjects.MaxBy(p => p.Score)!;

    /// <summary>
    /// Update skill levels when they completed a project at or below their skill level
    /// </summary>
    private static void UpdateContributorSkillLevel(ProjectPlanning planning, List<Contributor> contributors)
    {
        for (int i = 0; i < planning.Project.SkillRequirements.Count; i++)
        {
            Skill requirement = planning.Project.SkillRequirements[i];
            if (requirement.Level >= contributors[i].Skills.ToList().Find(s => s.Name == requirement.Name)!.Level)
            {
                contributors[i].Skills.ToList().Find(s => s.Name == requirement.Name)!.Level++;
            }
        }
    }
}
