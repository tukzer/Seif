using System;
using System.Linq;

namespace Seif.Rpc.Common
{
    public static class Asserts
    {
        public static void NotNull(object obj, string message)
        {
            NotTrue(() => obj == null, message);
        }

        public static void NotNullOrEmpty(string source, string message)
        {
            NotTrue(string.IsNullOrEmpty(source), message);
        }

        public static void NotInRange(int value, int[] expectedValues, string message)
        {
            NotTrue(expectedValues != null && (expectedValues.Any() && expectedValues.Contains(value)), message);
        }

        public static void InRange(int value, int[] expectedValues, string message)
        {
            if (expectedValues.Any() && expectedValues.Contains(value))
                return;
            NotTrue(true, message);
        }

        public static void GreaterAndEqualsThan(Decimal value, Decimal minValue, string message)
        {
            IsTrue(value >= minValue, message);
        }

        public static void GreaterThan(Decimal value, Decimal minValue, string message)
        {
            IsTrue(value > minValue, message);
        }

        public static void LessAndEqualsThan(Decimal value, Decimal minValue, string message)
        {
            IsTrue(value <= minValue, message);
        }

        public static void LessThan(Decimal value, Decimal minValue, string message)
        {
            IsTrue(value < minValue, message);
        }

        public static void IsInt(string source, string message)
        {
            int result;
            if (int.TryParse(source, out result))
                return;
            IsTrue(false, message);
        }

        public static void NotTrue(Func<bool> equFunc, string message)
        {
            NotTrue(equFunc(), message);
        }

        public static void NotTrue(bool condition, string message)
        {
            if (condition)
                throw new ArgumentException(message);
        }

        public static void IsTrue(bool condition, string message)
        {
            NotTrue(!condition, message);
        }
    }
}