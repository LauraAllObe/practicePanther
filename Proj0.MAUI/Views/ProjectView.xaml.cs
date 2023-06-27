using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
public partial class ProjectView : ContentPage
{
    public int ClientId { get; set; }
    public ProjectView()
    {
        InitializeComponent();
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Clients");
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewViewModel(ClientId);
    }
    
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshClientList();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectDetail");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshClientList();
    }
}

