using HealthCareMobileApp.ViewModels;
using Org.Apache.Http.Impl.Client;
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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm = new LoginViewModel();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
        
        protected override void OnDisappearing()
        {
            if (AccountManager.Instance().GetActiveAccount() == null)
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}