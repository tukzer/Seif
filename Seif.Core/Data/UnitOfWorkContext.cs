﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core.Data
{
    public static class UnitOfWorkContext
    {
        [ThreadStatic]
        private static IUnitOfWorkContext _threadInstance;

        public static void Bind(IUnitOfWorkContext unitOfWork)
        {
            _threadInstance = unitOfWork;
        }

        public static void Unbind()
        {
            _threadInstance = null;
        }

        /// <summary>
        /// Gets the <see cref="IUnitOfWorkContext"/> associated with the current thread context.
        /// </summary>
        /// <value>The current.</value>
        public static IUnitOfWorkContext Current
        {
            get
            {
                return _threadInstance;
            }
        }
    }
}
