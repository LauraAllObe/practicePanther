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
    public class ClientDetailViewModel
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
            ClientService.Current.Delete(id);
        }

        public bool IsActiveVisible { get; set; }

        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        public ClientDetailViewModel(Client client)
        {
            Model = client;

            openTime = Model.OpenDate.TimeOfDay.ToString();
            closedTime = Model.ClosedDate.TimeOfDay.ToString();
            openDay = Model.OpenDate.Day;
            closedDay = Model.ClosedDate.Day;
            openMonth = Model.OpenDate.Month;
            closedMonth = Model.ClosedDate.Month;
            openYear = Model.OpenDate.Year;
            closedYear = Model.ClosedDate.Year;
            if (ClientService.Current.allProjectsClosed(Model))
                IsActiveVisible = true;
            else
                IsActiveVisible = false;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
        }

        public ClientDetailViewModel(int id)
        {
            Model = ClientService.Current.Get(id);
            if (Model == null)
                Model = new Client();
            openTime = Model.OpenDate.TimeOfDay.ToString();
            closedTime = Model.ClosedDate.TimeOfDay.ToString();
            openDay = Model.OpenDate.Day;
            closedDay = Model.ClosedDate.Day;
            openMonth = Model.OpenDate.Month;
            closedMonth = Model.ClosedDate.Month;
            openYear = Model.OpenDate.Year;
            closedYear = Model.ClosedDate.Year;
            if (ClientService.Current.allProjectsClosed(Model))
                IsActiveVisible = true;
            else
                IsActiveVisible = false;

            DeleteCommand = new Command(
                    (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));//first
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
        }

        public ClientDetailViewModel()
        {
            Model = new Client();

            openTime = Model.OpenDate.TimeOfDay.ToString();
            closedTime = Model.ClosedDate.TimeOfDay.ToString();
            openDay = Model.OpenDate.Day;
            closedDay = Model.ClosedDate.Day;
            openMonth = Model.OpenDate.Month;
            closedMonth = Model.ClosedDate.Month;
            openYear = Model.OpenDate.Year;
            closedYear = Model.ClosedDate.Year;
            if (ClientService.Current.allProjectsClosed(Model))
                IsActiveVisible = true;
            else
                IsActiveVisible = false;

            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
        }

        public void Add()
        {
            Model.stringToOpenDate(openMonth.ToString() + '/' + openDay.ToString() + '/' + openYear.ToString() + ' ' + openTime);
            Model.stringToClosedDate(closedMonth.ToString() + '/' + closedDay.ToString() + '/' + closedYear.ToString() + ' ' + closedTime);

            ClientService.Current.Add(Model);
            Model = new Client();
        }

        public void Edit()
        {
            Model.stringToOpenDate(openMonth.ToString() + '/' + openDay.ToString() + '/' + openYear.ToString() + ' ' + openTime);
            Model.stringToClosedDate(closedMonth.ToString() + '/' + closedDay.ToString() + '/' + closedYear.ToString() + ' ' + closedTime);
            Model = new Client();
        }

        public void Active(bool isActive)
        {
            Model.IsActive = isActive;
        }
    }
}