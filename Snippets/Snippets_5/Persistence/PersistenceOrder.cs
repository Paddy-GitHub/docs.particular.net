﻿using NServiceBus;
using NServiceBus.Persistence;

class PersistenceOrder
{
    void Setup()
    {
        #region PersistenceOrder_Correct
        var config = new BusConfiguration();

        config.UsePersistence<RavenDBPersistence>();
            
        config.UsePersistence<NHibernatePersistence>()
            .For(Storage.Outbox);

        config.UsePersistence<InMemoryPersistence>()
            .For(Storage.GatewayDeduplication);
        #endregion
    }

    void Setup3()
    {
        #region PersistenceOrder_Explicit
        var config = new BusConfiguration();

        config.UsePersistence<NHibernatePersistence>()
            .For(Storage.Outbox);

        config.UsePersistence<InMemoryPersistence>()
            .For(Storage.GatewayDeduplication);

        config.UsePersistence<RavenDBPersistence>()
            .For(Storage.Sagas, Storage.Subscriptions, Storage.Timeouts);
        #endregion
    }

    void Setup2()
    {
        #region PersistenceOrder_Incorrect
        var config = new BusConfiguration();

        config.UsePersistence<NHibernatePersistence>()
            .For(Storage.Outbox);

        config.UsePersistence<InMemoryPersistence>()
            .For(Storage.GatewayDeduplication);

        // This one will override the above settings!
        config.UsePersistence<RavenDBPersistence>();
        #endregion
    }
}
