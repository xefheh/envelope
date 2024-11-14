### 1. Envelope.Common

## ENVELOPE.COMMON  - библиотека для общих структур/классов и тд (использующихся во всех сервисах)

Структура

- /Exceptions - общие исключения, которые могут быть встречены во множестве сервисов
- /Messages - сообщения для обмена через RabbitMq
- /ResultPatter - паттерн результата (OnSuccess/OnFailure)




### 2. Envelope.Integration

## ENVELOPE.INTEGRATION - ШИНА СООБЩЕНИЙ ДЛЯ ОБЩЕНИЯ МЕЖДУ МИКРОСЕРВИСАМИ

Библиотека представляет собой реализацию шины сообщений с использованием RabbitMq и библиотеки RabbitMq.Client.

### Составные части:

- Интерфейс IMessageBus описывающий следующуие методы:
1. Task <b>SubscribeAsync</b> <TMessage>(string queueName,
     Func<TMessage, Task> handleAsync) - подписаться на получение сообщений типа <b>TMessage</b> из очереди с именем <b>queueName</b>. <b>handleAsync</b> - функция, реализующая обработку полученного сообщения message (может быть объявлена как метод класса, может как анонимная функция async message => { ... } )
2. Task <b>PublishAsync</b> <TMessage>(string queueName,
   TMessage message) - отправить сообщение типа <b>TMessage</b> в очередь <b>queueName</b>, в дальнейшем обрабатывается в handler метода SubscribeAsync (если такой был объявлен в одном из сервисов, в противном случае сообщение останется в брокере)
3. Task<TResponse> <b>SendWithRequestAsync</b> <TMessage, TResponse>(string queueName,
   TMessage message,
   int timeoutInMs) - отправить запрос типа <b>TMessage</b> на получение ответа типа <b>TResponse</b>. Запрос отправляется в очередь <b>queueName</b>, ответ будет отправлен в очередь <b>queueName_reply</b>. <b>timeoutInMs</b> - время ожидания в мс. По умолчанию 30с (30000 мс).
4. Task <b>SubscribeResponseAsync</b> <TMessage, TResponse>(string queueName, Func<TMessage, Task<TResponse>> handleAsync) - подписаться на получение запросов типа <b>TMessage</b> и выдачу ответов типа <b>TResponse</b>. <b>queueName</b> - название очереди запросов, ответы кладутся 
в очередь <b>queueName_reply</b>. <b>handleAsync</b> - аналогично обработчику subscribeAsync, только используется для формирования ответа исходя из запроса.

### Добавление в проект:

```csharp
services.AddIntegrationMessageBus(configuration);
```

По умолчанию используется секция: "RabbitMqOptions", для использования своей секции:
```csharp
services.AddIntegrationMessageBus(configuration, sectionName);
```

формат секции для RabbitMq:
```json
{
  "RabbitMqOptions": {
    "Hostname": "hostname",
    "Port": "0000",
    "Login": "name",
    "Password": "pwd"
  }
}
```

### Примеры:

#### 1. Пара Subscribe/Publish:

```csharp
IMessageBus ConsumerMessageBus;
IMessageBus ProducerMessageBus;


const string queueName = "test_queue";

// CONSUMER

await ConsumerMessageBus.SubscribeAsync<Message>(queueName, message =>
{
    receivedMessage = message;
    Console.WriteLine($"Received message: {receivedMessage}");
    return Task.CompletedTask;
});


// PRODUCER

var requestMessage = new Message { Text = "This is a test message" };
await ProducerMessageBus.PublishAsync(queueName, requestMessage);
```

#### 2. Пара SendWithRequestAsync/SubscribeResponseAsync:

```csharp
IMessageBus ConsumerMessageBus;
IMessageBus ProducerMessageBus;


const string queueName = "test_queue";

// CONSUMER

await ConsumerMessageBus.SubscribeResponseAsync<Message, string>(queueName, message =>
{
    Console.WriteLine($"Received message: {message}");
    return Task.FromResult(new string(message.Text.Reverse().ToArray()));
});


// PRODUCER

var requestMessage = new Message { Text = "This is a test message" };
var response = await ProducerMessageBus.SendWithRequestAsync<Message, string>(queueName, requestMessage);
Console.WriteLine(response); // egassem tset a si sihT
```
