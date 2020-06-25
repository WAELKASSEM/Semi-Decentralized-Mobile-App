using HealthCareMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCareMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificationsPage : ContentPage
    {
        ModificationsViewModel vm;
        public ModificationsPage()
        {
            InitializeComponent();
            this.BindingContext = vm = new ModificationsViewModel();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetModifications();
        }
    }
}