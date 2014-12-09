---
title: Plugin a custom DataBus implementation
summary: Details how to register and plugin custom DataBus implementation into an endpoint.
tags:
- DataBus
---

NServiceBus endpoints support sending and receiving large chunks of data via the [DataBus feature](/nservicebus/attachments-databus-sample.md).

NServiceBus by default supports 2 DataBus implementations:

* `FileShare DataBus`;
* `Azure DataBus`;

Note: The `Azure DataBus` implementation is part of the Azure transport package.

It is possible to create your own DataBus implementation by implementing the `IDataBus` interface, such as in the following minimalistic sample:

<!-- import CustomDataBus -->

To configure the endpoint to use your custom DataBus implementation it is enough to register it at endpoint configuration time, such as in the following sample:

<!-- import PluginCustomDataBusV5 -->