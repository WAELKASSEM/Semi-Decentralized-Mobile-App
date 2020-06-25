using HealthCareMobileApp.ViewModels;
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
    public partial class Drugs : ContentPage
    {
        WritePrescriptionViewModel vm;
        public Drugs()
        {
            InitializeComponent();
            this.BindingContext = vm = new WritePrescriptionViewModel();
        }
    }
}