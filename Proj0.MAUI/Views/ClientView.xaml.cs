using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;

[QueryProperty(nameof(PersonId), "personId")]

public partial class ClientView : ContentPage
{
    public ClientView()
    {
        InitializeComponent();
        BindingContext = new ClientViewModel();
    }
    public int PersonId { get; set; }
    public void OnArriving(Object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(PersonId);
    }
    public void OnLeaving(Object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void DeleteClick(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Delete();
    }
    private void AddClick(Object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Add();
    }
    private void EditClick(Object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Edit();
    }
    private void CloseClick(Object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Close();
    }
}