internal class SampleHashCode
{
    public static Output CreateOutput(Input input)
    {
        var output = new Output();

        var planning = new ProjectPlanning(input.Projects.First(x => x.Name == "WebServer"));
        planning.AddContributor(input.Contributors.First(x => x.Name == "Bob"));
        planning.AddContributor(input.Contributors.First(x => x.Name == "Anna"));

        output.AddProjectPlanning(planning);

        planning = new ProjectPlanning(input.Projects.First(x => x.Name == "Logging"));
        planning.AddContributor(input.Contributors.First(x => x.Name == "Anna"));

        output.AddProjectPlanning(planning);

        planning = new ProjectPlanning(input.Projects.First(x => x.Name == "WebChat"));
        planning.AddContributor(input.Contributors.First(x => x.Name == "Maria"));
        planning.AddContributor(input.Contributors.First(x => x.Name == "Bob"));

        output.AddProjectPlanning(planning);

        return output;
    }
}
