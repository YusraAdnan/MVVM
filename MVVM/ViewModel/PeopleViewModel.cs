//using GalaSoft.MvvmLight.Command;
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
    public class PeopleViewModel : INotifyPropertyChanged
    {
        //Bindable properties
        private string _name;
        public string Name
        {
            get => _name;
            private set
            { 
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        private string _age;

        public string Age
        {
            get => _age;
            private set
            {
                _age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }

        private string _address;

        public string Address
        {
            get => _address;
            private set 
            {
               _address = value;
                NotifyPropertyChanged(nameof(Address));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
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
            Name = people[index].Name;
            Age = people[index].Age.ToString();
            Address = people[index].Address;

            //Relay command is a class that takes an action in as a par
            Previous = new RelayCommand(() =>
            {
                if (index > 0)
                {
                    index--;
                    Name = people[index].Name;
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
                    Name = people[index].Name;
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
                    Name = people[index].Name;
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
