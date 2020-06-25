using HealthCareMobileApp.Views;
using Xamarin.Forms;
using HealthCareMobileApp.LocalDatabase;

namespace HealthCareMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new SynchronousInit();
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("details", typeof(DetailPatientView));
            var cred = await DatabaseInstance.Database.GetCredentials();
            var manager = AccountManager.Instance();
            if (cred != null) manager.SetActiveAccount(cred.PrivateKey);

            else
                await Shell.Current.GoToAsync("login", true);

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
