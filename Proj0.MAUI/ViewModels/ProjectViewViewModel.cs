using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proj0.MAUI.ViewModels
{
    public class ProjectViewViewModel : INotifyPropertyChanged
    {
        public Client Client { get; set; }
        public Project SelectedProject { get; set; }

        public ICommand SearchCommand { get; private set; }

        public string Query { get; set; }

        public void ExecuteSearchCommand()
        {
            NotifyPropertyChanged(nameof(Projects));
        }
        public ProjectViewViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand);
        }

        public ObservableCollection<ProjectDetailViewModel> Projects
        {
            get
            {
                if (Client == null || Client.Id == 0)
                {
                    return new ObservableCollection<ProjectDetailViewModel>();
                }
                return new ObservableCollection<ProjectDetailViewModel>(ProjectService
                    .Current.Search(Query ?? string.Empty).Where(p => p.ClientId == Client.Id)
                    .Select(r => new ProjectDetailViewModel(r)));
            }
        }

        public ProjectViewViewModel(int clientId)
        {
            if (clientId > 0)
            {
                Client = new Client(ClientService.Current.Get(clientId));
            }
            else
            {
                Client = new Client();
            }
            SearchCommand = new Command(ExecuteSearchCommand);

        }

        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Delete()
        {
            if (SelectedProject != null)
            {
                ProjectService.Current.Delete(SelectedProject.Id);
                SelectedProject = null;
                NotifyPropertyChanged(nameof(Projects));
                NotifyPropertyChanged(nameof(SelectedProject));
            }
        }
    }
}
