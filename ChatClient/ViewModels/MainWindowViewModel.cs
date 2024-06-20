using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ReactiveUI;
using System;

namespace ChatClient.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase, IDisposable
    {
        IChatService _chatService;
        IUIService _uiService;
        ViewModelBase _currentPage;

        RegistrationPageViewModel _registrationPageViewModel;
        LoginPageViewModel _loginPageViewModel;
        MainViewModel _mainViewModel;

        public MainWindowViewModel(IChatService chatService, IUIService uiService) 
        {
            _chatService = chatService;
            _uiService = uiService;

            _loginPageViewModel = _uiService.GetViewModel<LoginPageViewModel>();
            _registrationPageViewModel = _uiService.GetViewModel<RegistrationPageViewModel>();
            _mainViewModel = _uiService.GetViewModel<MainViewModel>();

            _currentPage = _loginPageViewModel;

            _chatService.LoginSuccessfully += _chatService_LoginSuccessfully;
        }

        private void _chatService_LoginSuccessfully(object? sender, EventArgs e)
        {
            CurrentPage = _mainViewModel;
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage; 
            set => this.RaiseAndSetIfChanged(ref _currentPage, value); 
        }

        public void Dispose()
        {
        }
    }
}
