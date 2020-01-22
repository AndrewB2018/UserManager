using System.Collections.Generic;
using System.Web.Mvc;

namespace UserManager.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void Merge(this ModelStateDictionary modelState, IDictionary<string, string> dictionary, string prefix)
        {
            foreach (var item in dictionary)
            {
                modelState.AddModelError((string.IsNullOrEmpty(prefix) ? "" : (prefix + ".")) + item.Key, item.Value);
            }
        }
    }
}