internal class HashCode
{
    public readonly Dictionary<string, List<Contributor>> skillsList = new Dictionary<string, List<Contributor>>();
    public readonly Dictionary<string, int> highestOfSkill = new Dictionary<string, int>();
    public Output CreateOutput(Input input)
    {
        List<Project> todoProjects = input.Projects.ToList();

        List<string> allSkills = todoProjects.SelectMany(c => c.SkillRequirements.Select(s => s.Name)).Distinct().ToList();
        List<Contributor> contributors = input.Contributors.Select(c => new Contributor(c, allSkills)).ToList();
        foreach (string skill in allSkills)
        {
            skillsList.Add(skill, contributors.Where(c => c.Skills.Any(s => s.Name == skill)).OrderBy(c => c.Skills.First(s => s.Name == skill).Level).ToList());
            highestOfSkill[skill] = skillsList[skill].Select(c => c.SkillLevel[skill]).Max();
        }
        Output output = new();
        while (todoProjects.Any())
        {
            List<Project> feasibleProjects = CalculateFeasible(todoProjects);
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


    /// <summary>
    /// Find all projects where required skills are available.
    /// future: filter out projects with score 0
    /// futurefuture: consider improving skills through 0 score projects
    /// </summary>
    private List<Project> CalculateFeasible(List<Project> projects)
    {
        projects = HaveRequiredSkills(projects);
        return projects;
    }

    /// <summary>
    /// calculate whether the skills are available
    /// Future work: mentorship
    /// TODO: parallel projects
    /// </summary>
    private List<Project> HaveRequiredSkills(List<Project> projects)
    {
        List<Project> projectsWithSkills = new();
        projects.ForEach(project =>
        {
            HashSet<string> contributorsForProject = new();
            bool unmatchable = false;
            project.SkillRequirements.ToList().ForEach(sr =>
            {
                if (highestOfSkill[sr.Name] < sr.Level)
                {
                    unmatchable = true;
                    return;
                }
                //Greedy
                Contributor? cont = skillsList[sr.Name].Where(c => c.SkillLevel[sr.Name] >= sr.Level && !contributorsForProject.Any(cfp => cfp == c.Name)).FirstOrDefault();
                if (cont is null)
                {
                    unmatchable = true;
                    return;
                }
                contributorsForProject.Add(cont.Name);
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
    private Project PickBestProject(List<Project> feasibleProjects) => feasibleProjects.MaxBy(p => p.PossibleScore)!;

    private List<Contributor> SelectBestCandidates(ProjectPlanning projectPlanning, List<Contributor> contributors)
    {
        List<Contributor> contributorsForProject = new();
        projectPlanning.Project.SkillRequirements.ToList().ForEach(sr =>
        {
            Contributor cont = contributors.Find(c => c.SkillLevel[sr.Name] >= sr.Level && !contributorsForProject.Any(cfp => cfp.Name == c.Name))!;
            contributorsForProject.Add(cont);
        });
        return contributorsForProject;
    }

    /// <summary>
    /// Update skill levels when they completed a project at or below their skill level
    /// </summary>
    private void UpdateContributorSkillLevel(ProjectPlanning planning, List<Contributor> contributors)
    {
        for (int i = 0; i < planning.Project.SkillRequirements.Count; i++)
        {
            Skill requirement = planning.Project.SkillRequirements[i];
            if (requirement.Level >= contributors[i].SkillLevel[requirement.Name])
            {
                contributors[i].SkillLevel[requirement.Name]++;

                if (highestOfSkill[requirement.Name] < contributors[i].SkillLevel[requirement.Name])
                {
                    highestOfSkill[requirement.Name] = contributors[i].SkillLevel[requirement.Name];
                }
            }
        }
    }
}
