using System;
using System.Reactive;
using ReactiveUI;
using ChatClient.Services;
using ChatClient.ViewModels.Abstract;

namespace ChatClient.ViewModels;

internal class LoginPageViewModel : ViewModelBase, IDisposable
{
    IChatService _chatService;
    IUIService _uiService;

    string _login;
    string _passWord;
    string _errorMessage;

    ReactiveCommand<Unit, Unit> _loginCommand;

    public LoginPageViewModel(IChatService chatService, IUIService uiService) 
    {
        _chatService = chatService;
        _uiService = uiService;

        IObservable<bool> isInputValid = this.WhenAnyValue(
            x => x.Login,
            x => x.Password,
            (login, password) => !string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password)
            );

        _loginCommand = ReactiveCommand.Create(() =>
        {
            _chatService.Login(_login, _passWord);
        }, isInputValid);

        _chatService.LoginUnsuccessfully += _chatService_LoginUnsuccessfully;
    }

    private void _chatService_LoginUnsuccessfully(object? sender, Utility.LoginEventArgs e)
    {
        if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _errorMessage = "Unable connect to server";
        }
        else 
        {
            _errorMessage = "Wrong credentials";
        }
        this.RaisePropertyChanged(nameof(ErrorDescription));
        this.RaisePropertyChanged(nameof(HasError));
    }

    public bool HasError => !string.IsNullOrEmpty(_errorMessage);

    public string ErrorDescription 
    {
        get => _errorMessage;
    }

    public string Login 
    { 
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value); 
    }
    
    public string Password 
    {
        get => _passWord; 
        set => this.RaiseAndSetIfChanged(ref _passWord, value);
    }

    public IReactiveCommand LoginCommand => _loginCommand;

    public void Dispose()
    {
        _chatService.LoginUnsuccessfully -= _chatService_LoginUnsuccessfully;
    }
}
