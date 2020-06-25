using HealthCareMobileApp.Contracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using HealthCareMobileApp.WebInterface;
using Xamarin.Forms;
using Syncfusion.XForms.ComboBox;

namespace HealthCareMobileApp.ViewModels
{
    class DetailsPatientViewModel : INotifyPropertyChanged
    {



        public DetailsPatientViewModel(string address)
        {
            this.address = address.ToLower();
            SubmitCommand = new Command<SfComboBox>(async (x) => await SubmitModification(x));
            P = new Patient();
            GetPatient();
        }

        #region Properties
        private string address;

        public event PropertyChangedEventHandler PropertyChanged;

        public Patient P;
        public IEnumerable<string> EmergencyContacts { get => P.EmergencyContacts; }
        public IEnumerable<string> MedicalNotes { get => P.MedicalNotes; }
        public IEnumerable<string> Allergies { get => P.Allergies; }
        public string Address { get => P.Id; }
        public string BloodType { get => P.BloodType; }

        public string ModificationData { get; set; }
        public Command SubmitCommand { get; }
        #endregion

        #region Actions
        public async void GetPatient()
        {
            var webapi = WebClient.WebAPI;
            try
            {
                P = await webapi.GetPatientFile(address);
            }
            catch
            {
                return;
            }
            RaisePropertyChanged(nameof(Address));
            RaisePropertyChanged(nameof(BloodType));
            RaisePropertyChanged(nameof(EmergencyContacts));
            RaisePropertyChanged(nameof(MedicalNotes));
            RaisePropertyChanged(nameof(Allergies));

        }

        public async Task SubmitModification(SfComboBox cb)
        {
            if (string.IsNullOrEmpty(ModificationData)) return;
            var modification_type = cb.SelectedItem;
            if (modification_type == null) return;
            var modification = new Modification()
            {
                Data = ModificationData,
                DoctorAddress = AccountManager.Instance().GetAddress(),
                PatientAddress = P.Id.ToLower(),
                Type = modification_type.ToString()
            };

            var webapi = WebClient.WebAPI;
            try
            {
                await webapi.SubmitModification(modification);
            }
            catch (System.Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Not Successful", "Check Internet Connection","OK");
                return;
            }
        }
        void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName: propName));
        }



        #endregion




    }
}
