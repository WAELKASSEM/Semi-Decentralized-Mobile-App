using Android.Locations;
using HealthCareMobileApp.ViewModels;
using HealthCareMobileApp.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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