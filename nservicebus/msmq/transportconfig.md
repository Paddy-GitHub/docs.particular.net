---
title: MSMQ transport
summary: 'Explains the mechanics of MSMQ transport, its configuration options and various other configuration settings that were at some point coupled to this transport'
tags: 
- Transports
- MSMQ
redirects:
 - nservicebus/msmqtransportconfig
---

## MSMQ

Historically MSMQ is the first transport supported by NServiceBus. In version 5 it still is by far the most commonly used one. Because of these and also the fact that MSMQ client libraries are included in .NET Base Class Library (`System.Messaging` assembly), MSMQ transport is built into the core of NServiceBus.


### Receiving algorithm

Because of the way MSMQ API has been designed i.e. polling receive that throws an exception when timeout is reached the receive algorithm is more complex than for other polling-driven transports (such as [SQLServer](/nservicebus/sqlserver/)).

The main loops starts by subscribing to `PeekCompleted` event and calling the `BeginPeek` method. When a message arrives the event is raised by the MSMQ client API. The handler for this event starts a new receiving task and waits till this new task has completed its `Receive` call. After that is calls `BeginPeek` again to wait for more messages. 


## Configuration

Because of historic reasons, the configuration for MSMQ transport has been coupled to general bus configuration in the previous versions of NServiceBus.


### MSMQ-specific

Following settings are purely related to the MSMQ:

 * `UseDeadLetterQueue` (default: `true`)
 * `UseJournalQueue` (default: `false`)
 * `UseConnectionCache` (default: `true`)
 * `UseTransactionalQueues` (default: `true`)

From version 4 onwards these settings are configured via a transport connection string (named `nservicebus/transport` for all transports). Before V4 some of these properties could be set via `MsmqMessageQueueConfig` configuration section while other (namely the connectionCache and the ability to use non-transactional queues) were not available prior to V4.

<!-- import MessageQueueConfiguration -->


### MSMQ Label

WARNING: This feature was added in Version 6 and can be used to communicate with Version 5 (and higher) endpoints. However it should **not** be used when communicating to earlier versions (2,3 or 4) since in those versions the MSMQ Label was used to communicate certain NServiceBus implementation details.

Often when debugging MSMQ using [native tools](viewing-message-content-in-msmq.md) it is helpful to have some custom text in the MSMQ Label. For example the message type or the message id. As of Version 6 the text used to apply to [Message.Label](https://msdn.microsoft.com/en-us/library/vstudio/system.messaging.message.label.aspx) can be controlled at configuration time using the `ApplyLabelToMessages` extension method. This method takes a delegate which will be passed the header collection and should return a string to use for the label. It will be called for all standard messages as well as Audits, Errors and all control messages. The only exception to this rule is received messages with corrupted headers. In some cases it may be useful to use the `Headers.ControlMessageHeader` key to determine if a message is a control message.  These messages will be forwarded to the error queue with no label applied. The returned string can be `String.Empty` for no label and must be at most 240 characters.

<!-- import ApplyLabelToMessages -->


### Failure handling & throttling

NServiceBus is designed in such a way that a user does not have to care about exception handling. All the heavy lifting is done by the framework via a [two-level retries mechanism](/nservicebus/errors/).

From V4 onwards the configuration for this mechanism is implemented in the `TransportConfig` section. 

<!-- import TransportConfig -->

 * `MaximumMessageThroughputPerSecond` (default: `0`) sets a limit on how quickly messages can be processed between all threads. Use a value of 0 to have no throughput limit.
 * `MaximumConcurrencyLevel` defines the maximum number of threads concurrently processing messages at any given point in time
 * `MaxRetries` (default: `5`) defines how many times a message is tried to be processed before is is moved to the *error queue* or passed to the [Second-Level Retries, SLR](/nservicebus/errors/automatic-retries.md) mechanism.
 * `ErrorQueue` (default: `error`) sets the name of the queue where poison messages are sent to (including messages that failed *MaxRerties* number of times with SLR disabled and messages which cannot be processed at all, e.g. having unparsable or missing headers)

In V3 some of these setting were available via `MsqmTransportConfig` section with following 

 * In V3 the `ErrorQueue` (the queue where messages that fail a configured number of times) settings can be set both via the new `MessageForwardingInCaseOfFaultConfig ` section and the old `MsmqTransportConfig` section.
 * In V3 the `MaxRetries` as well as the throttling  (`NumberOfWorkerThreads`) settings can be set only via `MsmqTransportConfig` section.