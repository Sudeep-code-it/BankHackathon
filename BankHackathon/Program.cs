using Microsoft.SqlServer.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

    public enum TransactionStatus
    {
        Open, Close
    }

    public class Transaction
    {
        public int transID { get; set; } //from the generator
        public IAccount fromAccount { get; set; }

        public DateTime tranDate { get; set; }

        public double amount {  get; set; } 

        public TransactionType type { get; set; }

        public TransactionStatus status { get; set; }


    }



    public class TransactionLog
    {
        public static Dictionary<string, Dictionary<TransactionType, List<Transaction>>> Accountlogs {  get; set; } 

       public static Dictionary<string, Dictionary<TransactionType, List<Transaction>>>getTransactions()
        {
            if (Accountlogs == null)
            {
                LoadData();
            }


            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException("No Transactions found");

            return Accountlogs;


        }

        public static Dictionary<TransactionType, List<Transaction>> getTransactions(String accNo)
        {
            if (Accountlogs == null)
            {
                LoadData();
            }


            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException("No Transactions found");

            if (!Accountlogs.ContainsKey(accNo))
                throw new TransactionNotFoundException("No Transactions found with the given account number");

            return Accountlogs[accNo];  

        }

        public static List<Transaction> getTransactions(String accNo, TransactionType type)
        {
            if (Accountlogs == null)
            {
                LoadData();
            }


            if (Accountlogs.Count == 0)
                throw new TransactionNotFoundException("No Transactions found");

            if (!Accountlogs.ContainsKey(accNo))
                throw new TransactionNotFoundException("No Transactions found with the given account number");

            if (!Enum.IsDefined(typeof(TransactionType), type))
                throw new InvalidTransactionTypeException("Transaction type is invalid");


            return Accountlogs[accNo][type];



        }


        public static void logTransaction(String accNo, TransactionType type, Transaction transaction)
        {
            if (Accountlogs == null)
            {
                LoadData();
            }

            if (Accountlogs.ContainsKey(accNo))
            {
                if (Accountlogs[accNo].ContainsKey(type))
                {
                    Accountlogs[accNo][type].Add(transaction);
                }
                else
                {
                    List<Transaction> transactionlist = new List<Transaction>() { transaction };

                    Accountlogs[accNo].Add(type, transactionlist);
                }
            }
            else
            {
                List<Transaction> transactionlist=new List<Transaction>() { transaction };

                Dictionary<TransactionType, List<Transaction>> mapoftractiontypes = new Dictionary<TransactionType, List<Transaction>>();

                mapoftractiontypes.Add(type, transactionlist);

                Accountlogs.Add(accNo, mapoftractiontypes);
              
            }

            //dbcontext.Transactions.Add(transaction);


        }

        private static void LoadData()
        {
           
                BankDbContext db = new BankDbContext();

                var allData = db.Transactions.Select(t => new { t.FromAccount.AccNo, t.TransactionType, t });

                foreach (var item in allData)
                {
                    if (!Accountlogs.ContainsKey(item.AccNo))
                    {
                        Dictionary<TransactionType, List<Transaction>> dictTrans = new Dictionary<TransactionType, List<Transaction>>();
                        dictTrans.Add(item.TransactionType, new List<Transaction> { item.t });

                        Accountlogs.Add(item.AccNo, dictTrans);

                    }
                    else
                    {
                        if (!Accountlogs[item.AccNo].ContainsKey(item.TransactionType))
                        {

                            Accountlogs[item.AccNo].Add(item.TransactionType, new List<Transaction> { item.t });

                        }
                        else
                        {
                            Accountlogs[item.AccNo][item.TransactionType].Add(item.t);
                        }
                    }
                }

            
        }



    }

   
}
