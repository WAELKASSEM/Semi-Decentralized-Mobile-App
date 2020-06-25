using HealthCareMobileApp.Contracts;
using HealthCareMobileApp.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCareMobileApp.ViewModels
{
    class PatientsViewModel : INotifyPropertyChanged
    {
        
        public PatientsViewModel()
        {
            Patients = new ObservableCollection<PatientContact>();
            ItemTappedCommand = new Command<SearchableContactsList>(async(x)=>await Tap(x));
        }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PatientContact> Patients { get; set; }
        public Command ItemTappedCommand { get; set; }
        #endregion

        #region Actions
        public async Task GetPatients()
        {
            Patients.Clear();
            var db = LocalDatabase.DatabaseInstance.Database;
            var patients = await db.GetPatientContacts();
            foreach (var patient in patients)
            {
                Patients.Add(patient);
            }
        }
        async Task Tap(SearchableContactsList cv)
        {
            if (cv.SelectedItem == null) return;
            var item = (cv.SelectedItem as PatientContact);
            await Shell.Current.GoToAsync($"details?address={item.Address.ToLower()}");
            cv.SelectedItem = null;
        }
        #endregion
    }
}
