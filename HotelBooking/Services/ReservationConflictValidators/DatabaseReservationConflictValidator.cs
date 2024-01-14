using HotelBooking.DbContexts;
using HotelBooking.DTOs;
using HotelBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services.ReservationConflictValidators;

public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservoomDbContexFatory _dbContexFatory;

    public DatabaseReservationConflictValidator(ReservoomDbContexFatory dbContexFatory)
    {
        _dbContexFatory = dbContexFatory;
    }

    public async Task<Reservation> GetConflictingReservation(Reservation reservation)
    {
        using (ReservoomDbContext context = _dbContexFatory.CreateDbContext())
        {
            ReservationDTO reservationDto = await context.Reservations
                .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                .Where(r => r.StartTime < reservation.EndTime)
                .Where(r => r.EndTime > reservation.StartTime)
                .FirstOrDefaultAsync();
            if (reservationDto == null)
            {
                return null;
            }
            
            return ToReservation(reservationDto);
        }
    }

    private static Reservation ToReservation(ReservationDTO reservationDto)
    {
        return new Reservation(new RoomId(reservationDto.RoomNumber, reservationDto.FloorNumber),
            reservationDto.UserName, reservationDto.StartTime, reservationDto.EndTime);
    }
}