using System;
using System.Collections.Generic;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
