using CommunityToolkit.Mvvm.ComponentModel;

namespace Books.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;


    public virtual void OnAppearing()
    {

    }

    public virtual void OnDisappearing()
    {

    }
}
