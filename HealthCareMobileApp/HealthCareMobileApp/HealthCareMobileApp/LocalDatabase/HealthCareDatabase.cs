using HealthCareMobileApp.Contracts;
using HealthCareMobileApp.LocalDatabase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareMobileApp
{
    class HealthCareDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        public HealthCareDatabase()
        {
        }
        public async Task<Credentials> GetCredentials()
        {
            int count = await Database.Table<Credentials>().CountAsync(x=> true);
            if (count != 1) return default;
            return await Database.Table<Credentials>().Where(x=> true).FirstOrDefaultAsync();
        }
        public async Task DeleteCredentials(string address)
        {
            await Database.Table<Credentials>().DeleteAsync(c => c.Address == address);
        }

        public async Task<ObservableCollection<Doctor>> GetDoctors()
        {
            var ls = await Database.Table<Doctor>().ToListAsync();
            var obs = new ObservableCollection<Doctor>(ls);
            return obs;
        }


        public Task<Doctor> GetDoctor(string address)
        {
            return Database.Table<Doctor>().Where(i => i.Address == address).FirstOrDefaultAsync();
        }

        public Task SaveDoctor(Doctor item)
        {
            return Database.InsertAsync(item);

        }
        public Task UpdateDoctor(Doctor item)
        {
            return Database.UpdateAsync(item);

        }

        public Task<int> DeleteDoctor(Doctor item)
        {
            return Database.DeleteAsync(item);
        }

        public Task AddCredentials(Credentials cred)
        {
            return Database.InsertAsync(cred);
        }


        public async Task<ObservableCollection<PatientContact>> GetPatientContacts()
        {
            var ls = await Database.Table<PatientContact>().ToListAsync();
            var obs = new ObservableCollection<PatientContact>(ls);
            return obs;
        }
        public Task SavePatient(PatientContact patient)
        {
            return Database.InsertOrReplaceAsync(patient);
        }
    }
}

