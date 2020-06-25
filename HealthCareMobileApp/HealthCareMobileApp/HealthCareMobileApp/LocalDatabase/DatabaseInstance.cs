using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCareMobileApp.LocalDatabase
{
     sealed class DatabaseInstance
    {
        static HealthCareDatabase database;
        public static HealthCareDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new HealthCareDatabase();
                }
                return database;
            }
        }
    }
}
