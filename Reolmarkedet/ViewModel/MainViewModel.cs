using Reolmarkedet.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Reolmarkedet.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public ProductViewModel pvm { get; set; }

        public ShelfOwnerViewModel svm { get; set; }

                
        private ObservableCollection<ProductViewModel> _productsVM;

        public ObservableCollection<ProductViewModel> ProductsVM
        {
            get { return _productsVM; }
            set
            {
                _productsVM = value;
            }
        }

        private ObservableCollection<ShelfOwnerViewModel> _shelfOwnersVM;

        public ObservableCollection<ShelfOwnerViewModel> ShelfOwnersVM
        {
            get { return _shelfOwnersVM; }
            set
            {
                _shelfOwnersVM = value;
            }
        }


        private ProductViewModel _selectedProduct;

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

       

        public MainViewModel()
        {
            // Intialise the Products ViewModel
            ProductsVM = new ObservableCollection<ProductViewModel>();
            ShelfOwnersVM = new ObservableCollection<ShelfOwnerViewModel>();

            // Initialize pvm
            pvm = new ProductViewModel(new Product());

            svm = new ShelfOwnerViewModel(new ShelfOwner());

            // Get the list of persons from the original repo
            List<Product> products = pvm.productRepo.GetProducts();

            // Go through each of the products from original repo and create a product ViewModel for them
            foreach (Product product in products)
            {
                ProductsVM.Add(new ProductViewModel(product));
            }

            // Get the list of persons from the original repo
            List<ShelfOwner> shelfOwners = svm.ShelfOwnerRepo.GetShelfOwners();

            // Go through each of the products from original repo and create a product ViewModel for them
            foreach (ShelfOwner shelfOwner in shelfOwners)
            {
                ShelfOwnersVM.Add(new ShelfOwnerViewModel(shelfOwner));
            }
        }

        //public void DeleteSelectedProduct()
        //{
        //    if (SelectedProduct != null)
        //    {
        //        ProductsVM.Remove(SelectedProduct);
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)

        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;

            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void UpdateBalance()
        {

            foreach (ShelfOwnerViewModel shelfOwner in ShelfOwnersVM)
            {
                double balance = 0;

                foreach (ProductViewModel product in ProductsVM)
                {
                    if (product.ShelfOwnerID == shelfOwner.ShelfOwnerID)
                    {
                        balance += product.ProductPrice;
                    }
                }

                shelfOwner.ShelfOwnerBalance += balance;                                
            }

            
            // Update the balance in database after calculating the new balance for all the shelf owners
            foreach (ShelfOwnerViewModel shelfOwner in ShelfOwnersVM)
            {
                svm.ShelfOwnerRepo.InsertIntoShelfOwnerBalance(shelfOwner.ShelfOwnerID, shelfOwner.ShelfOwnerBalance);
            }
        }
    }






}






















