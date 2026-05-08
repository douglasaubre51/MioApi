namespace MioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController(
    TaskRepository taskRepo,
    ProjectRepository projectRepo
) : ControllerBase
{
    private readonly TaskRepository _taskRepo = taskRepo;
    private readonly ProjectRepository _projectRepo = projectRepo;

    [HttpGet("all")]
    public IResult GetAll()
    {
        try
        {
            var tasks = _taskRepo.GetAll();
            if (tasks.Count is 0)
                return Results.BadRequest("Empty list!");

            return Results.Ok(tasks);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("task GetAll error: " + ex.Message);
        }
    }

    [HttpGet("{projectId}/project/all-tasks")]
    public IResult GetAllByProjectId(int projectId)
    {
        try
        {
            var project = _projectRepo.GetById(projectId);
            if (project is null)
                return Results.BadRequest("project doesnt exist!");
            if (project.Tasks!.Count is 0)
                return Results.BadRequest("empty list!");

            List<TaskDtos> taskDtos = [];

            foreach (var task in project.Tasks)
            {
                taskDtos.Add(new TaskDtos(
                    Id: task.Id,
                    Content: task.Content,
                    ProjectId: task.ProjectId,
                    IsDone: task.IsDone
                ));
            }

            return Results.Ok(taskDtos);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("task Get error: " + ex.Message);
        }
    }

    [HttpGet("{taskId}")]
    public IResult Get(int taskId)
    {
        try
        {
            var task = _taskRepo.GetById(taskId);
            if (task is null)
                return Results.BadRequest("task doesnt exist!");

            return Results.Ok(task);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("task Get error: " + ex.Message);
        }
    }

    [HttpPost]
    public IResult Post(TaskDtos task)
    {
        try
        {
            _taskRepo.Add(new Tasks
            {
                ProjectId = task.ProjectId,
                Content = task.Content,
                IsDone = task.IsDone
            });

            return Results.Ok(task);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("task Post error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IResult Put(int id, TaskDtos dto)
    {
        try
        {
            var dbtask = _taskRepo.GetById(id);
            if (dbtask is null)
                return Results.BadRequest("task doesnt exist!");

            dbtask.IsDone = dto.IsDone;
            dbtask.Content = dto.Content;
            _taskRepo.Save();

            return Results.Ok(new { Message = "task updated successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.BadRequest(new { Message = "task update error!" });
        }
    }

    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        var dbtask = _taskRepo.GetById(id);
        if (dbtask is null)
            return Results.BadRequest("task doesnt exist!");

        _taskRepo.Delete(dbtask);
        return Results.Ok(new { Message = "task deleted successfully!" });
    }
}
