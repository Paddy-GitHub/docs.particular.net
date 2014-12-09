﻿using System;
using NServiceBus;

public class Scheduler
{
    #region Scheduler
    public class ScheduleMyTasks : IWantToRunWhenBusStartsAndStops
    {
        IBus bus;
        Schedule schedule;

        public ScheduleMyTasks(IBus bus, Schedule schedule)
        {
            this.bus = bus;
            this.schedule = schedule;
        }

        public void Start()
        {
            // To send a message every 5 minutes
            schedule.Every(TimeSpan.FromMinutes(5), () => bus.SendLocal(new MyMessage()));

            // Name a schedule task and invoke it every 5 minutes
            schedule.Every(TimeSpan.FromMinutes(5), "Task name", () => bus.SendLocal(new MyMessage()));
        }

        public void Stop()
        {
        }
    }

    #endregion
    public class MyMessage : IMessage
    {
    }
}