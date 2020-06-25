using HealthCareMobileApp.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using HealthCareMobileApp.LocalDatabase;
using System.Threading.Tasks;
using HealthCareMobileApp.Ethereum;
using System.Linq;
using System;

namespace HealthCareMobileApp.ViewModels
{
    class DoctorsViewModel : INotifyPropertyChanged
    {
        public DoctorsViewModel()
        {
            AddMenuVisibility = false;
            ShowAddMenuCommand = new Command(ShowAddMenu);
            AddContactCommand = new Command(async () => await AddDoctor());
            RefreshCommand = new Command(async () => await Refresh());
            LoadContacts();
        }

        #region Properties
        public ObservableCollection<Doctor> Contacts { get; set; }
        public bool AddMenuVisibility { get; set; }
        private bool IsBusy = false;
        public Command ShowAddMenuCommand { get; set; }
        public Command AddContactCommand { get; set; }
        public Command RefreshCommand { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsRefreshing { get; set; }

        #endregion
        #region Actions

        private void ShowAddMenu()
        {
            AddMenuVisibility = !AddMenuVisibility;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddMenuVisibility)));
        }
        private async Task AddDoctor()
        {
            if (IsBusy) return;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                return;

            if (!Address.IsAddress())
                return;

            IsBusy = true;
            Address = Address.ToLower();

            var db = DatabaseInstance.Database;
            var item = await db.GetDoctor(Address);


            if (item != default(Doctor))
            {
                await Application.Current.MainPage.
                    DisplayAlert("Error", "Doctor already In contacts list", "OK");
                IsBusy = false;
                return;
            }

            Config.Init(AccountManager.Instance().GetActiveAccount());

            var doctors_service = new Ethereum.DoctorsSmartContract.Service(Config.Instance().Web3Instance);

            var is_doctor = await doctors_service.CheckQueryAsync(Address, null);
            if (!is_doctor)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not a doctor", "OK");
                IsBusy = false;
                return;
            }

            var relationship_service = new Ethereum.RelationshipManagerSmartContract.Service(Config.Instance().Web3Instance);

            if (!await relationship_service.CheckPossibilityQueryAsync())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You already have a pending request", "OK");
                IsBusy = false;
                return;
            }

            if (!await relationship_service.CheckDoctorQueueQueryAsync(Address))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Doctor's queue is currently full", "OK");
                IsBusy = false;
                return;
            }

            if(await relationship_service.CheckDuplicateRequestQueryAsync(Address))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You already sent a request for this doctor", "OK");
                IsBusy = false;
                return;
            }

            await relationship_service.AddRequestAndWaitForReceiptAsync(Address);
            await db.SaveDoctor(new Doctor() { Address = Address, Name = Name, Status = Status.Pending.ToString() });
            
            
            Contacts =await db.GetDoctors();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacts)));
            IsBusy = false;
        }

        async void LoadContacts()
        {
            Contacts = await DatabaseInstance.Database.GetDoctors();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacts)));
        }

        async Task Refresh()
        {
            Config.Init(AccountManager.Instance().GetActiveAccount());
            var db = DatabaseInstance.Database;

            var pending = Contacts.FirstOrDefault(x => x.Status == Status.Pending.ToString());
            if (pending == default(Doctor))
            {
                IsRefreshing = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                return;
            }
            var relationship_service = new Ethereum.RelationshipManagerSmartContract.Service(Config.Instance().Web3Instance);
            var response = await relationship_service.GetPatientQueryAsync();

            if(response.ReturnValue1.ToLower() != pending.Address)
            {
                throw new Exception("!!!!! Not supposed to happen !!!!!");
            }
            if(response.ReturnValue2 == 1)
            {
                pending.Status = Status.Accepted.ToString();
                await db.UpdateDoctor(pending);
            }
            if(response.ReturnValue2 == 2)
            {
                await db.DeleteDoctor(pending);
            }

            if(response.ReturnValue2 != 0)
            {
                await relationship_service.UpdatePatientRequestAndWaitForReceiptAsync();
            }
            Contacts = await db.GetDoctors();
            IsRefreshing = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacts)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
        }
        #endregion
        public enum Status
        {
            Pending, Accepted
        }


    }
}
