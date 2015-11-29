using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Seif.Core.Data
{
    public abstract class UnitOfWorkBase : IUnitOfWorkContext
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        protected UnitOfWorkBase()
        {
            Contract.Ensures(IsDisposed == false);

            Log.DebugFormat("Creating new unit of work on thread {0}",
                            Thread.CurrentThread.ManagedThreadId);

            IsDisposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="UnitOfWork"/> is reclaimed by garbage collection.
        /// </summary>
        ~UnitOfWorkBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Contract.Ensures(IsDisposed == true);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            Contract.Ensures(IsDisposed == true);

            if (!IsDisposed)
            {
                if (disposing)
                {
                    Log.DebugFormat("Disposing unit of work {0}", this);
                }

                IsDisposed = true;
            }
        }

        public abstract void Accept();
    }
}
