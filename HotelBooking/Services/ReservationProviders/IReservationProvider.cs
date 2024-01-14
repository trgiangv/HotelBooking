using HotelBooking.Model;

namespace HotelBooking.Services.ReservationProviders;

public interface IReservationProvider
{
    Task<IEnumerable<Reservation>> GetAllReservations();
}