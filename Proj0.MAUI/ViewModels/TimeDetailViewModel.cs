using Summer2022Proj0.library.DTO;
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
        public TimeDTO Model { get; set; }
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
            //ADD ARRAY OF INTEGERS ON BILL, USE TO FILTER THROUGH WHICH TO DELETE
            //ADD INTEGERS ON CREATION OF A BILL
            BillService.Current.Delete(TimeService.Current.Get(id).BillId);
            bool end = false;
            while (true)
            {
                foreach (TimeDTO times in TimeService.Current.Times)
                {
                    if(TimeService.Current.Get(id).BillId == times.BillId)
                    {
                        times.Billed = false;
                        if(id != times.Id)
                        {
                            TimeService.Current.Delete(times.Id);
                            break;
                        }
                    }
                    if (TimeService.Current.Times.Last() == times)
                    {
                        end = true;
                    }
                }
                if (TimeService.Current.Times.Count <= 0)
                    end = true;
                if (end == true)
                    break;
            }
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
            if (Model.Id > 0)
            {
                if (Model.Billed == true)
                    IsVisible = false;
                else
                    IsVisible = true;
            }

            Time = Model.Date.TimeOfDay.ToString().Split('.')[0];
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

        public TimeDetailViewModel(TimeDTO time)
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
            Model = new TimeDTO();
            SetupCommands();
        }

        public void Add()
        {
            Model.Narrative = narrative;
            Model.Hours = hours;
            DateTime temp;
            if (DateTime.TryParse(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time, out temp))
            {
                if (temp > DateTime.Now)
                    Model.Date = DateTime.Now;
                else
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
            bool projectClosed = false;
            if (ProjectService.Current.Get(proId) != null && ProjectService.Current.Get(proId).IsActive == false)
                projectClosed = true;
            bool clientClosed = false;
            if (ProjectService.Current.Get(proId) != null && ClientService.Current.Get(ProjectService.Current.Get(proId).ClientId).IsActive == false)
                clientClosed = true;
            if (employeeExists == true)
            {
                Model.EmployeeId = empId;
                if (projectExists == true && (int)Model.Hours > 0 && projectClosed == false && clientClosed == false)
                {
                    Model.ProjectId = proId;
                    Model.BillId = -1;
                    TimeService.Current.AddOrEdit(Model);
                }
            }
            Model = new TimeDTO();
        }

        public void Edit()
        {
            bool clientClosed = false;
            if (ProjectService.Current.Get(proId) != null && ClientService.Current.Get(ProjectService.Current.Get(proId).ClientId).IsActive == false)
                clientClosed = true;
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
                if (projectExists == true && clientClosed == false)
                    Model.ProjectId = proId;
            }
            bool projectClosed = false;
            if (ProjectService.Current.Get(Model.ProjectId).IsActive == false)
                projectClosed = true;
            

            Model.Narrative = narrative;
            if((int)hours > 0)
                Model.Hours = hours;
            DateTime temp;
            if (DateTime.TryParse(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time, out temp))
            {
                if (temp > DateTime.Now)
                    Model.Date = DateTime.Now;
                else
                    Model.Date = temp;
            }
            else
            {
                Model.Date = DateTime.MinValue;
            }
            if ((int)Model.Hours > 0 && projectClosed == false && clientClosed == false)
                TimeService.Current.AddOrEdit(Model);
            Model = new TimeDTO();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsVisible { get; set; }
    }
}