using MorpionMaui.Models;
using MorpionMaui.ViewModels;

namespace MorpionMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        var db = IPlatformApplication.Current!.Services.GetRequiredService<DatabaseService>();
        BindingContext = new GameViewModel(db);
    }
}