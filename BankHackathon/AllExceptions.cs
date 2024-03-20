using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankHackathon
{
    internal class AllExceptions
    {
    }

    public class InvalidTransactionTypeException : Exception
    {
        public InvalidTransactionTypeException()
        {
        }

        public InvalidTransactionTypeException(string message) : base(message)
        {
        }

        public InvalidTransactionTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }


    }

    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
        {
        }

        public TransactionNotFoundException(string message) : base(message)
        {
        }

        public TransactionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }


    }

    public class InvalidPrivilegeTypeException : Exception
    {
        public InvalidPrivilegeTypeException()
        {
        }

        public InvalidPrivilegeTypeException(string message) : base(message)
        {
        }

        public InvalidPrivilegeTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
