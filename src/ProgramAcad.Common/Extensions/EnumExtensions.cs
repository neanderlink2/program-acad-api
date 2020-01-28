using ProgramAcad.Common.Attributes;
using System;
using System.ComponentModel;

namespace ProgramAcad.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string GetCompilerType(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (CompilerTypeAttribute[])field.GetCustomAttributes(typeof(CompilerTypeAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].CompilerType;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string GetAmbientValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (AmbientValueAttribute[])field.GetCustomAttributes(typeof(AmbientValueAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Value.ToString();
            }
            else
            {
                return value.ToString();
            }
        }

    }
}
