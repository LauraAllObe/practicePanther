using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Search();
        }
        private void DeleteClick(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Delete();
        }
        private void AddClick(Object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Add(Shell.Current);
        }
        private void EditClick(Object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Edit(Shell.Current);
        }
        
    }
}