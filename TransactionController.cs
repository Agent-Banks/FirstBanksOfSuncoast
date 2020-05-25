using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class TransactionsController
    {
        private List<Transaction> Transactions = new List<Transaction>();

        public void SaveAllTransactions()
        {
            var writer = new StreamWriter("Transactions.csv");

            var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Transactions);

            writer.Close();
        }

        public void LoadAllTransactions()
        {
            if (File.Exists("transactions.csv"))
            {
                var reader = new StreamReader("transactions.csv");

                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                Transactions = csvReader.GetRecords<Transaction>().ToList();

            }
        }



        public void DisplayCheckingAccountBalance()
        {


            var checkingTransaction = Transactions.Where(transactions => transactions.AccountId == 1).ToList();
            var withdrawTotalValue = checkingTransaction.Where(transactions => transactions.TransactionType == "Withdraw");
            var depositTotalValue = checkingTransaction.Where(transactions => transactions.TransactionType == "Deposit");
            var withdrawTotal = withdrawTotalValue.Sum(transactions => transactions.Amount);
            var depositTotal = depositTotalValue.Sum(transactions => transactions.Amount);

            var checkingAccountValue = depositTotal - withdrawTotal;

            Console.WriteLine($"Your current balance of your checking account is {checkingAccountValue}");
        }

        public void DisplaySavingsAccountBalance()
        {
            var savingsTransaction = Transactions.Where(transactions => transactions.AccountId == 2).ToList();

            var withdrawTransaction = Transactions.Select(transactions => transactions.TransactionType == "Withdraw");

            var savingsAccountValue = savingsTransaction.Sum(transactions => transactions.Amount);

            Console.WriteLine($"Your current balance of your savings account is {savingsAccountValue}");
        }

        internal void DepositChecking(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        internal void WithdrawChecking(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        internal void WithdrawSavings(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        internal void DepositSavings(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }
    }
}