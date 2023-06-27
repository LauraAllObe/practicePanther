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
        public Project Model { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ProjectDetailViewModel()
        {
            Model = new Project();
            SetUpCommands();
        }
        public ProjectDetailViewModel(int clientId)
        {
            Model = new Project { ClientId = clientId };
            SetUpCommands();
        }
        public ProjectDetailViewModel(Project model)
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
        public void ExecuteDelete(int id)
        {
            ProjectService.Current.Delete(id);
        }
        public ICommand EditCommand { get; private set; }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ProjectDetail?clientId={id}");
        }

        public void SetUpCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientDetailViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientDetailViewModel).Model.Id));
        }

    }
}
