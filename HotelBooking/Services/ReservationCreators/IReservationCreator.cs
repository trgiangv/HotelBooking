using HotelBooking.Model;

namespace HotelBooking.Services.ReservationCreators;

public interface IReservationCreator
{
    Task CreateReservation(Reservation reservation);
}