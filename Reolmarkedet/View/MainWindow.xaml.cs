using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Reolmarkedet.Model;
using Reolmarkedet.ViewModel;



namespace Reolmarkedet
{

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel mvm = new MainViewModel();


        public MainWindow()
        {
            mvm.ProductsVM.Clear();

            InitializeComponent();

            // Indstil DataContext for MainWindow til MainViewModel
            DataContext = mvm;

        }


        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel selectedProduct = (ProductViewModel)Listbox.SelectedItem;

            if (selectedProduct != null)
            {
                mvm.ProductsVM.Remove(selectedProduct);
            }
            else
            {
                MessageBox.Show("No product selected.");
            }
        }




        private void ScanProduct_Button_Click(object sender, RoutedEventArgs e)

        {
            try
            {
                int productID = int.Parse(ProductID_TextBox.Text);

                ProductViewModel productViewModel = mvm.pvm.GetProductByID(productID);

                if (productViewModel != null)
                {
                    mvm.ProductsVM.Add(productViewModel);
                }
                else
                {
                    throw new Exception("Product not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ShelfOwnerID_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int shelfOwnerIDinput = int.Parse(ShelfOwnerID_TextBox.Text);
                ShelfOwnerViewModel shelfOwnerViewModel = mvm.svm.GetShelfOwnerByID(shelfOwnerIDinput);

                string shelfOwnerName;
                if (shelfOwnerViewModel != null)
                {
                    shelfOwnerName = shelfOwnerViewModel.ShelfOwnerName;
                    mvm.svm.ShelfOwnerName = shelfOwnerName;
                    ShelfOwnerName_TextBox.Text = shelfOwnerName;
                }

                else
                {
                    throw new Exception("No shelfOwner with that ID was found");
                }

            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void ShelfOwnerName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //Get shelfOwner By ID
                int shelfOwnerID = int.Parse(ShelfOwnerID_TextBox.Text);

                ShelfOwnerViewModel shelfOwnerViewModel = mvm.svm.GetShelfOwnerByID(shelfOwnerID);

                if (shelfOwnerViewModel != null)
                {
                    ShelfOwnerName_TextBox.Text = shelfOwnerViewModel.ShelfOwnerName;

                    // Find the balance of the corresponding shelfowner
                    double balance = mvm.svm.GetBalance(shelfOwnerID);

                    ShelfOwnerBalance_TextBox.Text = balance.ToString();
                }
                else
                {
                    throw new Exception("No shelfOwner with that ID is found");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mvm.ProductsVM.Count == 0)
            {
                MessageBox.Show("No products in the receipt. Sale cannot be completed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mvm.UpdateBalance();
            MessageBox.Show("The sale is completed", "Succes");
            //Lukke det nuværende vindue
            Window.GetWindow(this).Close();
        }


    }
}





