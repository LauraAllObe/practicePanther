using Microsoft.Maui.Graphics.Text;
using Proj0.MAUI.Views;
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
    public class BillDetailViewModel : INotifyPropertyChanged
    {
        public Bill Model { get; set; }
        public string dueTime { get; set; }
        public int dueMonth { get; set; }
        public int dueYear { get; set; }
        public int dueDay { get; set; }
        public decimal totalAmount { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public BillDetailViewModel()
        {
            Model = new Bill();
            SetUpCommands();
        }

        public BillDetailViewModel(int clientId)
        {
            Model = new Bill { ClientId = clientId };
            SetUpCommands();
        }

        public BillDetailViewModel(int clientId, int projectId, int billId)
        {
            if (projectId > 0 && clientId > 0 && billId > 0)
                Model = BillService.Current.Get(billId);
            else if(clientId > 0 && billId > 0)
                Model = BillService.Current.Get(billId);
            else if (projectId > 0 && clientId > 0)
                Model = new Bill { ClientId = clientId, ProjectId = projectId };
            else if (clientId > 0)
                Model = new Bill { ClientId = clientId, ProjectId = 0 };
            SetUpCommands();
        }

        public BillDetailViewModel(Bill model)
        {
            Model = model;
            SetUpCommands();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProjectList()
        {
            NotifyPropertyChanged(nameof(Model));
        }

        public void SetUpCommands()
        {
            if(Model != null)
            {
                dueTime = DateTime.MaxValue.TimeOfDay.ToString();
                dueDay = DateTime.MaxValue.Day;
                dueMonth = DateTime.MaxValue.Month;
                dueYear = DateTime.MaxValue.Year;
                
                totalAmount = 0;
                foreach (Time time in TimeService.Current.Times)
                {
                    if(Model.ProjectId == 0 && time.Billed == false && time.wantToBill == true)
                    {
                        foreach(Project project in ProjectService.Current.Projects)
                        {
                            if (time.ProjectId == project.Id && Model.ClientId == project.ClientId)
                                totalAmount += ((decimal)(time.Hours) * (EmployeeService.Current.Get(time.EmployeeId).Rate));
                        }
                    }
                    else if (time.ProjectId == Model.ProjectId && time.Billed == false && time.wantToBill == true)
                        totalAmount += ((decimal)(time.Hours) * (EmployeeService.Current.Get(time.EmployeeId).Rate));
                }
            }
        }
        public void Undo()
        {
            SetUpCommands();
            NotifyPropertyChanged(nameof(dueTime));
            NotifyPropertyChanged(nameof(dueDay));
            NotifyPropertyChanged(nameof(dueMonth));
            NotifyPropertyChanged(nameof(dueYear));
            NotifyPropertyChanged(nameof(totalAmount));
        }
        public void Add()
        {
            DateTime temp;
            if (DateTime.TryParse(dueMonth.ToString() + '/' + dueDay.ToString() + '/' + dueYear.ToString() + ' ' + dueTime, out temp))
            {
                if(temp >= DateTime.Now)
                    Model.DueDate = temp;
                else
                    Model.DueDate = DateTime.Now;
            }
            else
            {
                Model.DueDate = DateTime.MinValue;
            }
            Model.TotalAmount = totalAmount;
            if (totalAmount > 0)
                BillService.Current.Add(Model);
            foreach (Time time in TimeService.Current.Times)
            {
                if (Model.ProjectId == 0 && time.Billed == false && time.wantToBill == true)
                {
                    foreach (Project project in ProjectService.Current.Projects)
                    {
                        if (time.ProjectId == project.Id && Model.ClientId == project.ClientId)
                        {
                            time.Billed = true;
                            time.BillId = Model.Id;
                        }
                    }
                }
                else if (time.ProjectId == Model.ProjectId && time.Billed == false && time.wantToBill == true)
                {
                    time.Billed = true;
                    time.BillId = Model.Id;
                }
            }
           
            Model = new Bill();
        }
    }
}
