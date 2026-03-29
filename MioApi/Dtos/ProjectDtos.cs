namespace MioApi.Dtos;

public record ProjectDto(
    string Title,
    string ShortDesc,
    string Desc,
    string ProjectSpec,
    string Dependencies);
