using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Services;

namespace Proj0.MAUI.Views;

public partial class TimeView : ContentPage
{
    public TimeView()
    {
        InitializeComponent();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewViewModel).RefreshTimeList();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TimeDetail");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewViewModel).RefreshTimeList();
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as TimeViewViewModel).RefreshTimeList();
    }
}