using Seif.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Seif.Core
{
    public static class LogManager
    {
        private static readonly ReaderWriterLockSlim _cacheLocker = new ReaderWriterLockSlim();

        private static bool _isLog4NetAvailable = false;
        private static Dictionary<Type, ILogger> _loggerCache = new Dictionary<Type, ILogger>();

        static LogManager()
        {
            try
            {
                Assembly.Load("log4net");
                _isLog4NetAvailable = true;
            }
            catch (FileNotFoundException)
            {
                _isLog4NetAvailable = false;
            }
        }

        public static ILogger GetLogger(Type type)
        {
            ILogger logger;
            _cacheLocker.EnterReadLock();

            try
            {
                if (_loggerCache.TryGetValue(type, out logger))
                {
                    return logger;
                }
            }
            finally
            {
                _cacheLocker.ExitReadLock();
            }

            _cacheLocker.EnterWriteLock();
            try
            {
                // double check, as while the read-lock was released, the dictionary could have been modified
                if (_loggerCache.TryGetValue(type, out logger))
                {
                    return logger;
                }

                logger = CreateLoggerForType(type);
                _loggerCache.Add(type, logger);
                return logger;
            }
            finally
            {
                _cacheLocker.ExitWriteLock();
            }
        }

        private static ILogger CreateLoggerForType(Type type)
        {
            if (_isLog4NetAvailable)
            {
                return (ILogger)Activator.CreateInstance(typeof(Log4NetLogger), new object[] { type });
            }
            else
            {
                return new TraceLogger();
            }
        }
    }
}
