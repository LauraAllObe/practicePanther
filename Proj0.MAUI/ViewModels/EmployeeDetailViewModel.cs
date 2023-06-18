using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public Employee Model { get; set; }

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

        public EmployeeDetailViewModel(Employee employee)
        {
            Model = employee;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as EmployeeDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as EmployeeDetailViewModel).Model.Id));
        }

        public EmployeeDetailViewModel(int id)
        {
            Model = EmployeeService.Current.Get(id);

            DeleteCommand = new Command(
                    (c) => ExecuteDelete((c as EmployeeDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as EmployeeDetailViewModel).Model.Id));
        }

        public EmployeeDetailViewModel()
        {
            Model = new Employee();

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as EmployeeDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as EmployeeDetailViewModel).Model.Id));
        }

        public void Add()
        {
            EmployeeService.Current.Add(Model);
            Model = new Employee();
        }

        public void Edit()
        {
            Model = new Employee();
        }
    }
}