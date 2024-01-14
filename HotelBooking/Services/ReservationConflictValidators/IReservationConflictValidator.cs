using HotelBooking.Model;

namespace HotelBooking.Services.ReservationConflictValidators;

public interface IReservationConflictValidator
{
    Task<Reservation> GetConflictingReservation(Reservation reservation);
}