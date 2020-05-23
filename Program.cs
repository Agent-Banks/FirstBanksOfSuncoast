using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace FirstBanksOfSuncoast
{
    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var inputFromUser = Console.ReadLine();

            return inputFromUser;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int inputFromUser;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out inputFromUser);

            if (isThisGoodInput)
            {
                return inputFromUser;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }

        }

        static decimal PromptForDecimal(string prompt)
        {
            Console.Write(prompt);
            decimal inputFromUser;
            var isThisGoodInput = decimal.TryParse(Console.ReadLine(), out inputFromUser);

            if (isThisGoodInput)
            {
                return inputFromUser;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }

        }
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

            var userWantsToQuit = false;


            while (userWantsToQuit == false)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Please choose an option");
                Console.WriteLine("(V)iew my account balances");
                Console.WriteLine("(D)eposit into one of my accounts");
                Console.WriteLine("(W)ithdraw from one of my accounts");
                Console.WriteLine("(Q)uit the application");
                Console.WriteLine("-----------------------");

                var option = PromptForString("Option: ");

                if (option == "Q")
                {
                    userWantsToQuit = true;
                }

                if (option == "D")
                {
                    Console.WriteLine("What account would you like to deposit funds into (C)hecking or (S)avings?");
                    var accountChoice = PromptForString("Option: ");

                    if (accountChoice == "C")
                    {
                        var newId = Guid.NewGuid();
                        var newAccountId = 1;
                        var newAmount = PromptForDecimal("Amount to deposit: ");
                        var newDate = DateTime.Now;
                        var newTransaction = new Transaction
                        {
                            Id = newId,
                            AccountId = newAccountId,
                            Amount = newAmount,
                            TransactionDate = newDate,
                        };
                        checkingAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($"You have deposited {newAmount} into your checking account");
                    }

                    if (accountChoice == "S")
                    {
                        var newId = Guid.NewGuid();
                        var newAccountId = 2;
                        var newAmount = PromptForDecimal("Amount to deposit: ");
                        var newDate = DateTime.Now;
                        var newTransaction = new Transaction
                        {
                            Id = newId,
                            AccountId = newAccountId,
                            Amount = newAmount,
                            TransactionDate = newDate,
                        };
                        savingsAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($"You have deposited {newAmount} into your savings account");
                    }
                }
            }
            var fileWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(checkingAccount.Transactions);
            csvWriter.WriteRecords(savingsAccount.Transactions);
            fileWriter.Close();
        }
    }
}
