using HotelBooking.ViewModels;

namespace HotelBooking.Stores;

public class NavigationStore
{
    public ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
       CurrentViewModelChanged?.Invoke();
    }

    public event Action CurrentViewModelChanged;
}