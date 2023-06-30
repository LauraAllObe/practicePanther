using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;

public partial class TimerView : ContentPage
{
    public TimerView(int projectId)
    {
        InitializeComponent();
        BindingContext = new TimerViewViewModel(projectId);
    }
}