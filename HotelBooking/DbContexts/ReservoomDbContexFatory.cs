using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelBooking.DbContexts;

public class ReservoomDbContexFatory
{
    private readonly string _connectionString;

    public ReservoomDbContexFatory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public ReservoomDbContext CreateDbContext()
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
        return new ReservoomDbContext(options);
    }
}