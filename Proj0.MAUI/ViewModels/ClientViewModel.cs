using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Summer2022Proj0.library.Services;
using Summer2022Proj0.library.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Proj0.MAUI.ViewModels
{
    public class ClientViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public Boolean isActive { get; set; }
        public DateTime openDate { get; set; }
        public DateTime closedDate { get; set; }
        public List<Project> Projects
        {
            get
            {
                if(id>0)
                    return new List<Project>(ClientService.Current.Get(id).Projects);
                else
                    return new List<Project>();
            }
            set
            {
                Projects = value;
            }
        }
        public string Query { get; set; }
        public Client State { get; set; }
        public ClientViewModel(int Id=0)
        {
            if (Id > 0)
                LoadById(Id);
            //clientId = Id;
        }
        public ClientViewModel()
        {
            //ADD CODE
        }
        public void LoadById(int Id)
        {
            var person = ClientService.Current.Get(Id) as Client;
            if(person != null)
            {
                notes = person.Notes;
                name = person.Name;
                id = person.Id;
                openDate = person.OpenDate;
                closedDate = person.ClosedDate;
                isActive = person.IsActive;
                Projects = person.Projects;
                //access by State.id
                State = person;
            }

            NotifyPropertyChanged(nameof(name));
            NotifyPropertyChanged(nameof(id));
            NotifyPropertyChanged(nameof(openDate));
            NotifyPropertyChanged(nameof(closedDate));
            NotifyPropertyChanged(nameof(isActive));
            NotifyPropertyChanged(nameof(Projects));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Delete()
        {
            
        }
        public void Add()
        {
            
        }
        public void Edit()
        {

        }
        public void Close()
        {

        }
    }
}
