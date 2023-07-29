using Proj0.MAUI.Views;
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
    public class ProjectDetailViewModel : INotifyPropertyChanged
    {
        public ProjectDTO Model { get; set; }
        public string openTime { get; set; }
        public int openMonth { get; set; }
        public int openYear { get; set; }
        public int openDay { get; set; }
        public string closedTime { get; set; }
        public int closedMonth { get; set; }
        public int closedYear { get; set; }
        public int closedDay { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }

        public bool isActive { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ProjectDetailViewModel()
        {
            Model = new ProjectDTO();
            SetUpCommands();
        }

        public ProjectDetailViewModel(int clientId)
        {
            Model = new ProjectDTO { ClientId = clientId };
            SetUpCommands();
        }

        public ProjectDetailViewModel(int clientId, int projectId)
        {
            if(projectId > 0 && clientId > 0)
                Model = ProjectService.Current.Get(projectId);
            else if(clientId > 0)
                Model = new ProjectDTO { ClientId = clientId };
            SetUpCommands();
        }

        public ProjectDetailViewModel(ProjectDTO model)
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
        
        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(ProjectDTO project)
        {
            bool end = false;
            while (true)
            {
                foreach (TimeDTO time in TimeService.Current.Times)
                {
                    if (time.ProjectId == project.Id)
                    {
                        TimeService.Current.Delete(time.Id);
                        if (time.BillId >= 0)
                            BillService.Current.Delete(time.BillId);
                        end = false;
                        break;

                    }
                    if(TimeService.Current.Times.Last() == time)
                    {
                        end = true;
                    }
                }
                if (TimeService.Current.Times.Count <= 0)
                    end = true;
                if (end == true)
                    break;
            }
            ProjectService.Current.Delete(project.Id);
        }
        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(ProjectDTO project)
        {
            int ClientId = project.ClientId;
            int ProjectId = project.Id;
            Shell.Current.GoToAsync($"//ProjectDetail?projectId={ProjectId}&clientId={ClientId}");
        }

        public ICommand BillViewCommand { get; private set; }
        public void ExecuteBillView(ProjectDTO project)
        {
            int ClientId = project.ClientId;
            int ProjectId = project.Id;
            Shell.Current.GoToAsync($"//Bills?projectId={ProjectId}&clientId={ClientId}");
        }

        public ICommand TimerCommand { get; private set; }
        private void ExecuteTimer()
        {
            var window = new Window(new TimerView(Model.Id))
            {
                Width = 280,
                Height = 320,
                X = 0,
                Y = 0
            };
            var view = new TimerView(Model.Id, window);
            window.Page = view;
            Application.Current.OpenWindow(window);
        }

        public void SetUpCommands()
        {
            openTime = Model.OpenDate.TimeOfDay.ToString();
            closedTime = Model.ClosedDate.TimeOfDay.ToString();
            openDay = Model.OpenDate.Day;
            closedDay = Model.ClosedDate.Day;
            openMonth = Model.OpenDate.Month;
            closedMonth = Model.ClosedDate.Month;
            openYear = Model.OpenDate.Year;
            closedYear = Model.ClosedDate.Year;
            shortName = Model.ShortName;
            longName = Model.LongName;
            isActive = Model.IsActive;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ProjectDetailViewModel).Model));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ProjectDetailViewModel).Model));
            TimerCommand = new Command(
                (c) => ExecuteTimer());
            BillViewCommand = new Command(
                (c) => ExecuteBillView((c as ProjectDetailViewModel).Model));
        }
        public void Undo()
        {
            SetUpCommands();
            NotifyPropertyChanged(nameof(openTime));
            NotifyPropertyChanged(nameof(openDay));
            NotifyPropertyChanged(nameof(openMonth));
            NotifyPropertyChanged(nameof(openYear));
            NotifyPropertyChanged(nameof(closedTime));
            NotifyPropertyChanged(nameof(closedDay));
            NotifyPropertyChanged(nameof(closedMonth));
            NotifyPropertyChanged(nameof(closedYear));
            NotifyPropertyChanged(nameof(shortName));
            NotifyPropertyChanged(nameof(longName));
            NotifyPropertyChanged(nameof(isActive));
        }

        public void AddOrEdit()
        {
            DateTime temp;
            if (DateTime.TryParse(openMonth.ToString() + '/' + openDay.ToString() + '/' + openYear.ToString() + ' ' + openTime, out temp))
            {
                Model.OpenDate = temp;
            }
            else
            {
                Model.OpenDate = DateTime.MinValue;
            }
            DateTime temp2;
            if (DateTime.TryParse(closedMonth.ToString() + '/' + closedDay.ToString() + '/' + closedYear.ToString() + ' ' + closedTime, out temp2))
            {
                Model.ClosedDate = temp2;
            }
            else
            {
                Model.ClosedDate = DateTime.MinValue;
            }
            Model.LongName = longName;
            Model.ShortName = shortName;
            Model.IsActive = isActive;
            ProjectService.Current.AddOrEdit(Model);
            Model = new ProjectDTO();
        }

        public void Active(bool isA)
        {
            isActive = isA;
        }
    }
}
