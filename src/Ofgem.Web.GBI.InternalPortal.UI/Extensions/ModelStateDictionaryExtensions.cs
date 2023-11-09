using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ofgem.Web.GBI.InternalPortal.UI.Models;
using System.Text.Json;

namespace Ofgem.Web.GBI.InternalPortal.UI.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static bool IsFieldValid(this ModelStateDictionary modelState, string fieldName)
        {
            return modelState.GetFieldValidationState(fieldName) != ModelValidationState.Invalid;
        }

        public static string Serialize(this ModelStateDictionary modelState)
        {
            var errorList = modelState.Select(kvp => new ModelStateTransferValue
            {
                Key = kvp.Key,
                AttemptedValue = kvp.Value?.AttemptedValue,
                RawValue = kvp.Value?.RawValue,
                ErrorMessages = kvp.Value?.Errors.Select(err => err.ErrorMessage).ToList()
            });

            return JsonSerializer.Serialize(errorList);
        }

        public static void MergeSerializedTransferValues(this ModelStateDictionary modelState, string serializedTransferValues)
        {
            var errorList = JsonSerializer.Deserialize<List<ModelStateTransferValue>>(serializedTransferValues);

            if (errorList == null)
            {
                return;
            }

            foreach (var item in errorList)
            {
                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);

                if (item.ErrorMessages == null)
                {
                    continue;
                }

                foreach (var error in item.ErrorMessages)
                {
                    modelState.AddModelError(item.Key, error);
                }
            }
        }
    }
}
