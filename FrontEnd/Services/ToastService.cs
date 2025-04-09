namespace FrontEnd.Services
{
    public class ToastService
    {
        public event Action<string, string, ToastLevel> OnShow;

        public void ShowToast(string title, string message, ToastLevel level = ToastLevel.Info)
        {
            OnShow?.Invoke(title, message, level);
        }
    }

    public enum ToastLevel
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }
}
