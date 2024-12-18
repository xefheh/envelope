using Envelope.Common.Enums;

namespace Envelope.Common.Messages.RequestMessages.Tags
{
    public class GetTagForEntityMessage
    {
        public Guid EntityId { get; set; }
        public TagType TagType { get; set; }
    }
}
