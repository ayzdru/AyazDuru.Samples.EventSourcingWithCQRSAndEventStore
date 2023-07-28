using System;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Exceptions
{
    public class UnsupportedColourException : Exception
    {
        public UnsupportedColourException(string code)
            : base($"Colour \"{code}\" is unsupported.")
        {
        }
    }
}