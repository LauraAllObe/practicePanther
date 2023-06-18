using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
public partial class ClientDetailView : ContentPage
{
    public int ClientId { get; set; }
    public ClientDetailView()
    {
        InitializeComponent();
        if (ClientId > 0)
            BindingContext = new ClientDetailViewModel(ClientId);
        else
            BindingContext = new ClientDetailViewModel();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        if(!(ClientId > 0))
            (BindingContext as ClientDetailViewModel).Add();
        else
            (BindingContext as ClientDetailViewModel).Edit();
        Shell.Current.GoToAsync("//Clients");
    }

    private void YesClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientDetailViewModel).Active(true);
    }

    private void NoClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientDetailViewModel).Active(false);
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientDetailViewModel(ClientId);
    }
}

