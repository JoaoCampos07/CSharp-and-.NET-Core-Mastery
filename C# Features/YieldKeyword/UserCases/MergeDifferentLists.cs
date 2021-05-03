namespace YieldKeyword.UserCases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MergeDifferentLists
    {
        private List<CurrentAccount> CurrentAccountList { get; set; }
        private List<SavingAccount> SavingAccountList { get; set; }


        /// <summary>
        /// Using a Enumerator and the Yield keyword.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAccount> GetAccountNumbers() // O(x) + O(y) : x = ca ; y = sa ==> Better performance and i have Single Responsability
        {
            if(CurrentAccountList != null && CurrentAccountList.Any())
            {
                foreach (var ca in CurrentAccountList)
                {
                    yield return ca;
                }
            }

            if(SavingAccountList != null && SavingAccountList.Any())
            {
                foreach (var sa in SavingAccountList)
                {
                    yield return sa;
                }
            }
        }

        public IEnumerable<string> GetAccountNumbersWithLINQ() // TOTAL : ( O(x) + O(y) ) + O(y)
        {
            var CANumbersList = CurrentAccountList.Select(ca => ca.Number).ToList(); // O(x) 
            var SANumberList = SavingAccountList.Select(sa => sa.Number); // O(y) : x = ca ; y = sa

            return CANumbersList.Concat(SANumberList); // Where I assume that they do append, so I assume is  a O(y)
        }


        #region Models
        public interface IAccount
        {
            public string Number { get; set; }
            public AccountType Type { get; set; }
        }

        public enum AccountType
        {
            CurrentAccount,
            SavingAccount
        }

        private class SavingAccount : IAccount
        {
            public string Number { get; set; }
            public AccountType Type { get; set; }
        }

        private class CurrentAccount : IAccount
        {
            public string Number { get; set; }
            public AccountType Type { get; set; }
        } 
        #endregion
    }
}
