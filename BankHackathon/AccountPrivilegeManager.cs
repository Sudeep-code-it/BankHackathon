using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankHackathon
{

    public enum PrivilegeType
    {
         REGULAR,
         GOLD,
          PREMIUM
    }
    public class AccountPrivilegeManager
    {
        private Dictionary<PrivilegeType, double> limitsMap = new Dictionary<PrivilegeType, double>();

        public AccountPrivilegeManager() 
        {

            using (StreamReader r = new StreamReader("dailyLimits.json"))
            {
                string json = r.ReadToEnd();
                var dailyLimitsConfig = JsonConvert.DeserializeObject<Dictionary<string, double>>(json);

                foreach (var kvp in dailyLimitsConfig)
                {
                    PrivilegeType privilegeType;
                    Enum.TryParse(kvp.Key, out privilegeType);
                    limitsMap[privilegeType] = kvp.Value;
                    
                }
            }

        }
        public double getDailyLimit(PrivilegeType privilegeType)
        {
            if (!Enum.IsDefined(typeof(PrivilegeType), privilegeType))
                throw new InvalidPrivilegeTypeException("Privilege Type is invalid");

            return limitsMap[privilegeType];
        }
    }

   
}
