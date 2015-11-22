using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using d60.Cirqus;
using d60.Cirqus.EntityFramework;
using d60.Cirqus.Logging;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.MsSql.Config;
using d60.Cirqus.Views;
using Oditel.Cirqus.Services;
using Oditel.Cirqus.Views;
using Oditel.Models.CustomerContext;
using Oditel.Models.RoomContext;
using Oditel.Services;

namespace Oditel.Console
{
    internal class Program
    {
        private const int SleepTimeout = 5000;

        private static void Main()
        {
            // Cirqus configuration
            var viewManagers = new List<IViewManager>();

            var bookingViewManager =
                new EntityFrameworkViewManager<BookingView>(
                    ConfigurationManager.ConnectionStrings["CirqusDemo"].ConnectionString);
            var bookings = bookingViewManager.CreateContext().Views;

            var customerViewManager =
                new EntityFrameworkViewManager<CustomerView>(
                    ConfigurationManager.ConnectionStrings["CirqusDemo"].ConnectionString);
            var customers = customerViewManager.CreateContext().Views;

            var roomViewManager =
                new EntityFrameworkViewManager<RoomView>(
                    ConfigurationManager.ConnectionStrings["CirqusDemo"].ConnectionString);
            var rooms = roomViewManager.CreateContext().Views;

            var roomStatisticsViewManager =
                new MongoDbViewManager<RoomStatisticsView>(
                    ConfigurationManager.ConnectionStrings["CirqusMongoDemo"].ConnectionString);

            viewManagers.Add(bookingViewManager);
            viewManagers.Add(customerViewManager);
            viewManagers.Add(roomStatisticsViewManager);
            viewManagers.Add(roomViewManager);

            var processor =
                CommandProcessor.With()
                    .Logging(l => l.UseConsole(Logger.Level.Debug))
                    .EventStore(e => e.UseSqlServer("CirqusDemo", "Events"))
                    .EventDispatcher(conf => conf.UseViewManagerEventDispatcher(viewManagers.ToArray()))
                    .Options(opt => opt.PurgeExistingViews(true))
                    .Create();

            IBookingService bookingService = new BookingService(processor, bookings);
            IRoomService roomService = new RoomService(processor, rooms);
            ICustomerService customerService = new CustomerService(processor, customers);

            Thread.Sleep(SleepTimeout);

            // Create rooms
            Debugger.Break();

            var room1 = roomService.AddRoom(true, false,
                new Bathroom(new Dimensions(10, 10, 10), true, true, false, true), new Dimensions(20, 20, 20),
                new Bed(Bed.Size.King));
            PrintDebug($"Created room with ID {room1}.");

            var room2 = roomService.AddRoom(true, false,
                new Bathroom(new Dimensions(9, 9, 9), true, false, true, true), new Dimensions(15, 15, 15),
                new Bed(Bed.Size.Queen));
            PrintDebug($"Created room with ID {room2}.");

            var room3 = roomService.AddRoom(true, false,
                new Bathroom(new Dimensions(8, 8, 8), true, true, false, true), new Dimensions(10, 10, 10),
                new Bed(Bed.Size.Full));
            PrintDebug($"Created room with ID {room3}.");

            var room4 = roomService.AddRoom(true, true,
                new Bathroom(new Dimensions(7, 7, 7), false, true, false, true), new Dimensions(5, 5, 5),
                new Bed(Bed.Size.Single));
            PrintDebug($"Created room with ID {room4}.");

            Thread.Sleep(SleepTimeout);

            // Get all rooms
            Debugger.Break();

            var allRooms = roomService.GetAllRooms();
            PrintDebug($"Found {allRooms.Count} rooms!");
            foreach (var room in allRooms)
            {
                PrintDebug($"Found room: {room}");
            }

            // Create customers
            Debugger.Break();

            var customer1 = customerService.AddCustomer("Jan Janssens", "jan@janssens.be",
                new Address("Straat Zonder Naam 1", null, "1000", "Brussels", Address.ECountry.Belgium));
            PrintDebug($"Created customer with ID {customer1}.");

            var customer2 = customerService.AddCustomer("Peter Peeters", "peter@peeters.be",
                new Address("Straat Zonder Naam 2", null, "2000", "Antwerp", Address.ECountry.Belgium));
            PrintDebug($"Created customer with ID {customer2}.");

            Thread.Sleep(SleepTimeout);

            // Get all customers
            Debugger.Break();

            var allCustomers = customerService.GetAllCustomers();
            PrintDebug($"Found {allCustomers.Count} customers!");
            foreach (var customer in allCustomers)
            {
                PrintDebug($"Found customer: {customer}");
            }

            // Create booking
            Debugger.Break();

            var booking1 = bookingService.AddBooking(DateTimeOffset.UtcNow.AddDays(7), DateTimeOffset.UtcNow.AddDays(14),
                true, customer1, room1, room2);
            PrintDebug($"Created booking with ID {booking1}.");

            Thread.Sleep(SleepTimeout);

            // Retrieve booking
            Debugger.Break();

            var booking1FromService = bookingService.GetBookingById(booking1);
            PrintDebug($"Found booking: {booking1FromService}");

            // Remove booking
            Debugger.Break();

            var removedBooking1 = bookingService.RemoveBooking(booking1);
            PrintDebug($"Booking removed? {removedBooking1}");

            Thread.Sleep(SleepTimeout);

            // Retrieve bookings
            Debugger.Break();

            var allBookings = bookingService.GetAllBookings();
            PrintDebug($"Found {allBookings.Count} bookings!");

            System.Console.ReadKey();
        }

        private static void PrintDebug(string debugInfo)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine(debugInfo);
            System.Console.ResetColor();
        }
    }
}