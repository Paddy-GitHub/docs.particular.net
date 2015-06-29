using System;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence;

class Program
{
    static void Main()
    {
        Console.Title = "RemoteSite";
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Gateway.RemoteSite");
        busConfiguration.EnableInstallers();
        busConfiguration.EnableFeature<Gateway>();
        busConfiguration.UsePersistence<RavenDBPersistence>();

        using (IBus bus = Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("\r\nPress any key to stop program\r\n");
            Console.ReadKey();
        }
    }
}

