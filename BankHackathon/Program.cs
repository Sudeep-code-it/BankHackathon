using Microsoft.SqlServer.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankHackathon
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

   public enum TransactionType
    {
        Withdraw, Deposit, Transfer, ExternalTransfer
    }

    public class Transaction
    {
        public int transID { get; set; } //from the generator
        public IAccount fromAccount { get; set; }

        public DateTime tranDate { get; set; }

        public double amount {  get; set; } 

        public TransactionType type { get; set; }

    }

    public interface IAccount
    {
    }

    public class TransactionLog
    {
        public static Dictionary<string, Dictionary<TransactionType, List<Transaction>>> Accountlogs {  get; set; } 

       public static Dictionary<string, Dictionary<TransactionType, List<Transaction>>>getTransactions()
        {
            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException();

            return Accountlogs;


        }

        public static Dictionary<TransactionType, List<Transaction>> getTransactions(String accNo)
        {
            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException();

            if (!Accountlogs.ContainsKey(accNo))
                throw new TransactionNotFoundException();

            return Accountlogs[accNo];  

        }

        public static List<Transaction> getTransactions(String accNo, TransactionType type)
        {
            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException();

            if (!Enum.IsDefined(typeof(TransactionType), type))
                throw new InvalidTransactionTypeException();

            if (!Accountlogs.ContainsKey(accNo))
                throw new TransactionNotFoundException();


            return Accountlogs[accNo][type];



        }


        public static void logTransaction(String accNo, TransactionTypes type, Transaction transaction)
        {
            dbcontext.Transactions.Add()
        }



    }
}
