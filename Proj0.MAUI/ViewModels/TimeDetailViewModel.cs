using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Proj0.MAUI.ViewModels
{
    public class TimeDetailViewModel : INotifyPropertyChanged
    {
        public Time Model { get; set; }
        public string Time { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public int empId { get; set; }
        public int proId { get; set; }
        public string narrative { get; set; }
        public double hours { get; set; }

        public void toBill(bool tobill)
        {
            Model.wantToBill = tobill;
        }

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
            TimeService.Current.Delete(id);
        }

        public bool IsActiveVisible { get; set; }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//TimeDetail?timeId={id}");
        }

        public void SetupCommands()
        {
            Time = Model.Date.TimeOfDay.ToString();
            Day = Model.Date.Day;
            Month = Model.Date.Month;
            Year = Model.Date.Year;
            empId = Model.EmployeeId;
            proId = Model.ProjectId;
            narrative = Model.Narrative;
            hours = Model.Hours;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as TimeDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as TimeDetailViewModel).Model.Id));
        }

        public void Undo()
        {
            SetupCommands();
            NotifyPropertyChanged(nameof(Time));
            NotifyPropertyChanged(nameof(Day));
            NotifyPropertyChanged(nameof(Month));
            NotifyPropertyChanged(nameof(Year));
            NotifyPropertyChanged(nameof(empId));
            NotifyPropertyChanged(nameof(proId));
            NotifyPropertyChanged(nameof(narrative));
            NotifyPropertyChanged(nameof(hours));
        }

        public TimeDetailViewModel(Time time)
        {
            Model = time;
            SetupCommands();
        }

        public TimeDetailViewModel(int id)
        {
            Model = TimeService.Current.Get(id);
            SetupCommands();
        }

        public TimeDetailViewModel()
        {
            Model = new Time();
            SetupCommands();
        }

        public void Add()
        {
            Model.Narrative = narrative;
            Model.Hours = hours;
            DateTime temp;
            if (DateTime.TryParse(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time, out temp))
            {
                Model.Date = temp;
            }
            else
            {
                Model.Date = DateTime.MinValue;
            }

            bool projectExists = false;
            foreach(var projects in ProjectService.Current.Projects)
            {
                if(projects.Id == proId)
                    projectExists = true;
            }
            bool employeeExists = false;
            foreach (var employee in EmployeeService.Current.Employees)
            {
                if (employee.Id == empId)
                    employeeExists = true;
            }
            if (employeeExists == true)
            {
                Model.EmployeeId = empId;
                if (projectExists == true)
                {
                    Model.ProjectId = proId;
                    TimeService.Current.Add(Model);
                }
            }
            Model = new Time();
        }

        public void Edit()
        {
            bool projectExists = false;
            foreach (var projects in ProjectService.Current.Projects)
            {
                if (projects.Id == proId)
                    projectExists = true;
            }
            bool employeeExists = false;
            foreach (var employee in EmployeeService.Current.Employees)
            {
                if (employee.Id == empId)
                    employeeExists = true;
            }
            if (employeeExists == true)
            {
                Model.EmployeeId = empId;
                if (projectExists == true)
                    Model.ProjectId = proId;
            }

            Model.Narrative = narrative;
            Model.Hours = hours;
            DateTime temp;
            if (DateTime.TryParse(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time, out temp))
            {
                Model.Date = temp;
            }
            else
            {
                Model.Date = DateTime.MinValue;
            }
            Model = new Time();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}