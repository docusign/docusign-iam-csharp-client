// This file is manually maintained and should not be overwritten by code generation.
// Contains custom enum extension methods for serialization support.

using System;
using System.Reflection;
using System.Runtime.Serialization;

#nullable enable

namespace Docusign.IAM.SDK
{
    static class EnumExtensions
    {
        /// <summary>
        /// Gets the string value from the EnumMemberAttribute of an enum value.
        /// </summary>
        /// <param name="val">The enum value to get the string representation for.</param>
        /// <returns>
        /// The Value property from the EnumMemberAttribute if present;
        /// otherwise, the string representation of the enum value.
        /// </returns>
        public static string ToEnumMemberValue(this Enum val)
        {
            if (val == null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            Type type = val.GetType();
            string name = val.ToString();
            FieldInfo? field = type.GetField(name);

            if (field == null)
            {
                return name;
            }

            EnumMemberAttribute[] attributes = (EnumMemberAttribute[])
                field.GetCustomAttributes(typeof(EnumMemberAttribute), false);

            if (attributes.Length > 0 && attributes[0] != null)
            {
                string? value = attributes[0].Value;
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return name;
        }
    }
}
