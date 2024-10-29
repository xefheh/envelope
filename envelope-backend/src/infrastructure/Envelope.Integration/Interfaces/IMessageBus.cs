namespace Envelope.Integration.Interfaces;

/// <summary>
/// Интерфейс шины сообщений для сервисов
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// Подписаться на получение сообщений из очереди
    /// </summary>
    /// <param name="queueName">Наименование очереди</param>
    /// <param name="handleAsync">Функция обработки полученого сообщения</param>
    /// <typeparam name="TMessage">Тип сообщений (его поля)</typeparam>
    /// <returns>Таск для асинхронности</returns>
    Task SubscribeAsync<TMessage>(string queueName,
        Func<TMessage, Task> handleAsync);
    
    /// <summary>
    /// Опубликовать сообщение в очередь
    /// </summary>
    /// <param name="queueName">Наименование очереди</param>
    /// <param name="message">Сообщение, которое необходимо отправить в очередь</param>
    /// <typeparam name="TMessage">Тип сообщения для отправления в очередь (его поля)</typeparam>
    /// <returns>Таск для асинхронности</returns>
    Task PublishAsync<TMessage>(string queueName,
        TMessage message);

    /// <summary>
    /// Отправить сообщение в очередь с ожиданием ответа от брокера сообщений
    /// </summary>
    /// <param name="queueName">Наименование очереди</param>
    /// <param name="message">Сообщение, которое необходимо отправить в очередь</param>
    /// <param name="timeoutInMs">Время данное для ответа</param>
    /// <typeparam name="TMessage">Тип сообщения для отправления в очередь</typeparam>
    /// <typeparam name="TResponse">Тип ответа полученного из брокера</typeparam>
    /// <returns>Ответ из очереди брокера</returns>
    Task<TResponse> SendWithRequestAsync<TMessage, TResponse>(string queueName,
        TMessage message,
        int timeoutInMs);

    /// <summary>
    /// Ответить на сообщение (реакция на SendWithRequest) при получении из брокера сообщений
    /// </summary>
    /// <param name="queueName">Наименование очереди</param>
    /// <param name="handleAsync">Обработка полученого сообщения с возвратом ответа</param>
    /// <typeparam name="TResponse">Тип ответа</typeparam>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    /// <returns>Таска асинхронности</returns>
    Task SubscribeResponseAsync<TMessage, TResponse>(string queueName,
        Func<TMessage, Task<TResponse>> handleAsync);
}