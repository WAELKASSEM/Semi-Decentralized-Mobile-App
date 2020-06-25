using HealthCareMobileApp.Contracts;
using HealthCareMobileApp.LocalDatabase;
using HealthCareMobileApp.WebInterface;
using HealthCareMobileApp.Ethereum.DrugsSmartContract;
using HealthCareMobileApp.Ethereum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCareMobileApp.ViewModels
{
    class PrescriptionsViewModel : INotifyPropertyChanged
    {
        public PrescriptionsViewModel()
        {
            Prescriptions = new ObservableCollection<Projection>();
            RefreshCommand = new Command(async () => await GetPrescriptions());
            buy = new Command<Projection>(async (x) => await Buy(x));
            Prescriptions = new ObservableCollection<Projection>();
        }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Projection> Prescriptions { get; set; }
        public Command RefreshCommand { get; set; }
        public bool IsRefreshing { get; set; }

        private Command buy;
        #endregion
        internal class Projection
        {
            public DateTime Date { get; set; }
            public string DoctorId { get; set; }
            public string DoctorName { get; set; }
            public string MedicineName { get; set; }
            public string MedicineCode { get; set; }
            public string Comment { get; set; }

            public Command BuyCommand { get; set; }
            public static explicit operator Projection(Prescription p)
            {
                Projection Proj = new Projection();
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(p.Timestamp);
                Proj.Date = dateTimeOffset.LocalDateTime;
                Proj.DoctorId = p.DoctorId;
                Proj.Comment = p.Comment;
                Proj.MedicineCode = p.MedicineCode;
                Proj.MedicineName = p.MedicineName;
                return Proj;
            }
        }
        public async Task GetPrescriptions()
        {
            IsRefreshing = true;
            var docs = await DatabaseInstance.Database.GetDoctors();
            var myaddress = AccountManager.Instance().GetAddress();
            var webclient = WebClient.WebAPI;

            List<Prescription> prescriptions;
            try
            {
                prescriptions = await webclient.GetPrescriptions(myaddress);
            }
            catch
            {
                IsRefreshing = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                return;
            }
            var query = prescriptions.Join(docs,
                 prescription => prescription.DoctorId,
                 doc => doc.Address,
                 (doc, prescription) => (Projection)doc);

            Prescriptions.Clear();
            foreach (var item in query)
            {
                var proj = new Projection();
                proj = (Projection)item;
                proj.BuyCommand = buy;
                proj.DoctorName= docs.First(x => x.Address == item.DoctorId).Name;
                Prescriptions.Add(proj);
            }
            IsRefreshing = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prescriptions)));
        }

        public async Task Buy(Projection p)
        {
            Config.Init(AccountManager.Instance().GetActiveAccount());
            var drugs = new Service(Config.Instance().Web3Instance);
            ulong price_in_gwei;
            try
            {
                price_in_gwei = await drugs.GetQueryAsync(int.Parse(p.MedicineCode), null);
            }
            catch
            {
                return;
            }
            decimal price = (decimal) price_in_gwei.GweiToEther();
            var approval = await Application.Current.MainPage.DisplayAlert("Prompt", $"Pay {price + 0.0005M} Eth?", "Yes", "No");
            if (!approval) return;

            try
            {
                Task t1 = Config.Instance().Web3Instance.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync("0xa3ED2A76c48B5BF3D5B92C1CcB97D989996D886B",price);
                Task t2 = Config.Instance().Web3Instance.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(p.DoctorId, price);
                t1.Start();
                t2.Start();
                Task.WaitAll(t1, t2);
                await Application.Current.MainPage.DisplayAlert("Sucess", "Successfully Paid", "OK");

            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Failed Transaction", "An Error Occured", "OK");
                return;
            }

        }

    }
}
