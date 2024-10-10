using TaskService.Domain.Projections;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.Repositories;

/// <summary>
/// Список всех проекций для тестов (SINGLETON)
/// </summary>
public class CommonListStorage
{
    public List<GlobalTaskProjection> GlobalProjections { get; set; } = [];
    public List<TaskProjection> TaskProjections { get; set; } = [];
    public List<SentToCheckTaskProjection> SentToCheckProjections { get; set; } = [];
}