using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class EmployeeViewViewModel : INotifyPropertyChanged
    {
        public Employee SelectedEmployee { get; set; }

        public ICommand SearchCommand { get; private set; }

        public string Query { get; set; }

        public void ExecuteSearchCommand()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        public EmployeeViewViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand);
        }

        public ObservableCollection<EmployeeDetailViewModel> Employees
        {
            get
            {
                return
                    new ObservableCollection<EmployeeDetailViewModel>
                    (EmployeeService
                        .Current.Search(Query ?? string.Empty)
                        .Select(c => new EmployeeDetailViewModel(c)).ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Delete()
        {
            if (SelectedEmployee != null)
            {
                EmployeeService.Current.Delete(SelectedEmployee.Id);
                SelectedEmployee = null;
                NotifyPropertyChanged(nameof(Employees));
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

    }
}

