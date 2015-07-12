﻿namespace Snippets4.Routing.EndpointMapping.ConfigurationSource
{
    using NServiceBus;

    public class Usage 
    {
        public Usage()
        {
            #region inject-endpoint-mapping-configuration-source
            Configure.With()
                .CustomConfigurationSource(new ConfigurationSource());
            #endregion
        }
    }
}