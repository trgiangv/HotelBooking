using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelBooking.DbContexts;

public class ReservoomDesignTimeDbContextFactory :IDesignTimeDbContextFactory<ReservoomDbContext>
{
    public ReservoomDbContext CreateDbContext(string[] args)
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=Reservoom.db").Options;
        return new ReservoomDbContext(options);
    }
}