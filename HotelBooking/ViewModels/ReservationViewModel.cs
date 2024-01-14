using HotelBooking.Model;

namespace HotelBooking.ViewModels
{
    public class ReservationViewModel :ViewModelBase
    {
        private Reservation _reservation;

        public string RoomId => _reservation.RoomId.ToString();
        public string UserName => _reservation.UserName;
        public string StartTime => _reservation.StartTime.ToString("d");
        public string EndTime => _reservation.EndTime.ToString("d");

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
