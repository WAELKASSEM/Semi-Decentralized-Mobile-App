using HealthCareMobileApp.ViewModels.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCareMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingRequests : ContentPage
    {
        PendingRequestsViewModel vm;
        public PendingRequests()
        {
            InitializeComponent();
            vm = new PendingRequestsViewModel();
            this.BindingContext = vm;
        }

    }
}