namespace HotelBooking.Model
{
    public class Reservation
    {
        public RoomId RoomId { get; }
        public string UserName { get;}
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Duration => EndTime.Subtract(StartTime);

        public Reservation(RoomId roomId, string userName, DateTime startTime, DateTime endTime)
        {
            RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
            UserName = userName;
        }
    }
}
