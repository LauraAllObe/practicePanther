using Summer2022Proj0.library.Models;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(ClientId), "clientId")]
public partial class ProjectDetailView : ContentPage
{
    public int ClientId { get; set; }
    public ProjectDetailView()
	{
		InitializeComponent();
	}
}