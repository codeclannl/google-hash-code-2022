string inputFileName = args[0];
string outputFileName = inputFileName.Replace("input", "output").Replace(".in", ".out");

Input input = ReadInput(args[0]);

Output output = HashCode.CreateOutput(input);

File.WriteAllLines(outputFileName, CreateOutputLines(output));

int score = Score.Calculate(output);

Console.WriteLine($"{inputFileName}: {score}");

Input ReadInput(string fileName)
{
    string[] lines = File.ReadAllLines(fileName);
    string[] info = lines[0].Split(' ');
    int contributers = int.Parse(info[0]);
    int projects = int.Parse(info[1]);

    Input input = new();

    int offset = 1;
    for (int i = 0; i < contributers; i++)
    {
        string[] personInfo = lines[offset++].Split(' ');
        string name = personInfo[0];
        int skillCount = int.Parse(personInfo[1]);

        Contributor contributer = new Contributor(name);
        for (int j = 0; j < skillCount; j++)
        {
            string[] skillInfo = lines[offset++].Split(' ');
            string skillName = skillInfo[0];
            int skillLevel = int.Parse(skillInfo[1]);
            contributer.AddSkill(skillName, skillLevel);
        }

        input.AddContributer(contributer);
    }

    for (int i = 0; i < projects; i++)
    {
        string[] projectInfo = lines[offset++].Split(' ');
        string name = projectInfo[0];
        int duration = int.Parse(projectInfo[1]);
        int score = int.Parse(projectInfo[2]);
        int bestBeforeDay = int.Parse(projectInfo[3]);
        int skillCount = int.Parse(projectInfo[4]);

        Project project = new(name, duration, score, bestBeforeDay); ;
        for (int j = 0; j < skillCount; j++)
        {
            string[] skillInfo = lines[offset++].Split(' ');
            string skillName = skillInfo[0];
            int skillLevel = int.Parse(skillInfo[1]);
            project.AddSkill(skillName, skillLevel);
        }

        input.AddProject(project);
    }

    return input;
}

IEnumerable<string> CreateOutputLines(Output output)
{
    yield return output.Planning.Count.ToString();

    foreach (var projectPlanning in output.Planning)
    {
        yield return projectPlanning.Project.Name;

        yield return string.Join(' ', projectPlanning.Contributors.Select(x => x.Name));
    }
}