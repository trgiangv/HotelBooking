using System.Windows;
using System.Windows.Navigation;
using HotelBooking.Command;
using HotelBooking.DbContexts;
using HotelBooking.Model;
using HotelBooking.Services.ReservationConflictValidators;
using HotelBooking.Services.ReservationCreators;
using HotelBooking.Services.ReservationProviders;
using HotelBooking.Stores;
using HotelBooking.ViewModels;
using Microsoft.EntityFrameworkCore;
using NavigationService = HotelBooking.Services.NavigationService;

namespace HotelBooking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=Reservoom.db";
        private Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private ReservoomDbContexFatory _reservoomDbContexFatory;

        public App()
        {
            _reservoomDbContexFatory = new ReservoomDbContexFatory(CONNECTION_STRING);
            
            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContexFatory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContexFatory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContexFatory);
            
            ReservationBook reservationBook = new ReservationBook(reservationCreator, reservationProvider, reservationConflictValidator);
            
            _hotel = new Hotel("GiangVu Suites", reservationBook);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(CONNECTION_STRING)
                .Options;
            using (ReservoomDbContext dbContext = _reservoomDbContexFatory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateReservationViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListingViewModel CreateReservationViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }

}
