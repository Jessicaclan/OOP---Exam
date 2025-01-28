using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eksamen
{
    public delegate void UserBalanceNotification(User user, decimal balance);
    public class Stregsystem : IStregsystem
    {
        public List<User> userList = new List<User>();
        public List<Product> productList = new List<Product>();
        public List<Transaction> transactionList = new List<Transaction>();

        public event UserBalanceNotification UserBalanceWarning; //Event

        public Stregsystem()
        {
            string[] productStrings = File.ReadAllLines(@"..\..\..\Data\products.csv");
            productStrings = productStrings.Skip(1).ToArray();
            foreach(string var in productStrings)
            {
                string[] values = var.Replace("\"", "").Replace("<h2>","").Replace("</h2>","").Split(';'); 
                productList.Add(new Product(int.Parse(values[0]), values[1], int.Parse(values[2]), Convert.ToBoolean(int.Parse(values[3])), false));
            }

            string[] userStrings = File.ReadAllLines(@"..\..\..\Data\users.csv");
            userStrings = userStrings.Skip(1).ToArray();
            foreach(string var in userStrings)
            {
                string[] values = var.Split(",");
                userList.Add(new User(int.Parse(values[0]) ,values[1], values[2], values[3], values[5], int.Parse(values[4])));
            }
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product, product.Price);
            ExecuteTransaction(transaction);
            UserBalanceWarning(user, 5000); //Grænse bestemt her
            return transaction;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction (transaction);
            return transaction;
        }
        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            File.AppendAllText(@"..\..\..\Data\transactionLog.csv", transaction.ToString() + Environment.NewLine);
        }
        public Product GetActiveProductByID(int id) 
        {
            foreach(Product product in ActiveProducts())
            {
                if (product.ID == id) { return product; }
            }
            throw new ProductNotFoundException("Product with ID: " + id + " could not be found");
        }
        public Product GetProductByID(int id)
        {
            foreach (Product product in productList)
            {
                if (product.ID == id) { return product; }
            }
            throw new ProductNotFoundException("Product with ID: " + id + " could not be found");
        }
        public IEnumerable<User> GetUsers(Func<User, bool>predicate) 
        {
            IEnumerable<User> wantedList = userList.Where(predicate);
            return wantedList;
        }
        public User GetUserByUsername(string username)
        {
            foreach(User user in userList)
            {
                if (user.Username == username) { return user; }
            }
            throw new UserNotFoundException(username + " could not be found");
        }
        public List<string> GetTransactions(User user, int count)
        {
            List<string> wantedList = new List<string>();
            int i = 0;

            string[] transactionStrings = File.ReadAllLines(@"..\..\..\Data\transactionLog.csv");
            transactionStrings = transactionStrings.Skip(1).ToArray();

            foreach (string var in transactionStrings)
            {
                string[] values = var.Split(";");
                if (values[2].Equals(user.Username) && i < count)
                {
                    wantedList.Add(var);
                    i++;
                }
            }
            wantedList.Reverse();
            return wantedList;
        }
        public List<Product> ActiveProducts()
        {
            List<Product> activeProductList = new List<Product>();

            foreach (Product product in productList)
            {
                if (product.Active)
                {
                    activeProductList.Add(product);
                }
            }
            if (activeProductList.Count == 0)
            {
                throw new Exception("No active products found");
            }
            return activeProductList;
        }
        public void ChangeProductStatus(Product product)
        {
            if (product.Active == false)
            {
                product.Active = true;
            }
            else if (product.Active == true)
            {
                product.Active = false;
            }
        }
        public void ChangeCreditStatus(Product product)
        {
            if (product.CanBeBoughtOnCredit == false)
            {
                product.CanBeBoughtOnCredit = true;
            }
            else if(product.CanBeBoughtOnCredit == true)
            {
                product.CanBeBoughtOnCredit = false;
            }
        }

    }
    
}
