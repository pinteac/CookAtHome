using System.Threading.Tasks;

namespace cootathome.Services
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        void ShowToast(string message);
    }
}
