using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWorkContext CreateUnitOfWork()
        {
            if (UnitOfWorkContext.Current != null)
            {
                throw new InvalidOperationException("There is already a unit of work created for this context.");
            }

            //var store = ApplicationContext.Get<IEventStore>();
            //var bus = ApplicationContext.Get<IEventBus>();
            //var snapshotStore = ApplicationContext.Get<ISnapshotStore>();
            //var snapshottingPolicy = ApplicationContext.Get<ISnapshottingPolicy>();
            //var aggregateCreationStrategy = ApplicationContext.Get<IAggregateRootCreationStrategy>();
            //var aggregateSnappshotter = ApplicationContext.Get<IAggregateSnapshotter>();

            //var repository = new DomainRepository(aggregateCreationStrategy, aggregateSnappshotter);
            //var unitOfWork = new UnitOfWork(commandId, repository, store, snapshotStore, bus, snapshottingPolicy);
            //UnitOfWorkContext.Bind(unitOfWork);
            //return unitOfWork;
            return null;
        }
    }
}
