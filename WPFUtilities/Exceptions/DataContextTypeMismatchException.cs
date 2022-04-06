using System;
using System.Windows.Controls;

using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Exceptions
{
    /// <summary>
    /// data context type mismatch expected type by a control
    /// </summary>
    public class DataContextTypeMismatchException : Exception
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="dataContextOwner">control</param>
        /// <param name="expectedType">expected type</param>
        /// <param name="actualType">actual type</param>
        public DataContextTypeMismatchException(
            Control dataContextOwner,
            Type expectedType,
            Type actualType)
            : base($"Control '{dataContextOwner}' expects a data context type inheriting from or implementing '{expectedType.UnmangledTypeName()}'. Actual type is '{actualType.UnmangledTypeName()}'")
        { }

        /// <summary>
        /// throws this exception if data context type mismatch expected type by a control
        /// </summary>
        /// <param name="dataContextOwner">control</param>
        /// <param name="expectedType">expected data context type</param>
        /// <param name="actualDataContext">data context</param>
        /// <exception cref="DataContextTypeMismatchException">data context type mismatch expected type by a control</exception>
        public static void ThrowIfTypeMismatch(
            Control dataContextOwner,
            Type expectedType,
            object actualDataContext)
        {
            if (actualDataContext == null) return;
            if (expectedType == null) return;
            var actualType = actualDataContext.GetType();
            if (!actualType.InheritsFrom(expectedType)
                && !actualType.HasInterface(expectedType))
                throw new DataContextTypeMismatchException(
                    dataContextOwner,
                    expectedType,
                    actualType);
        }
    }
}
