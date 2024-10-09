namespace TaskService.Persistence.EventStore.Queries;

public class TaskEventStoreQueries
{
    public const string AddTaskInStoreQuery = """
                                                INSERT INTO TaskEvents(id, versionId, date, eventData)
                                                VALUES (@id, @versionId, @date, @eventData);
                                              """;
    
    public const string GetTaskEventsFromStoreQuery = """
                                                SELECT eventData FROM TaskEvents
                                                WHERE id = @id
                                                ORDER BY versionId ASC
                                              """;
    
    public const string GetLastTaskEventFromStoreQuery = """
                                                        SELECT eventData FROM TaskEvents
                                                        WHERE id = @id
                                                        ORDER BY versionId DESC 
                                                        LIMIT 1
                                                      """;
}