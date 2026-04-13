namespace MioApi.Models;

public class Tasks
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public string Content { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}