namespace MioApi.Dtos;

public record TaskDtos(
    int Id,
    int ProjectId,
    string Content,
    bool IsDone);