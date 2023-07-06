using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(BillId), "billId")]
public partial class BillDetailView : ContentPage
{
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public int BillId { get; set; }
    public BillDetailView()
    {
        InitializeComponent();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        if (ProjectId >= 0 && ClientId > 0)
            (BindingContext as BillDetailViewModel).Add();
        if (ProjectId > 0)
            Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={ProjectId}");
        else
            Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={0}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        if (ProjectId > 0)
            Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={ProjectId}");
        else
            Shell.Current.GoToAsync($"//Bills?clientId={ClientId}&projectId={0}");
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
        if(BillId > 0)
            BindingContext = new BillDetailViewModel(ClientId, ProjectId, BillId);
        else
            BindingContext = new BillDetailViewModel(ClientId, ProjectId, 0);
    }
}

