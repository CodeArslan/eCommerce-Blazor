using Microsoft.JSInterop;

namespace eCommerce.Services.Extensions
{
    public static class IJSRuntimeExtension
    {
        public static async Task ToastrSuccess(this IJSRuntime _JS,string message)
        {
            await _JS.InvokeVoidAsync("ShowToastr","Success",message);
        }
        public static async Task ToastrError(this IJSRuntime _JS, string message)
        {
            await _JS.InvokeVoidAsync("ShowToastr", "Error", message);
        }
    }
}
