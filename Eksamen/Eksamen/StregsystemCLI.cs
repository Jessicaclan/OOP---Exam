using System;

namespace Eksamen
{
    public delegate void StregsystemEvents(string commandEntered);

    class StregsystemCLI : IStregsystemUI
    {
        private IStregsystem _stregsystem;
        private bool _running = true;
        
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _stregsystem.UserBalanceWarning += BalanceWarning; //Subsriber
        }
        public event StregsystemEvents CommandEntered; //Event

        public void Close()
        {
            Console.WriteLine("Goodbye");
            _running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine(adminCommand + " is not a recognized admin command");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(errorString);
        }

        public void DisplayInsufficientCash(User user, Product product) 
        {
            Console.WriteLine(user.Username + "'s balance(" + user.Balance + ") is not sufficient to buy " + product.Name + "(" + product.Price + ")");
        }

        public void DisplayProductNotFound(string product) 
        {
            Console.WriteLine(product +" were not found, try another product");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine("Too many commands were inputted");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction) 
        {
            Console.WriteLine(transaction.User.Username + " Has Bought " + transaction.Product.Name);
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction) 
        {
            Console.WriteLine(transaction.User.Username + " Has Bought " + count + " " + transaction.Product.Name);
        }

        public void DisplayDesposit(InsertCashTransaction transaction)
        {
            Console.WriteLine(transaction.User.Username + " has added " + transaction.Amount + " to thier balance (" + transaction.User.Balance + ")");
        }

        public void DisplayUserInfo(User user) 
        {
            Console.WriteLine(user.Firstname + " " + user.Lastname);
            Console.WriteLine(user.Username);
            Console.WriteLine("Balance: " + user.Balance);
            foreach (string transaction in _stregsystem.GetTransactions(user, 10))
            {
                string[] transactionArray = transaction.Split(";");
           
                if (transactionArray.Length == 5)
                {
                    Console.WriteLine("Type: {0}\n  ID: {1}\n  User: {2}\n  Amount: {3}\n  Date: {4}\n", transactionArray[0], transactionArray[1], transactionArray[2], transactionArray[3], transactionArray[4]);
                } 
                else if(transactionArray.Length == 6)
                {
                    Console.WriteLine("Type: {0}\n  ID: {1}\n  User: {2}\n  Amount: {3}\n  Date: {4}\n  Product: {5}\n", transactionArray[0], transactionArray[1], transactionArray[2], transactionArray[3], transactionArray[4], transactionArray[5]);
                }
               
            }
        }

        public void DisplayUserNotFound(string username) 
        {
            Console.WriteLine("User " + username + " not Found");
        }

        private void StartDisplay()
        {
            Console.WriteLine();
            Console.WriteLine("{0,34}", "TREOENs STREGSYSTEM");
            Console.WriteLine("| {0,-4} | {1,-35} | {2,-5} |", "ID", "Product", "Price");
            Console.WriteLine("______________________________________________________");
            foreach (Product product in _stregsystem.ActiveProducts())
            {
                Console.WriteLine("| {0,-4} | {1,-35} | {2,-5} |", product.ID, product.Name, product.Price);
            }
            Console.WriteLine("______________________________________________________");
        }

        private void BalanceWarning(User user, decimal balance)
        {
            if (user.Balance < balance)
            {
                Console.WriteLine(user.Username + " only has " + user.Balance + " on their acount, consider recharging");
            }
        }
        public void Start() 
        {
            while (_running)
            {
                StartDisplay();
                string userInput = Console.ReadLine();
                Console.Clear();
                CommandEntered(userInput); //TriggerEvent
            }
        }
    }
    
  
}
