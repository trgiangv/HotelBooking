using HotelBooking.Exceptions;

namespace HotelBooking.Model
{
    public class Hotel
    {
        private readonly ReservationBook _resevationBook;
        public string Name { get; set; }

        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _resevationBook = reservationBook;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _resevationBook.GetAllReservations();
        }

        /// <summary>
        /// Make a reservation for a room
        /// </summary>
        /// <param name="reservation"></param>
        /// <exception cref="ReservationConflictException"></exception>
        public async Task MakeReservation(Reservation reservation)
        {
            await _resevationBook.AddReservation(reservation);
        }
    }
}
