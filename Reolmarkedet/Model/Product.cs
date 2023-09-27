using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarkedet.Model
{
    public class Product
    {
        //Felter
        private int _productID;
        private string _productName;
        private double _productPrice; 
        private int _shelfOwnerID;


        //Egenskaber
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }

        }
            public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

          public double ProductPrice
		{
			get { return _productPrice; }
			set { _productPrice = value; }
		}

        public int ShelfOwnerID
        {
            get { return _shelfOwnerID; ; }
            set { _shelfOwnerID = value; }
        }


        //Constructor
        public Product(int productID, string productName, double productPrice, int shelfOwnerID)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ShelfOwnerID = shelfOwnerID;
        }

        public Product()
        { }

    }
}
