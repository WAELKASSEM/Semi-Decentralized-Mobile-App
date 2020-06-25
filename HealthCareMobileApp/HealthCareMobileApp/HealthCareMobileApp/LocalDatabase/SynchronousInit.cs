using SQLite;
using System;
using System.Linq;
using HealthCareMobileApp.Contracts;

namespace HealthCareMobileApp.LocalDatabase
{
    class SynchronousInit:IDisposable
    {
        private SQLiteConnection conn;

        private void Connect()
        {
            conn = new SQLiteConnection(databasePath: Constants.DatabasePath, Constants.Flags);
        }
        private bool Exists(System.Type type)
        {
            return conn.TableMappings.Any(m => m.MappedType.Name == type.Name);
        }
        private void CreateTable(System.Type type)
        {
            conn.CreateTables(CreateFlags.None, type);
        }

        public void Dispose()
        {
            conn = null;
        }

        public SynchronousInit()
        {
            Connect();
            if (!Exists(typeof(Credentials)))
                CreateTable(typeof(Credentials));

            if (!Exists(typeof(Doctor)))
                CreateTable(typeof(Doctor));

            if (!Exists(typeof(PatientContact)))
                CreateTable(typeof(PatientContact));

            conn.Close();
        }
    }
}
