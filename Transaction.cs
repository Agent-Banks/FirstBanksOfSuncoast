using System;

namespace FirstBankOfSuncoast
{

    public class Transaction
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
