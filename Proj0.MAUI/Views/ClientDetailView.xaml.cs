using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;

public partial class ClientDetailView : ContentPage
{
    public ClientDetailView()
    {
        InitializeComponent();
        BindingContext = new ClientDetailViewModel();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientDetailViewModel).Add();
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
}
