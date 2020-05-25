using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBanksOfSuncoast
{

    class TransactionController
    {
        public List<Transaction> Transactions = new List<Transaction>();
        public void LoadAllTransactions()
        {
            if (File.Exists("transactions.cvs"))
            {
                var reader = new StreamReader("transactions.cvs");
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                Transactions = csvReader.GetRecords<Transaction>().ToList();
            }
        }
        public void SaveAllTransactions()
        {
            var writer = new StreamWriter("transactions.csv");

            var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Transactions);
            writer.Close();
        }

        public IEnumerable<Transaction> orderTransactionsByTime()
        {
            return Transactions.OrderBy(transaction => transaction.TransactionDate);
            //var orderTransactionsByTime = Transactions.OrderBy(transactions => transactions.TransactionDate);
            //foreach (var transaction in orderTransactionsByTime)
            {
                //var description = transaction.Description;
                //return description;
            }
        }

        public void AddNewWithdrawTransaction(Guid newId, int newAccountId, decimal newAmount, DateTime newDate)
        {

            var newTransaction = new Transaction
            {
                Id = newId,
                AccountId = newAccountId,
                Amount = newAmount * -1,
                Description = ($"Withdraw {newAmount} from your account"),
                TransactionDate = newDate,
            };

            Transactions.Add(newTransaction);

            SaveAllTransactions();
        }
        public void ComputeAllAccountValues()
        {
            var checkingTransaction = Transactions.Where(transactions => transactions.AccountId == 1).ToList();
            var savingsTransaction = Transactions.Where(transactions => transactions.AccountId == 2).ToList();

            var checkingAccountValue = new List<decimal>();
            var savingsAccountValue = new List<decimal>();

            foreach (var transaction in checkingTransaction)
            {

                checkingAccountValue.Add(transaction.Amount);
            }

            var totalValueChecking = checkingAccountValue.Sum();
            Console.WriteLine($"Current Balance of Checking account is {totalValueChecking}");

            foreach (var transaction in savingsTransaction)
            {
                savingsAccountValue.Add(transaction.Amount);
            }

            var totalValueSavings = savingsAccountValue.Sum();
            Console.WriteLine($"Current Balance of Savings account is {totalValueSavings}");
        }


    }
}