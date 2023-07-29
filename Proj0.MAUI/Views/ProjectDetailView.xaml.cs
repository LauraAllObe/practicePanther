using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectDetailView : ContentPage
{
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public ProjectDetailView()
    {
        InitializeComponent();
        if (ClientId > 0)
            BindingContext = new ProjectDetailViewModel(ClientId, ProjectId);
        else
            BindingContext = new ProjectDetailViewModel();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).AddOrEdit();
        Shell.Current.GoToAsync($"//Projects?clientId={ClientId}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?clientId={ClientId}");
    }

    private void UndoClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).Undo();
    }

    private void YesClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).Active(false);
    }

    private void NoClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectDetailViewModel).Active(true);
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectDetailViewModel(ClientId, ProjectId);
    }
}

