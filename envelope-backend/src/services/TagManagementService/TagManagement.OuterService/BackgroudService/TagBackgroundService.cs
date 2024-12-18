using Envelope.Common.Messages.EventMessages.Tags;
using Envelope.Common.Messages.RequestMessages.Tags;
using Envelope.Common.Queries;
using Envelope.Integration.Interfaces;
using TagManagement.Application.Services;

namespace TagManagement.OuterService.BackgroudService
{
    public class TagBackgroundService : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly TagService _service;

        public TagBackgroundService(IMessageBus messageBus, TagService service)
        {
            _messageBus = messageBus;
            _service = service;
        }

        public async Task<string[]> ResponseMessage(GetTagForEntityMessage message)
        {
            var tags = await _service.GetTagsForEntityAsync((Domain.Enums.TagType)((int)message.TagType), message.EntityId);

            return tags.Value!.Select(tag => tag.TagName)!.ToArray()!;
        }

        public async Task HandlePublishAsync(AddTagMessage message)
        {
            await _service.AddTagAsync(new Application.Request.TagRequest() { EntityId = message.EntityId, TagType = (Domain.Enums.TagType)((int)message.TagType), TagName=message.Name });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageBus.SubscribeResponseAsync<GetTagForEntityMessage, string[]>
                (QueueNames.GetTagQueue, ResponseMessage);
            await _messageBus.SubscribeAsync<AddTagMessage>(QueueNames.AddTagQueue, HandlePublishAsync);
        }
    }
}
