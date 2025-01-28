using System;

namespace Eksamen
{

    public class InsertCashTransaction : Transaction 
    { 
        public InsertCashTransaction(User user, decimal amount) :base(user, amount)
        {

        }
        public override string ToString() 
        {
            return $"Deposit Transaction;{ID};{User.Username};{Amount};{Date}";
        }
        public override void Execute()
        {
            User.Balance += Amount;
        }
    }
    
}
