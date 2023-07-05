using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;

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
        (BindingContext as ProjectViewViewModel).RefreshClientList();
        int ProjectId = 0;
        Shell.Current.GoToAsync($"//ProjectDetail?projectId={ProjectId}&clientId={ClientId}");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshClientList();
    }

    private void BillClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshClientList();
    }
}

