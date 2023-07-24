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
        public Bill SelectedBill { get; set; }
        public string GrossTotal
        {
            get
            {
                if (Client.Id > 0)
                {
                    decimal grossTotal = 0;
                    if (Project.Id > 0)
                    {
                        foreach (Bill bill in BillService.Current.Bills)
                        {
                            if (bill.ClientId == Client.Id && bill.ProjectId == Project.Id)
                                grossTotal += bill.TotalAmount;
                        }
                        return $"Total Bills for Project {Project.Id} of Client {Client.Id}:$" + grossTotal.ToString("F2");
                    }
                    else
                    {
                        foreach (Bill bill in BillService.Current.Bills)
                        {
                            if (bill.ClientId == Client.Id)
                                grossTotal += bill.TotalAmount;
                        }
                        return $"Total Bills for Client {Client.Id}:$" + grossTotal.ToString("F2");
                    }
                }
                return string.Empty;
            }
        }

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
                if ((Project == null || Project.Id == 0) && Client != null && Client.Id > 0)
                {
                    return new ObservableCollection<BillDetailViewModel>(BillService
                        .Current.Search(Query ?? string.Empty).Where(b => (b.ClientId == Client.Id))
                        .Select(r => new BillDetailViewModel(r)));
                }
                if (Project == null || Project.Id == 0 || Client == null || Client.Id == 0)
                {
                    return new ObservableCollection<BillDetailViewModel>();
                }
                return new ObservableCollection<BillDetailViewModel>(BillService
                    .Current.Search(Query ?? string.Empty).Where(b => ((b.ClientId == Client.Id) && (b.ProjectId == Project.Id)))
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
            else if(clientId > 0)
            {
                Client = ClientService.Current.Get(clientId);
                Project = new Project();
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
