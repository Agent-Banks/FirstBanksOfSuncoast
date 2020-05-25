using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBanksOfSuncoast
{
    class Program
    {
        static void Main(string[] args)
        {
            // create an object that manages our transactions
            var transactionController = new TransactionController();
            transactionController.LoadAllTransactions();

            var frontEnd = new FrontEnd(transactionController);

            frontEnd.Menu();

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
            transactionController.orderTransactionsByTime();
            //transactionController.ComputeAllAccountValues();

            transactionController.SaveAllTransactions();
        }
    }
}
