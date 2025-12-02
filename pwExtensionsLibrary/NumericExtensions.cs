using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwExtensionsLibrary
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Determines whether the specified numeric value is an even number.
        /// </summary>
        /// <typeparam name="T">The numeric type (int, long, short, byte, decimal, double, float)</typeparam>
        /// <param name="value">The numeric value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// int value = 42;
        /// bool isEven = value.IsEven(); // returns true
        /// 
        /// decimal value2 = 13.5m;
        /// bool isEven2 = value2.IsEven(); // returns false
        /// </example>
        public static bool IsEven<T>(this T value) where T : struct, IConvertible
        {
            // Convert to decimal for universal handling
            decimal decimalValue = Convert.ToDecimal(value);
            return decimalValue % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified numeric value is an odd number.
        /// </summary>
        /// <typeparam name="T">The numeric type (int, long, short, byte, decimal, double, float)</typeparam>
        /// <param name="value">The numeric value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// int value = 13;
        /// bool isOdd = value.IsOdd(); // returns true
        /// </example>
        public static bool IsOdd<T>(this T value) where T : struct, IConvertible
        {
            // Convert to decimal for universal handling
            decimal decimalValue = Convert.ToDecimal(value);
            return decimalValue % 2 != 0;
        }

        // Overloads spécifiques conservées pour la performance et la clarté
        
        /// <summary>
        /// Determines whether the specified integer is an even number.
        /// </summary>
        /// <param name="value">The integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// int value = 42;
        /// bool isEven = value.IsEven(); // returns true
        /// 
        /// int value2 = 13;
        /// bool isEven2 = value2.IsEven(); // returns false
        /// </example>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified long integer is an even number.
        /// </summary>
        /// <param name="value">The long integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// long value = 42L;
        /// bool isEven = value.IsEven(); // returns true
        /// </example>
        public static bool IsEven(this long value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified short integer is an even number.
        /// </summary>
        /// <param name="value">The short integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// short value = 42;
        /// bool isEven = value.IsEven(); // returns true
        /// </example>
        public static bool IsEven(this short value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified byte is an even number.
        /// </summary>
        /// <param name="value">The byte value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// byte value = 42;
        /// bool isEven = value.IsEven(); // returns true
        /// </example>
        public static bool IsEven(this byte value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified decimal number is an even number.
        /// </summary>
        /// <param name="value">The decimal value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an even integer; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// decimal value = 42m;
        /// bool isEven = value.IsEven(); // returns true
        /// 
        /// decimal value2 = 42.5m;
        /// bool isEven2 = value2.IsEven(); // returns false
        /// </example>
        public static bool IsEven(this decimal value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified double number is an even number.
        /// </summary>
        /// <param name="value">The double value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an even integer; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// double value = 42.0;
        /// bool isEven = value.IsEven(); // returns true
        /// 
        /// double value2 = 42.5;
        /// bool isEven2 = value2.IsEven(); // returns false
        /// </example>
        public static bool IsEven(this double value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified float number is an even number.
        /// </summary>
        /// <param name="value">The float value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an even integer; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// float value = 42.0f;
        /// bool isEven = value.IsEven(); // returns true
        /// 
        /// float value2 = 42.5f;
        /// bool isEven2 = value2.IsEven(); // returns false
        /// </example>
        public static bool IsEven(this float value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Determines whether the specified integer is an odd number.
        /// </summary>
        /// <param name="value">The integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// int value = 13;
        /// bool isOdd = value.IsOdd(); // returns true
        /// </example>
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified long integer is an odd number.
        /// </summary>
        /// <param name="value">The long integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this long value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified short integer is an odd number.
        /// </summary>
        /// <param name="value">The short integer value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this short value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified byte is an odd number.
        /// </summary>
        /// <param name="value">The byte value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this byte value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified decimal number is an odd number.
        /// </summary>
        /// <param name="value">The decimal value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an odd integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this decimal value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified double number is an odd number.
        /// </summary>
        /// <param name="value">The double value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an odd integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this double value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified float number is an odd number.
        /// </summary>
        /// <param name="value">The float value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is an odd integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd(this float value)
        {
            return value % 2 != 0;
        }
    }
}
