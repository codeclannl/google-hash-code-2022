var input = ReadInput(args[0]);

var output = HashCode.CreateOutput(input);

Console.WriteLine(input.Contributors[0].Name);

Input ReadInput(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var info = lines[0].Split(' ');
    var contributers = int.Parse(info[0]);
    var projects = int.Parse(info[1]);

    var input = new Input();

    var offset = 1;
    for (int i = 0; i < contributers; i++)
    {
        var personInfo = lines[offset++].Split(' ');
        var name = personInfo[0];
        var skillCount = int.Parse(personInfo[1]);

        var contributer = new Contributor(name);
        for (int j = 0; j < skillCount; j++)
        {
            var skillInfo = lines[offset++].Split(' ');
            var skillName = skillInfo[0];
            var skillLevel = int.Parse(skillInfo[1]);
            contributer.AddSkill(skillName, skillLevel);
        }

        input.AddContributer(contributer);
    }

    for (int i = 0; i < projects; i++)
    {
        var projectInfo = lines[offset++].Split(' ');
        var name = projectInfo[0];
        var duration = int.Parse(projectInfo[1]);
        var score = int.Parse(projectInfo[2]);
        var bestBeforeDay = int.Parse(projectInfo[3]);
        var skillCount = int.Parse(projectInfo[4]);

        var project = new Project(name, duration, score, bestBeforeDay); ;
        for (int j = 0; j < skillCount; j++)
        {
            var skillInfo = lines[offset++].Split(' ');
            var skillName = skillInfo[0];
            var skillLevel = int.Parse(skillInfo[1]);
            project.AddSkill(skillName, skillLevel);
        }

        input.AddProject(project);
    }

    return input;
}