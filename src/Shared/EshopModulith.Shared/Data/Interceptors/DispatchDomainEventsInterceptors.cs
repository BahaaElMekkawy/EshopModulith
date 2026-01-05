using EshopModulith.Shared.DDD;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EshopModulith.Shared.Data.Interceptors
{
    public class DispatchDomainEventsInterceptors(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }


        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private async Task DispatchDomainEvents(DbContext? context)
        {
            if (context is null) return;

            var aggregates = context.ChangeTracker.Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any())
                .Select(a => a.Entity);

            var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList(); //After saving the Events i will clear them to avoid dispatching them again

            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
