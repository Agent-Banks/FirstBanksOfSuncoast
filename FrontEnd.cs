using System;

namespace FirstBanksOfSuncoast
{
    class FrontEnd
    {
        private TransactionController OurTransactionController;

        public FrontEnd(TransactionController transactionControllerToUse)
        {
            OurTransactionController = transactionControllerToUse;
        }
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
        public void Menu()
        {
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
                if (option == "W")
                {
                    Console.WriteLine("What account would you like to withdraw funds from (C)hecking or (S)avings?");
                    var accountChoice = PromptForString("Option: ");

                    if (accountChoice == "C")
                    {
                        var newId = Guid.NewGuid();
                        var newAccountId = 1;
                        var newAmount = PromptForDecimal("Amount to Withdraw: ");
                        var newDate = DateTime.Now;
                        var newTransaction = new Transaction
                        {
                            Id = newId,
                            AccountId = newAccountId,
                            Amount = newAmount * -1,
                            Description = ($"Withdraw {newAmount} from your Checking account"),
                            TransactionDate = newDate,
                        };
                        checkingAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($"Withdraw {newAmount} from your Checking account");
                        transactionController.SaveAllTransactions();
                    }
                    if (accountChoice == "S")
                    {
                        var newId = Guid.NewGuid();
                        var newAccountId = 2;
                        var newAmount = PromptForDecimal("Amount to Withdraw: ");
                        var newDate = DateTime.Now;
                        var newTransaction = new Transaction
                        {
                            Id = newId,
                            AccountId = newAccountId,
                            Amount = newAmount * -1,
                            Description = ($"Withdraw {newAmount} from your savings account"),
                            TransactionDate = newDate,
                        };
                        savingsAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($"Withdraw {newAmount} from your savings account");
                        transactionController.SaveAllTransactions();
                    }
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
                            Description = ($"Deposited {newAmount} to your Checking account"),
                            TransactionDate = newDate,
                        };
                        checkingAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($"Deposited {newAmount} into your Checking account");
                        transactionController.SaveAllTransactions();
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
                            Description = ($"Deposited {newAmount} into your Savings account"),
                            TransactionDate = newDate,
                        };
                        savingsAccount.Transactions.Add(newTransaction);
                        Console.WriteLine($" Deposited {newAmount} into your Savings account");
                        transactionController.SaveAllTransactions();
                    }
                }
            }
        }
    }
}