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
    public class BillViewViewModel : INotifyPropertyChanged
    {
        public Client Client { get; set; }
        public Project Project { get; set; }
        public Project SelectedProject { get; set; }

        public ICommand SearchCommand { get; private set; }

        public string Query { get; set; }

        public void ExecuteSearchCommand()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

        public ObservableCollection<BillDetailViewModel> Bills
        {
            get
            {
                if (Project == null || Project.Id == 0 || Client == null || Client.Id == 0)
                {
                    return new ObservableCollection<BillDetailViewModel>();
                }
                return new ObservableCollection<BillDetailViewModel>(BillService
                    .Current.Search(Query ?? string.Empty).Where(p => ((p.ClientId == Client.Id) && (p.ProjectId == Project.Id)))
                    .Select(r => new BillDetailViewModel(r)));
            }
        }

        public BillViewViewModel(int clientId, int projectId)
        {
            if (clientId > 0 && projectId > 0)
            {
                Client = ClientService.Current.Get(clientId);
                Project = ProjectService.Current.Get(projectId);
            }
            else
            {
                Client = new Client();
                Project = new Project();
            }
            SearchCommand = new Command(ExecuteSearchCommand);

        }

        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
