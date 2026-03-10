using MorpionMaui.ViewModels;

namespace MorpionMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new GameViewModel();
    }
}