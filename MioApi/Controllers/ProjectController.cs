namespace MioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(
    ProjectRepository projectRepo,
    ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    private readonly ProjectRepository _projectRepo = projectRepo;

    [HttpGet("all")]
    public IResult GetAll()
    {
        try
        {
            var projects = _projectRepo.GetAll();
            if (projects.Count is 0) return Results.BadRequest("Empty list!");

            return Results.Ok(projects);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.BadRequest("Project GetAll error: " + ex.Message);
        }
    }

    // For ANEMONE CLIENT
    [HttpGet("all/unfinished/task-count")]
    public IResult GetAllWithUnfinishedTaskCount()
    {
        try
        {
            var projects = _projectRepo.GetQueryable()
                                .Include(project => project.Tasks)
                                .AsNoTracking()
                                .ToList();

            if (projects.Count is 0) return Results.BadRequest("Empty list!");

            List<GetProjectDto> projectDtos = [];
            foreach(var project in projects)
            {
                GetProjectDto dto = new()
                {
                    Id = project.Id,
                    Title = project.Title,
                    ShortDesc = project.ShortDesc,
                    Desc = project.Desc,
                    ProjectSpec = project.ProjectSpec
                };

                projectDtos.Add(dto);

                // Before task prop is init!
                if (project.Tasks is null) continue;

                // Count unfinished tasks!
                int taskCount = project.Tasks.Count(project => project.IsDone == false);
                if (taskCount is 0) continue;

                dto.UnfinishedTasksNo = taskCount;
                dto.IsTaskAvailable = true;
            }

            return Results.Ok(projectDtos);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.BadRequest("Project GetAllWithUnfinishedTaskCount error: " + ex.Message);
        }
    }

    [HttpGet("all/bookmarks")]
    public IResult GetAllBookmarked()
    {
        try
        {
            var projects = _projectRepo.GetAll()
            .Where(project => project.IsBookmarked == true)
            .ToList();

            if (projects.Count is 0)
            return Results.BadRequest("Empty list!");

            return Results.Ok(projects);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project GetAllBookmarks error: " + ex.Message);
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
                    Dependencies = project.Dependencies,
                    IsFinished = project.IsFinished,
                    IsOngoing = project.IsOngoing,
                    IsReleased = project.IsReleased
                });

            return Results.Ok(project);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project Post error: " + ex.Message);
        }

    }

    [HttpPut("{id}")]
    public IResult Put(int id, Project project)
    {
        try
        {
            Project? dbProject = _projectRepo.GetById(id);
            if (dbProject is null)
            return Results.BadRequest("Project doesnt exist!");

            _context.Entry(dbProject).CurrentValues.SetValues(project);
            _context.SaveChanges();

            return Results.Ok(new { Message = "Project updated successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.BadRequest(new { Message = "Project update error!" });
        }
    }

    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        try
        {
            Project? dbProject = _projectRepo.GetById(id);
            if (dbProject is null)
            return Results.BadRequest("Project doesnt exist!");

            _projectRepo.Delete(dbProject);

            return Results.Ok(new { Message = "Project deleted successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Project Delete error: " + ex.Message);
            return Results.InternalServerError("Project Delete error!");
        }
    }

    [HttpGet("{id}/add-bookmark")]
    public IResult AddBookmark(int id)
    {
        try
        {
            Project? dbProject = _projectRepo.GetById(id);
            if (dbProject is null)
            return Results.BadRequest("Project doesnt exist!");

            dbProject.IsBookmarked = true;
            _projectRepo.Save();

            return Results.Ok(new { Message = "Project bookmarked successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("AddBookmark error: " + ex.Message);
            return Results.InternalServerError("Add project bookmark error!");
        }
    }

    [HttpGet("{id}/remove-bookmark")]
    public IResult RemoveBookmark(int id)
    {
        try
        {
            Project? dbProject = _projectRepo.GetById(id);
            if (dbProject is null)
            return Results.BadRequest("Project doesnt exist!");

            dbProject.IsBookmarked = false;
            _projectRepo.Save();

            return Results.Ok(new { Message = "Project bookmark removed successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("RemoveBookmark error: " + ex.Message);
            return Results.InternalServerError("Remove project bookmark error!");
        }
    }

    [HttpGet("search/{projectTitle}")]
    public IResult Search(string projectTitle)
    {
        try
        {
            Console.WriteLine("searched project: " + projectTitle);
            var projects = _projectRepo.SearchByTitle(projectTitle);
            if (projects.Count is 0)
            return Results.BadRequest("Empty list!");

            return Results.Ok(projects);
        }
        catch (Exception ex)
        {
            return Results.BadRequest("Project GetAll error: " + ex.Message);
        }
    }
}
