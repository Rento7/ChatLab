using ChatClient.Services;
using ChatClient.ViewModels.Abstract;
using ChatClient.ViewModels.Design;
using ReactiveUI;

namespace ChatClient.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
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
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage; 
            set => this.RaiseAndSetIfChanged(ref _currentPage, value); 
        }
    }
}
