using HealthCareMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCareMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientView : ContentPage
    {
        PatientViewModel vm;
        public PatientView()
        {
            InitializeComponent();
            vm = new PatientViewModel();
            this.BindingContext = vm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetCommand.Execute(this);
        }


    }
}