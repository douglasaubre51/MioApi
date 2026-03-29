namespace MioApi.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public List<Project> GetAll()
        => [.. _dbContext.Projects];
    public Project? GetById(int projectId)
        => _dbContext.Projects.SingleOrDefault(project => project.Id == projectId);

    public void Add(Project project)
    {
        _dbContext.Projects.Add(project);
        Save();
    }
    public void Update(Project project)
    {
        _dbContext.Projects.Update(project);
        Save();
    }
    public void Save()
        => _dbContext.SaveChanges();
}
