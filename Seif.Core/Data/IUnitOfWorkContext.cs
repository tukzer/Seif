using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core.Data
{
    public interface IUnitOfWorkContext : IDisposable
    {
        IUnitOfWorkContext Begin();

        /// <summary>
        /// Accept all the changes that has been made within this context. All <see cref="IEvent">events</see>
        /// that has been occured will be stored and published.
        /// </summary>
        void Accept();
    }
}
