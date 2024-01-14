using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelBooking.Command;
using HotelBooking.Model;
using HotelBooking.Services;
using HotelBooking.Stores;

namespace HotelBooking.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        
        public ICommand LoadReservationCommand { get; set; }
        public ICommand MakeReservationCommand { get; set; }
        

        public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            
            LoadReservationCommand = new LoadReservationCommand(this, hotel);
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
        }

        public static ReservationListingViewModel LoadViewModel(
            Hotel hotel, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel reservationListingViewModel = new ReservationListingViewModel(hotel, makeReservationNavigationService);
            
            reservationListingViewModel.LoadReservationCommand.Execute(null);
            
            return reservationListingViewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
