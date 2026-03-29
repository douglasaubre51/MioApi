namespace MioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(ProjectRepository projectRepo) : ControllerBase
{
    private readonly ProjectRepository _projectRepo = projectRepo;

    [HttpGet("all")]
    public IResult GetAll()
    {
        try
        {
            var projects = _projectRepo.GetAll();
            if (projects.Count is 0)
                return Results.BadRequest("Empty list!");

            return Results.Ok(projects);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project GetAll error: " + ex.Message);
        }
    }

    [HttpGet("{projectId}")]
    public IResult Get(int projectId)
    {
        try
        {
            var project = _projectRepo.GetById(projectId);
            if (project is null)
                return Results.BadRequest("Project doesnt exist!");

            return Results.Ok(project);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project Get error: " + ex.Message);
        }
    }

    [HttpPost]
    public IResult Post(ProjectDto project)
    {
        try
        {
            _projectRepo.Add(new Project
            {
                Title = project.Title,
                ProjectSpec = project.ProjectSpec,
                ShortDesc = project.ShortDesc,
                Desc = project.Desc,
                Dependencies = project.Dependencies
            });

            return Results.Ok(project);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project Post error: " + ex.Message);
        }

    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
