using System;
using System.IO;
using System.Linq;

namespace Eksamen
{

    public abstract class Transaction 
    {
        private static int _id;
        private User _user;
        private DateTime _date = DateTime.Now;
        private decimal _amount;
            

        public Transaction(User user, decimal amount)
        {
            if (_id == 0)
            {
                _id = returnLastIDInLogFile();
            }
            ID = ++_id;
            User = user;
            Amount = amount;
        }
        public int ID 
        { 
            get 
            { 
                return _id;
            } 
            private set 
            { 
                _id = value;
            } 
        }
        public User User 
        {
            get
            {
                return _user;
            }
            private set
            {
                _user = value;
            }
        }
        public DateTime Date 
        {
            get
            {
                return _date;
            }
        }
        public decimal Amount 
        {
            get
            {
                return _amount;
            }
            private set
            {
                _amount = value;
            }
        }
        public override string ToString()
        {
            return $"Transaction ;{ID} ;{User.Username};{Amount};{Date}";
        }
        public abstract void Execute();
        private int returnLastIDInLogFile()
        {
            if (File.Exists(@"..\..\..\Data\transactionLog.csv") == false)
            {
                return 1;
            }
            if (new FileInfo(@"..\..\..\Data\transactionLog.csv").Length == 0)
            {
                return 1;
            }
            string lastLine = File.ReadLines(@"..\..\..\Data\transactionLog.csv").Last();
            string[] values = lastLine.Split(";");
            return int.Parse(values[1]);
        }
    }
    
}
