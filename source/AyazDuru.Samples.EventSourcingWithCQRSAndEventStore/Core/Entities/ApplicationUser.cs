using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
