using System;

namespace Eksamen
{

        public class BuyTransaction : Transaction
        {
            public BuyTransaction(User user,Product product, decimal amount) : base(user, amount)
            {
                Product = product;
            }
            public Product Product { get; set; }
            public override string ToString()
            {
                return $"Buy Transaction;{ID};{User.Username};{Amount};{Date};{Product.Name}";
            }
            public override void Execute()
            {
                if (Amount > User.Balance && Product.CanBeBoughtOnCredit == true)
                {
                    User.Balance -= Amount;
                    return;
                }

                if (Amount > User.Balance)
                {
                    throw new InsufficientCreditsException("Insuffient credit on acount to buy " + Product.Name);
                }

                User.Balance -= Amount;

            }
        }
    
}
