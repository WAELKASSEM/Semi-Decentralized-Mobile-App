using HealthCareMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Support.Design.Widget;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;

namespace HealthCareMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PrescriptionsView : ContentPage
    {
        PrescriptionsViewModel vm;
        public PrescriptionsView()
        {
            vm = new PrescriptionsViewModel();
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}