using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]
public partial class BillDetailView : ContentPage
{
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public BillDetailView()
    {
        InitializeComponent();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        if (ProjectId > 0 && ClientId > 0)
            (BindingContext as BillDetailViewModel).Add();
        Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={ProjectId}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={ProjectId}");
    }

    private void UndoClicked(object sender, EventArgs e)
    {
        (BindingContext as BillDetailViewModel).Undo();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillDetailViewModel(ClientId, ProjectId, 0);
    }
}

