using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;

public partial class TimerView : ContentPage
{
    public TimerView(int projectId)
    {
        InitializeComponent();
        BindingContext = new TimerViewViewModel(projectId);
    }
    public TimerView(int projectId, Window parentWindow)
    {
        InitializeComponent();
        BindingContext = new TimerViewViewModel(projectId, parentWindow);
    }
}