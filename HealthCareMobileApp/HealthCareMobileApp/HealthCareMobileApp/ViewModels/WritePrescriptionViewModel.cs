using HealthCareMobileApp.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.ComboBox;
using HealthCareMobileApp.Ethereum.DrugsSmartContract;
using HealthCareMobileApp.Ethereum;

namespace HealthCareMobileApp.ViewModels
{
    class WritePrescriptionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Drug drug;
        public WritePrescriptionViewModel()
        {
            drug = new Drug();
            PrescribeCommand = new Command<SfComboBox>(async (x) => await Prescribe(x));
            SearchCommand = new Command(async () => await Search());
            GetPatients();
        }
        async void GetPatients()
        {
            var db = LocalDatabase.DatabaseInstance.Database;
            MyPatients = await db.GetPatientContacts();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyPatients)));
        }


        #region Properties
        public string Query { get; set; }
        public bool IsVisible { get; set; }
        public Command SearchCommand { get; }
        public ObservableCollection<PatientContact> MyPatients { get; set; }
        public string Code { get => drug._id; }
        public string BrandName { get => drug.BrandName; }
        public string Strength { get => drug.Strength; }
        public string Presentation { get => drug.Presentation; }
        public string Form { get => drug.Form; }
        public string Price { get; set; }

        public string Comment { get; set; }
        public Command PrescribeCommand { get; set; }
        #endregion

        #region Actions
        async Task Search()
        {
            if (string.IsNullOrWhiteSpace(Query)) return;
            Query = Query.Trim().ToUpper();
            var webapi = WebInterface.WebClient.WebAPI;
            Drug result;
            try
            {
                result = await webapi.GetDrug(Query);
                Config.Init(AccountManager.Instance().GetActiveAccount());
                var service = new Service(Config.Instance().Web3Instance);
                var price_in_gwei =await  service.GetQueryAsync(int.Parse(result._id));
                Price = price_in_gwei.GweiToEther().ToString();
            }
            catch 
            {
                return;
            }
            if (result == default(Drug))
            {
                IsVisible = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
                return;
            }
            drug = result;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(BrandName)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Code)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Strength)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Presentation)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Form)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            IsVisible = true;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
        }

        async Task Prescribe(SfComboBox cb)
        {
            if (string.IsNullOrEmpty(Comment)) return;
            if (cb.SelectedIndex ==-1) return;
            var address = (cb.SelectedItem as PatientContact).Address;
            var prescription = new Prescription()
            {
                Comment = this.Comment,
                DoctorId = AccountManager.Instance().GetAddress(),
                PatientId = address,
                MedicineCode = Code
            };
            //WEB API Logic
            var webapi = WebInterface.WebClient.WebAPI;
            await webapi.CreatePrescription(prescription);
        }
        #endregion
    }
}
