namespace TaskService.Domain.Enums;

/// <summary>
/// Состояние задачи 
/// </summary>
public enum TaskGlobalState
{
    /// <summary>
    /// Локальная (не отправляется в общий банк)
    /// </summary>
    Local,
    
    /// <summary>
    /// Ожидает проверки 
    /// </summary>
    WaitCheck,
    
    /// <summary>
    /// Глобальная
    /// </summary>
    Global
}