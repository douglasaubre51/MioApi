namespace MioApi.Models;

public class Tasks
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public string Content { get; set; }
    public bool IsDone { get; set; }
}
