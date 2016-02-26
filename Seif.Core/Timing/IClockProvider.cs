using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Seif.Core.Timing
{
    /// <summary>
    /// Represents a clock that can give the current time.
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// Gets Now.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized.</param>
        /// <returns>Normalized DateTime</returns>
        DateTime Normalize(DateTime dateTime);
    }

    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with local times.
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }

            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            return dateTime;
        }
    }

    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }
    }
}
