using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyTrackingFinalProject
{
    public class Account
    {
        public int Id;
        public string Type;
        public string Title;
        public int Amount;
        public DateTime Date;
    }

    public class Income : Account
    {
        public void Constructor()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Expenses : Account
    {
        public void Constructor()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ProcessTransactionDetails
    {
        public Account Account
        {
            get => default;
            set
            {
            }
        }

        public list readDataFromFile()
        {
            throw new System.NotImplementedException();
        }

        public void writeDataToFile()
        {
            throw new System.NotImplementedException();
        }

        public void editList()
        {
            throw new System.NotImplementedException();
        }

        public list printAccountList()
        {
            throw new System.NotImplementedException();
        }
    }
}