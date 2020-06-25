
using System.Collections.ObjectModel;
using System.ComponentModel;
using HealthCareMobileApp.Ethereum;
using System.Threading.Tasks;
using Xamarin.Forms;
using HealthCareMobileApp.Ethereum.RelationshipManagerSmartContract;
using HealthCareMobileApp.Contracts;
using Android.Views.Animations;

namespace HealthCareMobileApp.ViewModels.Navigation
{
    class PendingRequestsViewModel : INotifyPropertyChanged
    {

        public PendingRequestsViewModel()
        {
            rejectCommand = new Command<Request>(async (Request r) => await Reject(r));
            addCommand = new Command<Request>(async (Request r) => await Accept(r));
            GetCommand = new Command(async () => await Get());
            Requests = new ObservableCollection<Request>();
        }
        internal class Request
        {
            public string Address { get; set; }
            public string Name { get; set; }
            public Command RejectCommand { get; set; }
            public Command AddCommand { get; set; }

        }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Command<Request> rejectCommand, addCommand;
        public Command GetCommand { get; set; }
        public ObservableCollection<Request> Requests { get; set; }
        public bool IsRefreshing { get; set; }
        #endregion

        #region Actions
        private async Task Accept(Request r)
        {
            if (string.IsNullOrEmpty(r.Name)) return;
            r.Address = r.Address.ToLower();
            var db = LocalDatabase.DatabaseInstance.Database;
            Config.Init(AccountManager.Instance().GetActiveAccount());
            var relationship_manager = new Service(Config.Instance().Web3Instance);
            await relationship_manager.RespondRequestAsync(r.Address, 1);
            var patient_contact = new PatientContact()
            {
                Address = r.Address,
                Name = r.Name
            };
            await db.SavePatient(patient_contact);
            Requests.Remove(r);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Requests)));
        }
        private async Task Reject(Request r)
        {
            Config.Init(AccountManager.Instance().GetActiveAccount());
            r.Address = r.Address.ToLower();
            var relationship_manager = new Service(Config.Instance().Web3Instance);
            await relationship_manager.RespondRequestAndWaitForReceiptAsync(r.Address, 2);
            Requests.Remove(r);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Requests)));
        }
        async Task Get()
        {
            IsRefreshing = true;
            Requests.Clear();
            Config.Init(AccountManager.Instance().GetActiveAccount());
            var relationship_manager = new Service(Config.Instance().Web3Instance);

            for (int i = 0; i < 3; i++)
            {
                var address = await relationship_manager.GetDocQueryAsync(i);
                if (address.IsZero()) continue;
                Requests.Add(new Request()
                {
                    AddCommand = addCommand,
                    RejectCommand = rejectCommand,
                    Address = address.ToLower()
                });
            }
            IsRefreshing = false;
        }
        #endregion

    }
}
