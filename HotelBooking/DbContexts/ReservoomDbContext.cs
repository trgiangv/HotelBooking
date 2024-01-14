using HotelBooking.DTOs;
using HotelBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DbContexts;

public class ReservoomDbContext : DbContext
{
    public ReservoomDbContext(DbContextOptions options) : base(options)  
    {
        
    }

    public DbSet<ReservationDTO> Reservations { get; set; }
}