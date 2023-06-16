using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Summer2022Proj0.library.Models;
using System.Collections.ObjectModel;

namespace Proj0.MAUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Client> Clients
        {
            get
            {
                if(string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Clients);
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
               
            }
        }
        public string Query { get; set; }
        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }
        public void Delete()
        {
            if(SelectedClient == null)
            {
                return;
            }
            NotifyPropertyChanged("Clients");
        }
        public void Add(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//Client?personId={idParam}");
        }
        public void Edit(Shell s)
        {
            var idParam = SelectedClient?.Id ?? 0;
            s.GoToAsync($"//Client?personId={idParam}");
        }

        public Client SelectedClient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
