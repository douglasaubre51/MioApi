namespace MioApi.Dtos;

public record ProjectDto(
    string Title,
    string ShortDesc,
    string Desc,
    string ProjectSpec,
    string Dependencies,
    bool IsFinished,
    bool IsReleased,
    bool IsOngoing);

public class GetProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public string ProjectSpec { get; set; } = string.Empty;

    public int UnfinishedTasksNo { get; set; }
    public bool IsTaskAvailable { get; set; }
}
