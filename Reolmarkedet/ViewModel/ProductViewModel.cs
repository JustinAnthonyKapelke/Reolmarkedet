using Reolmarkedet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Reolmarkedet.ViewModel
{
    public class ProductViewModel
    {
        public ProductRepository productRepo = new ProductRepository();

        //Fields
        private int _productID;
        private string _productName;
        private double _productPrice;
        private int _shelfOwnerID;


        //Properties
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
            get { return _shelfOwnerID; }
            set { _shelfOwnerID = value; }
        }


        //Constructor
        public ProductViewModel(Product product)
        {         
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            ProductPrice = product.ProductPrice;
            ShelfOwnerID = product.ShelfOwnerID;
        }

        public void CreateProduct(ProductRepository productRepo)
        {
            // Her bruger du 'shelfOwner' som en parameter i din metode
            productRepo.createProduct(ProductID, ProductName, ProductPrice, ShelfOwnerID);
        }


        public void DeleteProduct(ProductRepository productRepo, ListBox listBox)
        {
            // Få det valgte produkt fra ListBox'en
            Product selectedProduct = listBox.SelectedItem as Product;

            if (selectedProduct != null)
            {
                // Slet produktet fra repository
                productRepo.removeProduct(selectedProduct);

                // Opdater brugergrænsefladen eller anden relevant logik, hvis nødvendigt
                // For eksempel, hvis du vil opdatere ListBox'en efter sletning:
                listBox.Items.Remove(selectedProduct);
            }
        }


        public ProductViewModel GetProductByID(int productID)
        {
            Product product = productRepo.GetProductByID(productID);
            if (product != null)
            {
                return new ProductViewModel(product) 
                { 
                    productRepo = productRepo 
                };
            }
            return null;
        }




    }
}
