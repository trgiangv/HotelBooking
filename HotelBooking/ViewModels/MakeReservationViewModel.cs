using System.Windows.Input;
using HotelBooking.Command;
using HotelBooking.Model;
using HotelBooking.Services;
using HotelBooking.Stores;

namespace HotelBooking.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private int _floorNumber;
        public int FloorNumber
        {
            get => _floorNumber;
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        private DateTime _starTime = DateTime.Now;
        public DateTime StartTime
        {
            get => _starTime;
            set
            {
                _starTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private DateTime _endTime = DateTime.Now.AddDays(1);
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public MakeReservationViewModel(Hotel hotel,
                                        NavigationService reservationViewNavigationService)
        {
            
            SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);
        }
    }
}
