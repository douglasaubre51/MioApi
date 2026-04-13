namespace MioApi.Repositories;

public class TaskRepository(
    ApplicationDbContext dbContext
)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public List<Tasks> GetAll()
        => [.. _dbContext.Tasks.AsNoTracking()];
    public Tasks? GetById(int taskId)
        => _dbContext.Tasks.SingleOrDefault(task => task.Id == taskId);

    public void Add(Tasks task)
    {
        _dbContext.Tasks.Add(task);
        Save();
    }
    public void Update(Tasks task)
    {
        _dbContext.Tasks.Update(task);
        Save();
    }
    public void Delete(Tasks task)
    {
        _dbContext.Tasks.Remove(task);
        Save();
    }
    public void Save()
        => _dbContext.SaveChanges();
}
