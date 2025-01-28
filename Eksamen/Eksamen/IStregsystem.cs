using System;
using System.Collections.Generic;

namespace Eksamen
{

    public interface IStregsystem
    {
        BuyTransaction BuyProduct(User user, Product product);
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        public Product GetActiveProductByID(int id);
        Product GetProductByID(int id);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        List<string> GetTransactions(User user, int count);
        List<Product> ActiveProducts();
        public void ChangeProductStatus(Product product);
        public void ChangeCreditStatus(Product product);
        event UserBalanceNotification UserBalanceWarning;
    }
    
}
