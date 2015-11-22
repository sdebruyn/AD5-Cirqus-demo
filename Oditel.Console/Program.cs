using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using d60.Cirqus;
using d60.Cirqus.EntityFramework;
using d60.Cirqus.MongoDb.Views;
using d60.Cirqus.MsSql.Config;
using d60.Cirqus.Views;
using Oditel.Cirqus.Services;
using Oditel.Cirqus.Views;
using Oditel.Services;

namespace Oditel.Console
{
    internal class Program
    {
        private static void Main()
        {
            // Cirqus configuration
            Debugger.Break();

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
                    .EventStore(e => e.UseSqlServer("CirqusDemo", "Events"))
                    .EventDispatcher(conf => conf.UseViewManagerEventDispatcher(viewManagers.ToArray()))
                    .Options(opt => opt.PurgeExistingViews(true))
                    .Create();

            IBookingService bookingService = new BookingService(processor, bookings);
            IRoomService roomService = new RoomService(processor, rooms);
            ICustomerService customerService = new CustomerService(processor, customers);


            // Create a new customer
            Debugger.Break();

            
        }
    }
}