using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;

namespace Proj0.MAUI.Views;

public partial class EmployeeView : ContentPage
{
    public EmployeeView()
    {
        InitializeComponent();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshClientList();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//EmployeeDetail");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshClientList();
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshClientList();
    }
}