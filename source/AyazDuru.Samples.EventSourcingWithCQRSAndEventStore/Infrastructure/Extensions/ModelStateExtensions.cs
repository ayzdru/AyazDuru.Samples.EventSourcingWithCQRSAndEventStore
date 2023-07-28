﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.ValueObjects;
using System.Linq;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetModelStateErrors(this ModelStateDictionary modelState)
        {
            return string.Join("\n", modelState.Values.Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        }
    }
}
