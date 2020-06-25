using HealthCareMobileApp.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using HealthCareMobileApp.WebInterface;

namespace HealthCareMobileApp.ViewModels
{
    class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {
            P = new Patient();
            AddCommand = new Command(async () => await Add());
            DeleteEmergencyContactCommand = new Command<CollectionView>(async(x) => await DeleteEmergencyContact(x));
            GetCommand = new Command(async () => await GetFile());
            UpdateCommand = new Command(async () => await Update());
        }

        public Patient P;


        #region Properties

        public string Address { get => P.Id; }
        public string BloodType { get => P.BloodType;set { P.BloodType = value; } }
        public ObservableCollection<string> EmergencyContacts
        {
            get => P.EmergencyContacts;
        }
        public IEnumerable<string> MedicalNotes { get => P.MedicalNotes; }
        public IEnumerable<string> Allergies { get => P.Allergies; }
        public Command AddCommand { get; }
        public Command DeleteEmergencyContactCommand { get; }
        public Command GetCommand { get; }
        public Command UpdateCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Actions
        async Task GetFile()
        {
            var my_address = AccountManager.Instance().GetAddress();
            try
            {
                P = await WebClient.WebAPI.GetPatientFile(my_address);
            }
            catch
            {
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Allergies)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EmergencyContacts)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BloodType)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MedicalNotes)));

        }
        async Task Update()
        {
            var address = AccountManager.Instance().GetAddress();
            var webapi = WebClient.WebAPI;
            
            try
            {
                await webapi.UpdatePatientFile(address, P);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Update Failed", "Check your Internet Connection and try again", "OK");
            }
        }
        async Task Add()
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync("Add", "", "ADD", "Cancel");
            if (string.IsNullOrWhiteSpace(result))
                return;
            EmergencyContacts.Add(result);
        }
        async Task DeleteEmergencyContact(CollectionView v)
        {
            var item = v.SelectedItem;
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure?", "Yes", "No");
            if (!answer) return;
            EmergencyContacts.Remove((string)item);
        }

        #endregion

    }
}
