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
            List<Contributor> bestCandidates = SelectBestCandidates(projectPlanning, contributors);
            projectPlanning.AddContributors(bestCandidates);

            projects.Remove(bestProject);
            UpdateContributorSkillLevel(contributors);
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
    private static void UpdateContributorSkillLevel(List<Contributor> contributors) { return; }
}
