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
    public class TimeViewViewModel : INotifyPropertyChanged
    {
        public Time SelectedTime { get; set; }

        public ICommand SearchCommand { get; private set; }

        public string Query { get; set; }

        public void ExecuteSearchCommand()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        public TimeViewViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand);
        }

        public ObservableCollection<TimeDetailViewModel> Times
        {
            get
            {
                return
                    new ObservableCollection<TimeDetailViewModel>
                    (TimeService
                        .Current.Search(Query ?? string.Empty)
                        .Select(c => new TimeDetailViewModel(c)).ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Delete()
        {
            if (SelectedTime != null)
            {
                TimeService.Current.Delete(SelectedTime.Id);
                SelectedTime = null;
                NotifyPropertyChanged(nameof(Times));
                NotifyPropertyChanged(nameof(SelectedTime));
            }
        }

        public void RefreshTimeList()
        {
            NotifyPropertyChanged(nameof(Times));
        }

    }
}
