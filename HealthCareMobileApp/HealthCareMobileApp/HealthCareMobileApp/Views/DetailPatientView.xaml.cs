using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCareMobileApp.ViewModels;

namespace HealthCareMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [QueryProperty("Address", "address")]
    public partial class DetailPatientView : ContentPage
    {
        DetailsPatientViewModel vm;
        public string Address
        {
            set
            {
                BindingContext = vm = new DetailsPatientViewModel(value);
            }
        }
        public DetailPatientView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}