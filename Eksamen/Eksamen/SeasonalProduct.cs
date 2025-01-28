using System;

namespace Eksamen
{

        public class SeasonalProduct : Product 
        {
            private DateTime _seasonStartDate;
            private DateTime _seasonEndDate; 


            public SeasonalProduct(string name, int price, bool active, bool canbeboughtoncredit, DateTime seasonstartdate, DateTime seasonenddate) 
                :base(name, price, active, canbeboughtoncredit)
            {
                SeasonStartDate = seasonstartdate;
                SeasonEndDate = seasonenddate;
            }

            public DateTime SeasonStartDate
            {
                get 
                { 
                    return _seasonStartDate; 
                }
                set 
                { 
                    _seasonStartDate = value; 
                }
            }

            public DateTime SeasonEndDate
            {
                get
                {
                    return _seasonEndDate;
                }
                set
                {
                    _seasonEndDate = value;
                }
            }
            public override string ToString()
            {
                return $"Type: SeasonalProduct \n" +
                       $"ID: {ID} \n" +
                       $"Name: {Name} \n" +
                       $"Price: {Price} \n" +
                       $"StartDate: {SeasonStartDate} \n" +
                       $"EndDate: {SeasonEndDate} \n";
            }
        }
    
}
