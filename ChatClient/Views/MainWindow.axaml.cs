using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ChatClient.ViewModels;

namespace ChatClient.Views;

internal partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
