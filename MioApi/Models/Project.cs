namespace MioApi.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public string ProjectSpec { get; set; } = string.Empty;
    public string Dependencies { get; set; } = string.Empty;
}
