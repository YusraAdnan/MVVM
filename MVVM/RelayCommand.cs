using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    //This class implements an interface (interface is a class that has methods you HAVE to implement and use)
    public class RelayCommand : ICommand
    {
        Action action;
       
        public event EventHandler? CanExecuteChanged;
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        //This method in the interface takes in an object as parameter and return a bool for if the action is enabled or disabled
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        //This method runs/executes the View Model Method 
        public void Execute(object? parameter)
        {
            action();
        }
    }
}
