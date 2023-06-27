using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class ClientDetailViewModel : INotifyPropertyChanged
    {
        public Client Model { get; set; }
        public string openTime { get; set; }
        public int openMonth { get; set; }
        public int openYear { get; set; }
        public int openDay { get; set; }
        public string closedTime { get; set; }
        public int closedMonth { get; set; }
        public int closedYear { get; set; }
        public int closedDay { get; set; }
        public string name { get; set; }
        public string notes { get; set; }

        public bool isActive { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
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
            name = Model.Name;
            notes = Model.Notes;
            isActive = Model.IsActive;
            foreach(var project in ProjectService.Current.Projects)
            {
                if (project.ClientId == Model.Id && project.IsActive == true)
                    IsActiveVisible = false;
                else
                    IsActiveVisible = true;
            }
            if (ProjectService.Current.Projects.Count == 0)
                IsActiveVisible = true;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
            ProjectViewCommand = new Command(
                (c) => ExecuteProjectView((c as ClientDetailViewModel).Model.Id));
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
            NotifyPropertyChanged(nameof(name));
            NotifyPropertyChanged(nameof(notes));
            NotifyPropertyChanged(nameof(isActive));
        }

        public ICommand DeleteCommand { get; private set; }
        public void ExecuteDelete(int id)
        {
            ClientService.Current.Delete(id);
        }

        public ICommand AddCommand { get; private set; }
        public void ExecuteAdd(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={0}");
        }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        public bool IsActiveVisible { get; set; }

        
        public ICommand ProjectViewCommand { get; private set; }
        public void ExecuteProjectView(int id)
        {
            Shell.Current.GoToAsync($"//Projects?clientId={id}");
        }

        public ClientDetailViewModel(Client client)
        {
            Model = client;
            SetUpCommands();
        }

        public ClientDetailViewModel(int id)
        {
            Model = ClientService.Current.Get(id);
            if (Model == null)
                Model = new Client();
            SetUpCommands();
        }

        public ClientDetailViewModel()
        {
            Model = new Client();
            SetUpCommands();
        }

        public void Add()
        {
            AddOrEdit();
            ClientService.Current.Add(Model);
            Model = new Client();
        }

        public void Edit()
        {
            AddOrEdit();
            Model = new Client();
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
            Model.Name = name;
            Model.Notes = notes;
            Model.IsActive = isActive;
        }

        public void Active(bool isA)
        {
            isActive = isA;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}