using Proj0.MAUI.ViewModels;

namespace Proj0.MAUI.Views;
[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EmployeeDetailView : ContentPage
{
    public int EmployeeId { get; set; }
    public EmployeeDetailView()
    {
        InitializeComponent();
        if (EmployeeId > 0)
            BindingContext = new EmployeeDetailViewModel(EmployeeId);
        else
            BindingContext = new EmployeeDetailViewModel();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        if (!(EmployeeId > 0))
            (BindingContext as EmployeeDetailViewModel).Add();
        else
            (BindingContext as EmployeeDetailViewModel).Edit();
        Shell.Current.GoToAsync("//Employees");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if(EmployeeId > 0)
            BindingContext = new EmployeeDetailViewModel(EmployeeId);
        else
            BindingContext = new EmployeeDetailViewModel();
    }
}

