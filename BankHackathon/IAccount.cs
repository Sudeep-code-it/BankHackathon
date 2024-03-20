using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankHackathon
{
    public interface IAccount
    {
        string getAccType();
        bool Open();
        bool Close();
        IPolicy GetPolicy();
    }

    public abstract class Account : IAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Disable auto-generation
        protected string accNo { get; set; }
        protected string Name { get; set; }
        protected string Pin { get; set; }
        protected bool active { get; set; }
        protected DateTime dateOfOpening { get; set; }
        protected double balance { get; set; }
        protected PrivilegeType privilegeType { get; set; }

        public abstract bool Open();

        public abstract bool Close();

        public abstract string getAccType();

        // Policy 

        protected IPolicy policy;
        public IPolicy GetPolicy()
        {
            return policy;
        }
        public void SetPolicy(IPolicy policy)
        {
            this.policy = policy;
        }
    }

    public class Current : Account
    {
        Current()
        {
            accNo = "CUR" + IDGenerator.generateID();
        }



        public override string getAccType()
        {
            return AccountType.CURRENT.ToString();
        }

        public override bool Open()
        {
            this.dateOfOpening = DateTime.Now;
            this.accNo = accNo;
            this.active = true;

            return true;

        }
        public override bool Close()
        {
            this.active = false;
            this.balance = 0;
            return true;
        }


    }
    public class Savings : Account
    {
        Savings()
        {
            accNo = "SAV" + IDGenerator.generateID();
        }

        public override string getAccType()
        {
            return AccountType.SAVINGS.ToString();
        }

        public override bool Open()
        {
            this.dateOfOpening = DateTime.Now;
            this.accNo = accNo;
            this.active = true;

            return true;
        }

        public override bool Close()
        {
            this.active = false;
            this.balance = 0;
            return true;
        }

    }

    public enum AccountType { SAVINGS, CURRENT };



    public interface IPolicy
    {
        double GetMinBalance();
        double GetRateOfInterest();
    }

    public class Policy : IPolicy
    {
        private double minBalance;
        private double rateOfInterest;

        public Policy(double minBalance, double rateOfInterest)
        {
            this.minBalance = minBalance;
            this.rateOfInterest = rateOfInterest;
        }

        public double GetMinBalance()
        {
            return minBalance;
        }

        public double GetRateOfInterest()
        {
            return rateOfInterest;
        }
    }

    public class PolicyFactory
    {
        private static readonly PolicyFactory Instance = new PolicyFactory();

        protected PolicyFactory() { }

        public virtual IPolicy CreatePolicy(string accType, string privilege)
        {
            string key = $"{accType.ToUpper()}-{privilege.ToUpper()}";
            string policyConfig = ConfigurationManager.AppSettings[$"Policy_{key}"];

            if (policyConfig != null)
            {
                string[] values = policyConfig.Split(',');

                if (values.Length != 2)
                {
                    throw new ConfigurationErrorsException($"Invalid policy configuration for key: {key}");
                }

                double minBalance = double.Parse(values[0]);
                double rateOfInterest = double.Parse(values[1]);

                return new Policy(minBalance, rateOfInterest);
            }

            throw new InvalidPolicyTypeException($"Invalid policy type: {key}");
        }
    }
}
