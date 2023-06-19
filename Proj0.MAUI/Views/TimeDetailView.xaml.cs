using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(TimeId), "timeId")]
public partial class TimeDetailView : ContentPage
{
    public int TimeId { get; set; }
    public TimeDetailView()
    {
        InitializeComponent();
        if (TimeId > 0)
            BindingContext = new TimeDetailViewModel(TimeId);
        else
            BindingContext = new TimeDetailViewModel();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        if (!(TimeId > 0))
            (BindingContext as TimeDetailViewModel).Add();
        else
            (BindingContext as TimeDetailViewModel).Edit();
        Shell.Current.GoToAsync("//Times");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if (TimeId > 0)
            BindingContext = new TimeDetailViewModel(TimeId);
        else
            BindingContext = new TimeDetailViewModel();
    }
}

