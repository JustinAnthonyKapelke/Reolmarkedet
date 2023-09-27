using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Reolmarkedet.Model;

namespace Reolmarkedet.ViewModel
{
    public class ShelfOwnerViewModel : INotifyPropertyChanged
    {
        //Instance of ShelfOwnerRepository to interact with data layer
        public ShelfOwnerRepository ShelfOwnerRepo { get; set; } = new ShelfOwnerRepository();

        // Private fields
        private int _shelfOwnerID;
        private string _shelfOwnerName;
        private double _shelfOwnerBalance;

        // PropertyChanged event to notify the view of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to trigger PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties with OnPropertyChanged to update the view when they change
        public int ShelfOwnerID
        {
            get { return _shelfOwnerID; }
            set
            {
                if (_shelfOwnerID != value)
                {
                    _shelfOwnerID = value;
                    OnPropertyChanged(nameof(ShelfOwnerID));
                }
            }
        }

        public string ShelfOwnerName
        {
            get { return _shelfOwnerName; }
            set
            {
                if (_shelfOwnerName != value)
                {
                    _shelfOwnerName = value;
                    OnPropertyChanged(nameof(ShelfOwnerName));
                }
            }
        }

        public double ShelfOwnerBalance
        {
            get { return _shelfOwnerBalance; }
            set
            {
                if (_shelfOwnerBalance != value)
                {
                    _shelfOwnerBalance = value;
                    OnPropertyChanged(nameof(ShelfOwnerBalance));
                }
            }
        }



        // Constructor which accepts a ShelfOwner model as parameter and sets properties
        public ShelfOwnerViewModel(ShelfOwner shelfOwner)
        {
            ShelfOwnerID = shelfOwner.ShelfOwnerID;
            ShelfOwnerName = shelfOwner.ShelfOwnerName;
        }

        // Default constructor
        public ShelfOwnerViewModel() { }

        // Method to get a ShelfOwnerViewModel by ID
        public ShelfOwnerViewModel GetShelfOwnerByID(int shelfOwnerId)
        {
            ShelfOwner shelfOwner = ShelfOwnerRepo.GetShelfOwnerByID(shelfOwnerId);
            if (shelfOwner != null)
            {
                return new ShelfOwnerViewModel(shelfOwner)
                {
                    ShelfOwnerRepo = this.ShelfOwnerRepo
                };
            }
            return null;
        }


        // New method to get actual shelfowner balance
        public double GetBalance(int shelfOwnerID)
        {
            double balance = ShelfOwnerRepo.GetShelfOwnerBalanceFromDB(shelfOwnerID);
            return balance;     
        }

    }
}

