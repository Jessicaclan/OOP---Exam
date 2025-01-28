using System;
using System.Text.RegularExpressions;

namespace Eksamen
{

    public class User : IComparable<User>
    {

        private int _id;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;
        private decimal _balance;
            
        public User(int id, string firstname, string lastname, string username, string email, decimal balance) 
        {
            ID = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Balance = balance;

        }
        public int ID 
        { 
            get 
            { 
                return _id; 
            } 
            set 
            { 
                _id = value; 
            } 
        }
        public string Firstname 
        { 
            get 
            { 
                return _firstname;
            }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Firstname can't be empty");
                }
                else
                {
                    _firstname = value;
                }
            } 
        }
        public string Lastname 
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Lastname can't be empty");
                }
                else
                {
                    _lastname = value; 
                }
            }
        }
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value == null) 
                {
                    throw new ArgumentException();
                }
                if (!IsUsernameValid(value))
                {
                    throw new ArgumentException("Username can only contain '0-9', 'a-z' and '_'");
                }
                else
                {
                    _username = value;
                }
            }
        }
        public string Email
        {
            get 
            { 
                return _email; 
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Email can't be empty");
                }
                if(!IsEmailValid(value))
                {
                    throw new ArgumentException("Email has to be real");
                }
                else
                {
                    _email = value;
                }
            }
        }
        public decimal Balance
        {
            get 
            { 
                return _balance; 
            }
            set
            {
                _balance = value;
            }
        }
        private bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(@"^[a-z0-9_]*$"); 
            return regex.IsMatch(username);
        }
        private bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\._\-]+@(?!\-)([a-zA-Z0-9\-]+\.[a-zA-Z0-9]+)+(?<!\-)$"); 
            return regex.IsMatch(email);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
            {
                throw new UserException("Object is not a user");
            }
            return (((User)obj).GetHashCode() == GetHashCode());

        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override string ToString()
        {
            return $"Type: User \n" +
                    $"Firstname: {Firstname}  \n" +
                    $"Lastname: {Lastname}  \n" +
                    $"Email: {Email} \n";
        }
        public int CompareTo(User other)
        {
            return this.ID.CompareTo(other.ID);
        }
            
    }
}
