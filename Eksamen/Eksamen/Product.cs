using System;

namespace Eksamen
{

        public class Product 
        {
            private static int _id = 1;
            private string _name;
            private int _price;
            private bool _active;
            private bool _canBeBoughtOnCredit;

            public Product(string name, int price, bool active, bool canbeboughtoncredit)
            {
                ID = _id++;
                Name = name;
                Price = price;
                Active = active;
                CanBeBoughtOnCredit = canbeboughtoncredit;
            }
            public Product(int id, string name, int price, bool active, bool canbeboughtoncredit)
            {
                ID = id;
                Name = name;
                Price = price;
                Active = active;
                CanBeBoughtOnCredit = canbeboughtoncredit;
            }

            public int ID { get; set; }

            public string Name 
            { 
                get 
                { 
                    return _name;
                }
                set
                {
                    if(value == null)
                    {
                        throw new ArgumentException("Name can't be empty"); 
                    }
                    _name = value;
                }
            }

            public int Price 
            { 
                get 
                {
                    return _price;
                }
                set
                {
                _price = value;
                }
            }

            public bool Active 
            { 
                get 
                { 
                    return _active; 
                }
                set { _active = value; }
            }

            public bool CanBeBoughtOnCredit 
            { 
                get 
                {
                    return _canBeBoughtOnCredit;
                }
                set
                {
                    _canBeBoughtOnCredit = value;
                }
            }

            public override string ToString()
            {
                return $"Type: Product \n" +
                       $"ID: {ID} \n" + 
                       $"Name: {Name} \n" +
                       $"Price: {Price} \n";
            }
        }
    
}
