using HotelBooking.DbContexts;
using HotelBooking.DTOs;
using HotelBooking.Model;

namespace HotelBooking.Services.ReservationCreators;

public class DatabaseReservationCreator : IReservationCreator
{
    private readonly ReservoomDbContexFatory _dbContexFatory;

    public DatabaseReservationCreator(ReservoomDbContexFatory dbContexFatory)
    {
        _dbContexFatory = dbContexFatory;
    }
    public async Task CreateReservation(Reservation reservation)
    {
        using (ReservoomDbContext context = _dbContexFatory.CreateDbContext())
        {
            ReservationDTO reservationDTO = ToReservationDTO(reservation);
            context.Reservations.Add(reservationDTO);
            await context.SaveChangesAsync();
        }
    }

    private ReservationDTO ToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomId?.FloorNumber ?? 0,
            RoomNumber = reservation.RoomId?.RoomNumber ?? 0,
            UserName = reservation.UserName,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime
        };
    }
}