﻿using NServiceBus;

public class CustomConfigOverrides
{
    public void Simple()
    {
        #region CustomConfigOverrides

        var configuration = new BusConfiguration();

        configuration.AssembliesToScan(AllAssemblies.Except("NotThis.dll"));
        configuration.Conventions().DefiningEventsAs(type => type.Name.EndsWith("Event"));
        configuration.EndpointName("MyEndpointName");
        configuration.EndpointVersion("1.2.3");

        #endregion
    }

}