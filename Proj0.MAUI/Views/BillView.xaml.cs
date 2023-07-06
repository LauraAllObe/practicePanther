using Proj0.MAUI.ViewModels;
using Summer2022Proj0.library.Models;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(ClientId), "clientId")]
public partial class BillView : ContentPage
{
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public BillView()
    {
        InitializeComponent();
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?clientId={ClientId}");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillViewViewModel(ClientId, ProjectId);
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as BillViewViewModel).RefreshClientList();
        Shell.Current.GoToAsync($"//BillDetail?projectId={ProjectId}&clientId={ClientId}");
    }
}

