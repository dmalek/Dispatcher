﻿namespace Dispatcher.Benchamrks
{
    internal class DataChangedEvent : INotificationHandler<DataChanged>
    {
        public async Task ReceiveAsync(DataChanged notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
