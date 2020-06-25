using HealthCareMobileApp.Contracts;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCareMobileApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {
            VerifyCommand = new Command(async () => await Verify());
            Errors[0] = "Address Length must be 42 characters";
            Errors[1] = "Private Key Length must be 64 characters";
            Errors[2] = "Address contains invalid Characters";
            Errors[3] = "PrivateKey contains invalid Characters ";
           

        }

        public Command VerifyCommand { get; }
        public Command ExitCommand { get; }
        private readonly int AddressLength = 42;
        private readonly int PrivateKeyLength = 64;
        private readonly string[] Errors = new string[4];

        private string address;
        private string privateKey;
        #region AddressRegion
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                AddressError = (address.Length != AddressLength) || !address.All(c => char.IsLetterOrDigit(c));
                AddressColor = AddressError ? Color.Red : Color.Green;
                AddressErrorText = AddressError && (address.Length != AddressLength) ? Errors[0] : Errors[2];

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddressError)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddressColor)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddressErrorText)));

            }
        }
        public Color AddressColor { get; set; }
        public bool AddressError { get; set; }
        public string AddressErrorText { get; set; }

        #endregion

        #region PrivateKeyErrorRegion
        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                privateKey = value;
                PrivateKeyError = (privateKey.Length != PrivateKeyLength) || !privateKey.All(c => char.IsLetterOrDigit(c));
                PrivateKeyColor = PrivateKeyError ? Color.Red : Color.Green;
                PrivateKeyErrorText = PrivateKeyError && (privateKey.Length != PrivateKeyLength) ? Errors[1] : Errors[3];

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrivateKeyError)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrivateKeyColor)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrivateKeyErrorText)));

            }
        }
        public Color PrivateKeyColor { get; set; }
        public bool PrivateKeyError { get; set; }
        public string PrivateKeyErrorText { get; set; }


        #endregion

        public async Task Verify()
        {
            if (AddressError || PrivateKeyError) 
                return;
            var accountManager = AccountManager.Instance();
            if (!accountManager.Verify(address, privateKey))
            {
                await Application.Current.MainPage.DisplayAlert("", "Invalid Credentials", "OK");
                return;
            }
            var cred = new Credentials()
            {
                Address = address.ToLower(),
                PrivateKey = privateKey
            };
            await LocalDatabase.DatabaseInstance.Database.AddCredentials(cred);
            accountManager.SetActiveAccount(privateKey);
            await Shell.Current.GoToAsync("//my_doctors");
        }
    }
}
