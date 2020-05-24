using System;
using System.Collections.Generic;

public class Account
{
    public int Id { get; set; }
    public string AccountType { get; set; }
    public List<Transaction> Transactions { get; set; }

    public decimal GetBalance()
    {
        throw new NotImplementedException();
    }

}