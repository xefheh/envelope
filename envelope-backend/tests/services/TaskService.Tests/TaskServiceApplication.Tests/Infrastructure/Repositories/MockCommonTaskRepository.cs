using TaskService.Application.Repositories.ReadOnlyRepositories;
using TaskService.Application.Repositories.WriteOnlyRepositories;
using TaskService.Domain.Projections;

namespace TaskService.Tests.TaskServiceApplication.Tests.Infrastructure.Repositories;

/// <summary>
/// Общий репозиторий для всех контекстов (тесты)
/// </summary>
public class MockCommonTaskRepository : 
    IGlobalProjectionReadOnlyRepository,
    IGlobalProjectionWriteOnlyRepository,
    ITaskProjectionReadOnlyRepository,
    ITaskProjectionWriteOnlyRepository,
    ISentToCheckProjectionReadOnlyRepository,
    ISentToCheckProjectionWriteOnlyRepository
{
    
    private readonly CommonListStorage _commonListStorage;

    public MockCommonTaskRepository(CommonListStorage commonListStorage)
    {
        _commonListStorage = commonListStorage;
    }
    
    async Task<GlobalTaskProjection?> IGlobalProjectionReadOnlyRepository.GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.GlobalProjections.FirstOrDefault(p => p.Id == projectionId), cancellationToken);

    async Task<ICollection<SentToCheckTaskProjection>> ISentToCheckProjectionReadOnlyRepository.GetProjectionsAsync(CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.SentToCheckProjections, cancellationToken);

    async Task<SentToCheckTaskProjection?> ISentToCheckProjectionReadOnlyRepository.GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.SentToCheckProjections.FirstOrDefault(p => p.Id == projectionId), cancellationToken);

    async Task<ICollection<GlobalTaskProjection>> IGlobalProjectionReadOnlyRepository.GetProjectionsAsync(CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.GlobalProjections, cancellationToken);

    public async Task AddAsync(GlobalTaskProjection projection, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.GlobalProjections.Add(projection), cancellationToken);

    public async Task AddAsync(TaskProjection projection, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.TaskProjections.Add(projection), cancellationToken);

    public async Task AddAsync(SentToCheckTaskProjection projection, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.SentToCheckProjections.Add(projection), cancellationToken);

     async Task ISentToCheckProjectionWriteOnlyRepository.RemoveAsync(Guid id, CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.SentToCheckProjections.RemoveAll(p => p.Id == id), cancellationToken);

     public async Task UpdateAsync(SentToCheckTaskProjection projection, CancellationToken cancellationToken) =>
         await Task.Run(() =>
         {
             if (_commonListStorage.SentToCheckProjections.All(p => p.Id != projection.Id)) return;
             _commonListStorage.SentToCheckProjections.RemoveAll(p => p.Id == projection.Id);
             _commonListStorage.SentToCheckProjections.Add(projection);
         }, cancellationToken);

    async Task ITaskProjectionWriteOnlyRepository.RemoveAsync(Guid id, CancellationToken cancellationToken)  =>
        await Task.Run(() => _commonListStorage.TaskProjections.RemoveAll(p => p.Id == id), cancellationToken);

    public async Task UpdateAsync(TaskProjection projection, CancellationToken cancellationToken) =>
        await Task.Run(() =>
        {
            if (_commonListStorage.TaskProjections.All(p => p.Id != projection.Id)) return;
            _commonListStorage.TaskProjections.RemoveAll(p => p.Id == projection.Id);
            _commonListStorage.TaskProjections.Add(projection);
        }, cancellationToken);

    async Task IGlobalProjectionWriteOnlyRepository.RemoveAsync(Guid id, CancellationToken cancellationToken)  =>
        await Task.Run(() => _commonListStorage.GlobalProjections.RemoveAll(p => p.Id == id), cancellationToken);

    public async Task UpdateAsync(GlobalTaskProjection projection, CancellationToken cancellationToken) =>
        await Task.Run(() =>
        {
            if (_commonListStorage.GlobalProjections.All(p => p.Id != projection.Id)) return;
            _commonListStorage.GlobalProjections.RemoveAll(p => p.Id == projection.Id);
            _commonListStorage.GlobalProjections.Add(projection);
        }, cancellationToken);


    public async Task<TaskProjection?> GetProjectionAsync(Guid projectionId, CancellationToken cancellationToken)  =>
        await Task.Run(() => _commonListStorage.TaskProjections.FirstOrDefault(p => p.Id == projectionId), cancellationToken);

    public async Task<ICollection<TaskProjection>> GetProjectionsAsync(Guid authorId,
        CancellationToken cancellationToken) =>
        await Task.Run(() => _commonListStorage.TaskProjections.Where(p => p.Author == authorId).ToList() , cancellationToken);
}