using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ChatClient.Services;
using ChatClient.ViewModels;
using ChatClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatClient;

public partial class App : Application, IDisposable
{
    IChatService _chatService = null!;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<IUIService, UIService>();
        collection.AddSingleton<IChatService, ChatService>();

        collection.AddTransient<MainWindowViewModel>();

        collection.AddTransient<LoginPageViewModel>();
        collection.AddTransient<RegistrationPageViewModel>();

        collection.AddTransient<MainViewModel>();
        collection.AddTransient<ChatViewModel>();
        collection.AddTransient<ChatsListViewModel>();

        var services = collection.BuildServiceProvider();
        var vm = services.GetRequiredService<MainWindowViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();

        _chatService = services.GetService<IChatService>()!;
        ArgumentNullException.ThrowIfNull(_chatService);
    }

    public void Dispose()
    {
        if (_chatService is IDisposable disposable)
            disposable.Dispose();
    }
}
