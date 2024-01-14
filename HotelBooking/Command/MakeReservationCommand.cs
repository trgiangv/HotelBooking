using System.ComponentModel;
using System.Windows;
using HotelBooking.Exceptions;
using HotelBooking.Model;
using HotelBooking.Services;
using HotelBooking.ViewModels;

namespace HotelBooking.Command
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly Hotel _hotel;
        private readonly NavigationService _reservationViewNavigationService;

        public MakeReservationCommand(
            MakeReservationViewModel makeReservationViewModel, 
            Hotel hotel, 
            NavigationService reservationViewNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotel = hotel;
            _reservationViewNavigationService = reservationViewNavigationService;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.UserName)
                   && _makeReservationViewModel.StartTime < _makeReservationViewModel.EndTime
                   && _makeReservationViewModel.EndTime > DateTime.Now
                   && _makeReservationViewModel.FloorNumber > 0
                   && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomId(_makeReservationViewModel.RoomNumber, _makeReservationViewModel.FloorNumber),
                _makeReservationViewModel.UserName,
                _makeReservationViewModel.StartTime,
                _makeReservationViewModel.EndTime);
            try
            {
                await _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved room",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room already taken.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(MakeReservationViewModel.UserName) or nameof(MakeReservationViewModel.FloorNumber)
                || e.PropertyName == nameof(MakeReservationViewModel.StartTime)
                || e.PropertyName == nameof(MakeReservationViewModel.EndTime))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
