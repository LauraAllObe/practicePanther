using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using Summer2022Proj0.library.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class EmployeeDetailViewModel : INotifyPropertyChanged
    {
        public EmployeeDTO Model { get; set; }
        public string name { get; set; }
        public decimal rate { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            EmployeeService.Current.Delete(id);
        }

        public bool IsActiveVisible { get; set; }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//EmployeeDetail?employeeId={id}");
        }

        public void SetupCommands()
        {
            name = Model.Name;
            rate = Model.Rate;
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as EmployeeDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as EmployeeDetailViewModel).Model.Id));
        }

        public void Undo()
        {
            SetupCommands();
            NotifyPropertyChanged(nameof(name));
            NotifyPropertyChanged(nameof(rate));
        }

        public EmployeeDetailViewModel(EmployeeDTO employee)
        {
            Model = employee;
            SetupCommands();
        }

        public EmployeeDetailViewModel(int id)
        {
            if (id > 0)
                Model = EmployeeService.Current.Get(id);
            else
                Model = new EmployeeDTO();
            SetupCommands();
        }

        public EmployeeDetailViewModel()
        {
            Model = new EmployeeDTO();
            SetupCommands();
        }

        public void AddOrEdit()
        {
            Model.Rate = rate;
            Model.Name = name;
            EmployeeService.Current.AddOrEdit(Model);
            Model = new EmployeeDTO();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}