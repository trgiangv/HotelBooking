using HotelBooking.Model;
using HotelBooking.Services;
using HotelBooking.Stores;
using HotelBooking.ViewModels;

namespace HotelBooking.Command;

public class NavigateCommand : CommandBase
{
    private readonly NavigationService _navigationService;

    public NavigateCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}