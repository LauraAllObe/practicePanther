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
    public class TimeDetailViewModel
    {
        public Time Model { get; set; }
        public string Time { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public int empId { get; set; }
        public int proId { get; set; }

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

        public TimeDetailViewModel(Time time)
        {
            Model = time;
            Time = Model.Date.TimeOfDay.ToString();
            Day = Model.Date.Day;
            Month = Model.Date.Month;
            Year = Model.Date.Year;
            empId = Model.EmployeeId;
            proId = Model.ProjectId;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as TimeDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as TimeDetailViewModel).Model.Id));
        }

        public TimeDetailViewModel(int id)
        {
            Model = TimeService.Current.Get(id);

            Time = Model.Date.TimeOfDay.ToString();
            Day = Model.Date.Day;
            Month = Model.Date.Month;
            Year = Model.Date.Year;
            empId = Model.EmployeeId;
            proId = Model.ProjectId;

            DeleteCommand = new Command(
                    (c) => ExecuteDelete((c as TimeDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as TimeDetailViewModel).Model.Id));
        }

        public TimeDetailViewModel()
        {
            Model = new Time();

            Time = Model.Date.TimeOfDay.ToString();
            Day = Model.Date.Day;
            Month = Model.Date.Month;
            Year = Model.Date.Year;
            empId = Model.EmployeeId;
            proId = Model.ProjectId;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as TimeDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as TimeDetailViewModel).Model.Id));
        }

        public void Add()
        {
            Model.stringToDate(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time);
            if(TimeService.Current.isValid(empId, proId))
                TimeService.Current.Add(Model);
            Model = new Time();
        }

        public void Edit()
        {
            Model.stringToDate(Month.ToString() + '/' + Day.ToString() + '/' + Year.ToString() + ' ' + Time);
            if (TimeService.Current.isValid(empId, proId))
            {
                Model.EmployeeId = empId;
                Model.ProjectId = proId;
            }
                Model = new Time();
        }
    }
}