﻿using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchEvents(DbContext? context)
    {
        if (context is null) return;
        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e=>e.Entity.DomainEvents.Any())
            .Select(x => x.Entity);
        var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();
        foreach(var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}