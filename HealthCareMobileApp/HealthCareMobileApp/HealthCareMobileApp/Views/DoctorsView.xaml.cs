using HealthCareMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCareMobileApp.Views
{
    /// <summary>
    /// A view(Content Page) that shows a list of a patient's doctor,
    /// with the possibility to add new doctors.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorsView : ContentPage
    {
        DoctorsViewModel vm = new DoctorsViewModel();
        public DoctorsView()
        {
            InitializeComponent();
            this.BindingContext = vm;

        }
    }
}