using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace pwExtensionsLibrary
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this string text)
        {
            T res = default(T);
            decimal numericValue;

            if ((typeof(T) == typeof(bool) || typeof(T) == typeof(bool?)) &&
                text is string &&
                decimal.TryParse((string)text, out numericValue))
            {
                if (numericValue == 0)
                    res = (T)(object)false;
                else
                    res = (T)(object)true;
                return res;
            }

            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(text.GetType()))
                res = (T)tc.ConvertFrom(text);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(text.GetType());
                if (tc.CanConvertTo(typeof(T)))
                    res = (T)tc.ConvertTo(text, typeof(T));
                else
                    throw new NotSupportedException();
            }
            return res;
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///     If the <paramref name="value"/> can't be convert even if the types are 
        ///     convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }

        /// <summary>
        /// Converts the specified value to a different type.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <returns>An universal converter suppliying additional target conversion methods</returns>
        /// <example>
        /// <code>
        /// 	var value = "123";
        /// 	var numeric = value.ConvertTo().ToInt32();
        /// </code>
        /// </example>
        public static IConverter<T> ConvertTo<T>(this T value)
        {
            return new Converter<T>(value);
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>
        ///     If the <paramref name="value"/> can't be convert even if the types are 
        ///     convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                if (value.GetType() == targetType) return (T)value;

                var converter = TypeDescriptor.GetConverter(value);
                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                        return (T)converter.ConvertTo(value, targetType);
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                        return (T)converter.ConvertFrom(value);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Converts an object to the specified target type or returns the default value if
        ///     those 2 types are not convertible.
        ///     <para>Any exceptions are optionally ignored (<paramref name="ignoreException"/>).</para>
        ///     <para>
        ///     If the exceptions are not ignored and the <paramref name="value"/> can't be convert even if 
        ///     the types are convertible with each other, an exception is thrown.</para>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value">The value.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <param name = "ignoreException">if set to <c>true</c> ignore any exception.</param>
        /// <returns>The target type</returns>
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (ignoreException)
            {
                try
                {
                    return value.ConvertTo<T>();
                }
                catch
                {
                    return defaultValue;
                }
            }
            return value.ConvertTo<T>();
        }

        /// <summary>
        /// Creates a deep clone of the current System.Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The original object.</param>
        /// <returns>A clone of the original object</returns>
        public static T DeepClone<T>(this T item) where T : ISerializable
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, item);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// Returns TRUE, if specified target reference is equals with null reference.
        /// Othervise returns FALSE.
        /// </summary>
        /// <typeparam name = "T">Type of target.</typeparam>
        /// <param name = "target">Target reference. Can be null.</param>
        /// <remarks>
        /// Some types has overloaded '==' and '!=' operators.
        /// So the code "null == ((MyClass)null)" can returns <c>false</c>.
        /// The most correct way how to test for null reference is using "System.Object.ReferenceEquals(object, object)" method.
        /// However the notation with ReferenceEquals method is long and uncomfortable - this extension method solve it.
        /// 
        /// Contributed by tencokacistromy, http://www.codeplex.com/site/users/view/tencokacistromy
        /// </remarks>
        /// <example>
        /// MyClass someObject = GetSomeObject();
        /// if ( someObject.IsNotNull() ) { /* the someObject is not null */ }
        /// else { /* the someObject is null */ }
        /// </example>
        public static bool IsNotNull<T>(this T target)
        {
            var result = !ReferenceEquals(target, null);
            return result;
        }

        /// <summary>
        /// c# version of "Between" clause of sql query with including option
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="includeStart"></param>
        /// <param name="includeEnd"></param>
        /// <url>https://www.extensionmethod.net/csharp/between/between</url>
        /// <returns></returns>
        /// <example>
        /// int start = 10;
        /// int end = 20;
        /// int num = 10;
        ///
        /// bool isBetween = num.Between(start, end);
        /// bool isNotBetween = num.Between(start, end, false, false);
        /// </example>
        public static bool Between<T>(this T item, T start, T end, bool includeStart = true, bool includeEnd = true)
        {
            return
                (
                    (includeStart && Comparer<T>.Default.Compare(item, start) >= 0)
                    ||
                    (!includeStart && Comparer<T>.Default.Compare(item, start) > 0)
                )
                &&
                (
                    (includeEnd && Comparer<T>.Default.Compare(item, end) <= 0)
                    ||
                    (!includeEnd && Comparer<T>.Default.Compare(item, end) < 0)
                );
        }

        /// <summary>
        /// Determines if the Item is contained within the listed array
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="item">The calling item</param>
        /// <param name="range">An array or comma separated list of the items to check against the calling</param>
        /// <returns>True if item is found in list</returns>
        /// <remarks>Provides syntatic sugar by reordering the subject of the IEnumerable.Contains method</remarks>
        /// <url>http://www.codingeverything.com/2013/12/InFunction.html</url>
        public static bool In<T>(this T item, params T[] range)
        {
            return range.Contains(item);
        }

    }
}
