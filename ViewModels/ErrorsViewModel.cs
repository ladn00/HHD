using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HHD.ViewModels
{
    public class ErrorsViewModel
    {
        public ErrorsViewModel(ModelStateDictionary modelState)
        {
            foreach (var k in modelState.Keys)
            {
                Errors.Add(k, modelState[k]?.Errors[0].ErrorMessage ?? "");
            }
        }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}
