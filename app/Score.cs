internal class Score
{
    internal static int Calculate(Output output)
    {
        Dictionary<string, int> nextAvailableDay = new Dictionary<string, int>();

        foreach (var projectPlanning in output.Planning)
        {
            foreach (var contributor in projectPlanning.Contributors)
            {
                nextAvailableDay[contributor.Name] = 0;
            }
        }

        var score = 0;
        foreach (var projectPlanning in output.Planning)
        {
            var project = projectPlanning.Project;
            var startDay = 0;

            foreach (var contributor in projectPlanning.Contributors)
            {
                startDay = Math.Max(nextAvailableDay[contributor.Name], startDay);
            }

            foreach (var contributor in projectPlanning.Contributors)
            {
                nextAvailableDay[contributor.Name] = startDay + project.Duration;
            }

            score += project.Score - Math.Max((startDay + project.Duration) - project.BestBeforeDay, 0);
        }

        return score;
    }
}