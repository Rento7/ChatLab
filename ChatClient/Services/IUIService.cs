using ChatClient.ViewModels.Abstract;

namespace ChatClient.Services;

internal interface IUIService
{
    T GetViewModel<T>() where T : ViewModelBase;
}

