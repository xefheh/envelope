using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envelope.Common.Messages.ResponseMessages.Tags
{
    public class TagResponseMessage
    {
        public string[] Tags { get; set; } = Array.Empty<string>();
    }
}
