using HotelBooking.DbContexts;
using HotelBooking.DTOs;
using HotelBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services.ReservationProviders;

public class DatabaseReservationProvider : IReservationProvider
{
    private readonly ReservoomDbContexFatory _dbContexFatory;

    public DatabaseReservationProvider(ReservoomDbContexFatory dbContexFatory)
    {
        _dbContexFatory = dbContexFatory;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        using (ReservoomDbContext context = _dbContexFatory.CreateDbContext())
        {
            IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();
            return reservationDTOs.Select(r => ToReservation(r));
        }
    }

    private static Reservation ToReservation(ReservationDTO reservationDto)
    {
        return new Reservation(new RoomId(reservationDto.RoomNumber, reservationDto.FloorNumber),
            reservationDto.UserName, reservationDto.StartTime, reservationDto.EndTime);
    }
}