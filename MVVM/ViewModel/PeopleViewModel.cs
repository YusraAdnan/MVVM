using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace MVVM.ViewModel
{
    /* ViewModel is a bridge between your Model and your VIew (XAML)
     * Prepares the data in a way that the UI can bind with 
     * Implements notifications so the View updates automatically through events handlers 
     * Exposes (gives the UI) commands to fulfill when a specific button is pressed 
     * Like the recipe and preparation */
    public class PeopleViewModel : INotifyPropertyChanged //This is the interface that enables the ViewModel to notify the UI that a change has occured
    {
        /* We are not redeclaring these variables for no reason, the viewModel is like the presentation layer
         * It copies data from the model, raises notifications, if need be it formats or transforms it for the view */
        private string _name;
        public string FirstName
        {
            get 
            { 
                return _name;
            }
            private set
            { 
                _name = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }
        private string _age;

        public string Age
        {
            get
            {
                return _age; 
            }
            private set
            {
                _age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        private string _address;

        public string Address
        {
            get 
            {
                return _address; 
            }
            private set 
            {
               _address = value;
                NotifyPropertyChanged(nameof(Address));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                NotifyPropertyChanged(nameof(SearchText));
            }
        }
        public ICommand Search { get; init; }
        public ICommand Previous { get; init; }
        public ICommand Next { get; init; }

        private int index = 0;
        private PersonModel[] people = new PersonModel[] 
        {
            new PersonModel("Alice", 20, "67 Musgrave Road"),
            new PersonModel("Bob", 25, "32 EssenWood Road"),
            new PersonModel("Charlie", 30, "23 GRINGO Wood")
        };
        public PeopleViewModel()
        {
            FirstName = people[index].Name;
            Age = people[index].Age.ToString();
            Address = people[index].Address;

           
            Previous = new RelayCommand(() =>
            {
                if (index > 0)
                {
                    index--;
                    FirstName = people[index].Name;
                    Age = people[index].Age.ToString();
                    Address = people[index].Address;
                }
            });
            /*Relay Command is our helped class implementing ICommand
             Allows us to connect a button in xaml to a method (action) inside the viewmodel
             When button is clicked, WPF calls the RelayCommand.Execute()
             Which relays and connects the click to the action we passed in here */
            Next = new RelayCommand(() =>
            {
                //This is the action we are passing to the RelayCommand class's method
                if (index < people.Length - 1)
                {
                    index++;
                    FirstName = people[index].Name;
                    Age = people[index].Age.ToString();
                    Address = people[index].Address;
                }
            }); 
            
            Search = new RelayCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(SearchText)) return;

                var foundIndex = Array.FindIndex(people,
                    p => p.Name.Equals(SearchText, StringComparison.OrdinalIgnoreCase));

                if (foundIndex >= 0)
                {
                    index = foundIndex;
                    FirstName = people[index].Name;
                    Age = people[index].Age.ToString();
                    Address = people[index].Address;
                }
            });
        }

        //This is the event that HAS to be implemented when using the Notification Interface
        public event PropertyChangedEventHandler PropertyChanged;

        //The above declared event is used in the method below
        //This method is called in the encapsulation of each variable (setter)
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged is null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
