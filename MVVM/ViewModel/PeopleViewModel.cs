using GalaSoft.MvvmLight.Command;
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

            Next = new RelayCommand(() =>
            {
                if (index < people.Length - 1)
                {
                    index++;
                    Name = people[index].Name;
                    Age = people[index].Age.ToString();
                    Address = people[index].Address;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
