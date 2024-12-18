using Envelope.Common.Enums;

namespace Envelope.Common.Messages.EventMessages.Tags
{
    public class AddTagMessage
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = null!;
        public TagType TagType { get; set; }
    }
}
