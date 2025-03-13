using System;
using System.Threading;
using LionBitcoin.Payments.Service.Application.Domain.Events.Base;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Configs;

public class EventsCache<TEvent> where TEvent : BaseEvent
{
    private static Lock _populateCacheLock = new();
    private static EventsCache<TEvent>? _cachedMetadata = null;

    public string EventName { get; set; }

    public static EventsCache<TEvent> GetCachedMetadata()
    {
        TryPopulateCache();
        return _cachedMetadata;
    }

    private static void TryPopulateCache()
    {
        if (_cachedMetadata == null)
        {
            lock (_populateCacheLock)
            {
                if (_cachedMetadata == null)
                {
                    Type eventType = typeof(TEvent);
                    _cachedMetadata = new EventsCache<TEvent>()
                    {
                        EventName = eventType.Name,
                    };
                }
            }
        }
    }
}