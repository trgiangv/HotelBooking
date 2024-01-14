using HotelBooking.Exceptions;
using HotelBooking.Services.ReservationConflictValidators;
using HotelBooking.Services.ReservationCreators;
using HotelBooking.Services.ReservationProviders;

namespace HotelBooking.Model
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationCreator reservationCreator, IReservationProvider reservationProvider, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationCreator = reservationCreator;
            _reservationProvider = reservationProvider;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }
            
            await _reservationCreator.CreateReservation(reservation);
        }
    }
}
