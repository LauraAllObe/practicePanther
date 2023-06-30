using System.Windows.Input;

namespace Proj0.MAUI.Controls;

public partial class SearchBarControl : ContentView
{
    public static readonly BindableProperty SearchButtonSourceProperty
        = BindableProperty.Create(nameof(SearchButtonSource)
            , typeof(string)
            , typeof(SearchBarControl)
            , string.Empty);

    public static readonly BindableProperty SearchCommandProperty
        = BindableProperty.Create(
            nameof(SearchCommand)
            , typeof(ICommand)
            , typeof(SearchBarControl)
            , default(ICommand));

    public static readonly BindableProperty QueryTextProperty
        = BindableProperty.Create(nameof(QueryText)
            , typeof(string)
            , typeof(SearchBarControl)
            , string.Empty
            , BindingMode.TwoWay);

    public string SearchButtonSource
    {
        get => (string)GetValue(SearchButtonSourceProperty);
        set => SetValue(SearchButtonSourceProperty, value);
    }

    public string QueryText
    {
        get => (string)GetValue(QueryTextProperty);
        set => SetValue(QueryTextProperty, value);
    }

    public ICommand SearchCommand
    {
        get => (ICommand)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    public SearchBarControl()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
}
