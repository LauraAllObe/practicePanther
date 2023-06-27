using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;

namespace Proj0.MAUI.Views;

public partial class ClientView : ContentPage
{
    public ClientView()
    {
        InitializeComponent();
    }
    
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ClientDetail");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }

    private void ProjectClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }
}