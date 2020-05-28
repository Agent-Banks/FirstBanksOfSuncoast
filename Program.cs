using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class Program
    {

        static int PromptForDecimal(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static void Main(string[] args)
        {
            var transactionController = new TransactionsController();
            transactionController.LoadAllTransactions();

            transactionController.ComputeCheckingAccountBalance();
            transactionController.ComputeSavingsAccountBalance();
            Console.WriteLine($"You have {transactionController.CheckingAccountValue} in your checking account");
            Console.WriteLine($"You have {transactionController.SavingsAccountValue} in your savings account");


            var userHasQuitApp = false;

            while (userHasQuitApp == false)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Welcome to First Bank of Suncoast. Please choose an option.");
                Console.WriteLine("(V)iew account balances");
                Console.WriteLine("(D)eposit funds into checking or savings accounts");
                Console.WriteLine("(W)ithdraw funds from checking or savings accounts");
                Console.WriteLine("(Q)uit the application");
                Console.WriteLine("------------------------------------------------------------");

                var option = PromptForString("Option: ");
                if (option == "V")
                {
                    transactionController.ComputeCheckingAccountBalance();
                    transactionController.ComputeSavingsAccountBalance();
                    Console.WriteLine($"You have {transactionController.CheckingAccountValue} in your checking account");
                    Console.WriteLine($"You have {transactionController.SavingsAccountValue} in your savings account");
                }

                if (option == "Q")
                {
                    userHasQuitApp = true;
                }

                if (option == "D")
                {
                    Console.WriteLine("Which account would you like to deposit into? (C)hecking or (S)avings account.");
                    var typeOfAccount = PromptForString("Option: ");

                    if (typeOfAccount == "C")
                    {
                        var newAmount = PromptForDecimal("How much would you like to deposit? ");
                        var newTransaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            AccountId = 1,
                            AccountType = "Checking",
                            TransactionType = "Deposit",
                            Amount = newAmount,
                            TransactionDate = DateTime.Now,
                        };

                        if (newAmount <= 0)
                        {
                            Console.WriteLine("This is an invalid amount. Returning to main menu.");
                        }
                        else
                        {
                            transactionController.DepositChecking(newTransaction);
                            Console.WriteLine($"You deposited {newAmount} into your checking account.");
                            transactionController.SaveAllTransactions();
                            transactionController.ComputeCheckingAccountBalance();
                        }
                    }

                    if (typeOfAccount == "S")
                    {
                        var newAmount = PromptForDecimal("How much would you like to deposit? ");
                        var newTransaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            AccountId = 2,
                            AccountType = "Savings",
                            TransactionType = "Deposit",
                            Amount = newAmount,
                            TransactionDate = DateTime.Now,
                        };

                        if (newAmount <= 0)
                        {
                            Console.WriteLine("This is an invalid amount. Returning to main menu.");
                        }
                        else
                        {
                            transactionController.DepositSavings(newTransaction);
                            Console.WriteLine($"You deposited {newAmount} into your savings account.");
                            transactionController.SaveAllTransactions();
                            transactionController.ComputeSavingsAccountBalance();
                        }
                    }
                }

                if (option == "W")
                {
                    Console.WriteLine("Which account would you like to withdraw from? (C)hecking or (S)avings account.");
                    var typeOfAccount = PromptForString("Option: ");

                    if (typeOfAccount == "C")
                    {
                        var newAmount = PromptForDecimal("How much would you like to withdraw? ");
                        var newTransaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            AccountId = 1,
                            AccountType = "Checking",
                            TransactionType = "Withdraw",
                            Amount = newAmount,
                            TransactionDate = DateTime.Now,
                        };

                        if (newAmount <= 0)
                        {
                            Console.WriteLine("This is an invalid amount. Returning to main menu.");
                        }

                        if (transactionController.CheckingAccountValue < newAmount)
                        {
                            Console.WriteLine("This would cause your account to overdraft. Returning to main menu.");
                        }
                        else
                        {
                            transactionController.WithdrawChecking(newTransaction);
                            Console.WriteLine($"You withdrew {newAmount} from your checking account.");
                            transactionController.SaveAllTransactions();
                            transactionController.ComputeCheckingAccountBalance();

                        }
                    }

                    if (typeOfAccount == "S")
                    {
                        var newAmount = PromptForDecimal("How much would you like to withdraw? ");
                        var newTransaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            AccountId = 2,
                            AccountType = "Savings",
                            TransactionType = "Withdraw",
                            Amount = newAmount,
                            TransactionDate = DateTime.Now,

                        };

                        if (newAmount <= 0)
                        {
                            Console.WriteLine("This is an invalid amount. Returning to main menu and please try again.");
                        }

                        if (transactionController.SavingsAccountValue < newAmount)
                        {
                            Console.WriteLine("This would cause your account to overdraft Returning to main menu");
                        }
                        else
                        {
                            transactionController.WithdrawSavings(newTransaction);
                            Console.WriteLine($"You withdrew {newAmount} from your savings account.");
                            transactionController.SaveAllTransactions();
                            transactionController.ComputeSavingsAccountBalance();
                        }
                    }
                }


            }







        }
    }
}
