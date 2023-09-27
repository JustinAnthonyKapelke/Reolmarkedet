using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarkedet.Model
{
    public class ShelfOwner
    {
        //Fields       
        private int _shelfOwnerID;
        private string _shelfOwnerName;
        private double _shelfOwnerBalance;     


        //Properties
        public int ShelfOwnerID
        {
            get { return _shelfOwnerID; }
            set { _shelfOwnerID = value; }
        }


        public string ShelfOwnerName
        {
            get { return _shelfOwnerName; }
            set { _shelfOwnerName = value; }
        }

        public double ShelfOwnerBalance
        {
            get { return _shelfOwnerBalance; }
            set { _shelfOwnerBalance = value; }
        }


        //Constructor
        public ShelfOwner(int shelfOwnerID, string shelfOwnerName, double shelfOwnerBalance)
        {
            ShelfOwnerID = shelfOwnerID;
            ShelfOwnerName = shelfOwnerName;
            ShelfOwnerBalance = shelfOwnerBalance;
        }

        public ShelfOwner()
        { }

        //Methods
        //public double shelfOwnerBalanceCalculator(double shelfOwnerBalance) 
        //{ 
        //    foreach (Sale sale in Sales)
        //        foreach (Product product in sale.ListOfProducts)
        //        {
        //            shelfOwnerBalance += product.ProductPrice;
        //        }
        //    return shelfOwnerBalance;
        //}


    }
}
