using HealthCareMobileApp.Contracts;
using HealthCareMobileApp.LocalDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCareMobileApp.ViewModels
{
    class ModificationsViewModel : INotifyPropertyChanged
    {

        public ModificationsViewModel()
        {
            Modifications = new ObservableCollection<Projection>();
            AcceptModificationCommand = new Command<CollectionView>(async(x)=> await AcceptModification(x));
            GetCommand = new Command(async () => await GetModifications());
        }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Projection> Modifications { get; set; }
        public Command AcceptModificationCommand { get; set; }
        public Command GetCommand { get; set; }
        public bool IsRefreshing { get; set; }

        #endregion

        #region Actions
        async Task AcceptModification(CollectionView cv)
        {
            if (cv.SelectedItem == null) return;
            var selectedItem = cv.SelectedItem as Projection;
            var action = await Shell.Current.DisplayActionSheet("Allow/Deny", "", "", "Allow Modification", "Deny Modification");

            if (string.IsNullOrEmpty(action))
            {
                cv.SelectedItem = null;
                return;
            }
            cv.IsEnabled = false;

            var webapi = WebInterface.WebClient.WebAPI;
            var my_address = AccountManager.Instance().GetAddress();

            if (action.Equals("Allow Modification"))
            {
                Modification mod = new Modification()
                {
                    Data = selectedItem.Data,
                    PatientAddress = my_address,
                    Type = selectedItem.Type,
                    TimeStamp = 0,
                    DoctorAddress = ""
                };
                try
                {
                    await webapi.Respond(mod);
                }
                catch
                {
                    return;
                }

            }
            try
            {
                await webapi.DeleteModification(selectedItem.TimeStamp);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed To Submit Modification", "OK");
                return;
            }
            Modifications.Remove(selectedItem);
            cv.SelectedItem = null;
            cv.IsEnabled = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Modifications)));
        }
        public async Task GetModifications()
        {
            IsRefreshing = true;
            var webapi = WebInterface.WebClient.WebAPI;
            var my_address = AccountManager.Instance().GetAddress();
            List<Modification> modifications;
            try
            {
                modifications = await webapi.GetModifications(my_address);
            }
            catch
            {
                IsRefreshing = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                return;
            }
            var db = DatabaseInstance.Database;
            var doctors = await db.GetDoctors();
            var query = modifications.Join(doctors,
                modification => modification.DoctorAddress,
                doc => doc.Address.ToLower(),
                (modification, doc) => modification).ToList();
            foreach (var item in query)
            {
                var proj = new Projection();
                proj.Type = item.Type;
                proj.Data = item.Data;
                proj.TimeStamp = item.TimeStamp;
                proj.DoctorName = (await db.GetDoctor(item.DoctorAddress)).Name;
                Modifications.Add(proj);
            }
            IsRefreshing = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
        }

        internal class Projection
        {
            public int TimeStamp { get; set; }
            public string Type { get; set; }
            public string Data { get; set; }
            public string DoctorName { get; set; }
        }

        #endregion

    }
}
