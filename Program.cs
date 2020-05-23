using System;
using System.Collections.Generic;

namespace FirstBanksOfSuncoast
{
    class Program
    {
        static void Main(string[] args)
        {
            var checkingAccount = new Account()
            {
                Id = 1,
                AccountType = "Checking",
                Transactions = new List<Transaction>()
            };

            var savingsAccount = new Account()
            {
                Id = 2,
                AccountType = "Savings",
                Transactions = new List<Transaction>()
            };
        }
    }
}
