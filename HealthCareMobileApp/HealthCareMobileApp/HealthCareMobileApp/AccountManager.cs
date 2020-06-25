using System;
using Nethereum.Web3.Accounts;
namespace HealthCareMobileApp
{
    //Singleton Design Pattern
    //Singleton Design Pattern
    //Singleton Design Pattern
    sealed class AccountManager
    {
        private static AccountManager accountManager = new AccountManager();
        public static AccountManager Instance() => accountManager;
        private Account ActiveAccount;
        public bool Verify(string address, string privateKey)
        {
            if (address.Length != 42 || privateKey.Length != 64)
            {
                throw new Exception("Error from ViewModel,Length does not meet requirements!");
            }
            try
            {
                Account test = new Account(privateKey);
                return test.Address == address;
            }
            catch { return false; };
        }
        public void SetActiveAccount(string privateKey)
        {
            ActiveAccount = new Account(privateKey);
        }
        public Account GetActiveAccount() => ActiveAccount;
        public string GetAddress() => ActiveAccount.Address.ToLower();
        


    }
}
