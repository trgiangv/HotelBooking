using System.Windows;
using System.Windows.Controls;
using HotelBooking.Model;
using HotelBooking.ViewModels;

namespace HotelBooking.Command;

public class LoadReservationCommand : AsyncCommandBase
{
    private readonly Hotel _hotel;
    private readonly ReservationListingViewModel _reservationListingViewModel;

    public LoadReservationCommand(ReservationListingViewModel reservationListingViewModel, Hotel hotel)
    {
        _reservationListingViewModel = reservationListingViewModel;
        _hotel = hotel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
            _reservationListingViewModel.UpdateReservations(reservations);
        }
        catch (Exception e)
        {
            MessageBox.Show("Failed to load reservations.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}