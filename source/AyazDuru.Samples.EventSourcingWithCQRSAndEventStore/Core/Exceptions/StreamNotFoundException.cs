using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions
{
    public class StreamNotFoundException : Exception
    {
        public StreamNotFoundException() : base("Stream not found")
        { }
        public StreamNotFoundException(string message) : base(message)
        { }
    }
}
