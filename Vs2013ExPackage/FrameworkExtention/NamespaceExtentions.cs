using System;
using System.Data;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention
{
    public static class DeVesExtensionObject
    {
        public static T ParseTo<T>(this object value)
        {
            return value.ParseTo(DeVesHelper.GetDefaultValue<T>());
        }
        public static T ParseTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                try
                {
                    if (value.IsTypeOf(typeof(T)))
                    {
                        return (T)value;
                    }

                    else if (DeVesValidator.IsTypeOf<T>(typeof(string)))
                    {
                        return (T)(object)value.ToString();
                    }

                    else if (DeVesValidator.IsTypeOf<T>(typeof(Int16), typeof(Int16?)))
                    {
                        return (T)(object)Convert.ToInt16(value);
                    }
                    else if (DeVesValidator.IsTypeOf<T>(typeof(Int32), typeof(Int32?), typeof(int), typeof(int?)))
                    {
                        return (T)(object)Convert.ToInt32(value);
                    }
                    else if (DeVesValidator.IsTypeOf<T>(typeof(Int64), typeof(Int64?), typeof(long), typeof(long?)))
                    {
                        return (T)(object)Convert.ToInt64(value);
                    }

                    else if (DeVesValidator.IsTypeOf<T>(typeof(double), typeof(double?), typeof(Double), typeof(Double?)))
                    {
                        return (T)(object)Convert.ToDouble(value);
                    }
                    else if (DeVesValidator.IsTypeOf<T>(typeof(float), typeof(float?)))
                    {
                        return (T)(object)Convert.ToDouble(value);
                    }

                    else if (DeVesValidator.IsTypeOf<T>(typeof(DateTime), typeof(DateTime?)))
                    {
                        return (T)(object)Convert.ToDateTime(value);
                    }

                    else if (DeVesValidator.IsTypeOf<T>(typeof(bool), typeof(bool?), typeof(Boolean), typeof(Boolean?)))
                    {
                        return (T)(object)Convert.ToDateTime(value);
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return defaultValue;
        }

        public static bool IsTypeOf(this object value, params Type[] types)
        {
            if (value != null)
            {
                return DeVesValidator.IsTypeOf(value.GetType(), types);
            }
            return false;
        }


        public static bool HasValue(this object value)
        {
            return value != null;
        }
        public static bool IsNullOrEmpty(this object value, bool trimmed = true)
        {
            if (!value.HasValue())
                return true;

            if (string.IsNullOrEmpty(value.ToString()))
                return true;

            if (trimmed && string.IsNullOrEmpty(value.ToString().Trim()))
                return true;

            return false;
        }
    }



    public static class DeVesExtensionString
    {
        public static bool HasValue(this string value)
        {
            return ((object)value).HasValue();
        }
        public static bool IsNullOrEmpty(this string value, bool trimmed = true)
        {
            return ((object)value).IsNullOrEmpty(trimmed);
        }

        public static bool IsEqual(this string value, string valueToCompare, bool caseSensitive = false)
        {
            if (value == valueToCompare)
            {
                return true;
            }

            if (!caseSensitive && value.HasValue() && valueToCompare.HasValue() &&
                string.Compare(value, valueToCompare, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }

        public static string FetchPart(this string value, string tocken)
        {
            string _result;
            if (!value.TryFetchPart(tocken, out _result))
                _result = value;
            return _result;
        }
        public static string FetchPart(this string value, string tocken, bool holdTocken)
        {
            string _result;
            if (!value.TryFetchPart(tocken, holdTocken, out _result))
                _result = value;
            return _result;
        }
        public static string FetchPart(this string value, int fromIndex, int length)
        {
            string _result;
            if (!value.TryFetchPart(fromIndex, length, out _result))
                _result = value;
            return _result;
        }

        public static bool TryFetchPart(this string value, string tocken, out string result)
        {
            return value.TryFetchPart(tocken, false, out result);
        }
        public static bool TryFetchPart(this string value, string tocken, bool holdTocken, out string result)
        {
            result = null;

            if (value.IsNullOrEmpty() || tocken.IsNullOrEmpty())
            {
                return false;
            }

            int _startIndex = 0;
            int _index = value.IndexOf(tocken, _startIndex, StringComparison.Ordinal);
            if (_index < _startIndex)
            {
                return false;
            }

            int _length = _index - _startIndex + (holdTocken ? tocken.Length : 0);

            return value.TryFetchPart(_startIndex, _length, out result);
        }
        public static bool TryFetchPart(this string value, int fromIndex, int length, out string result)
        {
            result = null;

            if (fromIndex >= 0 && length > 0 &&
                !value.IsNullOrEmpty()
                && value.Length > fromIndex)
            {
                length = Math.Min(value.Length - fromIndex, length);

                result = value.Substring(fromIndex, length);

                return true;
            }

            return false;
        }


        public static T ParseTo<T>(this string value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this string value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }



    public static class DeVesExtensionInt16
    {
        public static T ParseTo<T>(this Int16 value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this Int16 value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }

    public static class DeVesExtensionInt32
    {
        public static T ParseTo<T>(this Int32 value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this Int32 value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }

    public static class DeVesExtensionInt64
    {
        public static T ParseTo<T>(this Int64 value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this Int64 value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }



    public static class DeVesExtensionDouble
    {
        public static T ParseTo<T>(this Double value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this Double value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }

    public static class DeVesExtensionFloat
    {
        public static T ParseTo<T>(this float value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this float value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }



    public static class DeVesExtensionDateTime
    {
        public static T ParseTo<T>(this DateTime value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this DateTime value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }



    public static class DeVesExtensionBoolean
    {
        public static T ParseTo<T>(this Boolean value)
        {
            return ((object)value).ParseTo<T>();
        }
        public static T ParseTo<T>(this Boolean value, T defaultValue)
        {
            return ((object)value).ParseTo(defaultValue);
        }
    }



    public static class DeVesExtensionDataTable
    {
    }

    public static class DeVesExtensionDataRow
    {
        public static T GetField<T>(this DataRow dataRow, string columnName)
        {
            return dataRow.GetField(columnName, DeVesHelper.GetDefaultValue<T>());
        }
        public static T GetField<T>(this DataRow dataRow, string columnName, T defaultValue)
        {
            if (columnName.IsNullOrEmpty())
                return DeVesHelper.GetDefaultValue<T>();

            if (!dataRow.Table.Columns.Contains(columnName))
                return DeVesHelper.GetDefaultValue<T>();

            return dataRow[columnName].ParseTo(defaultValue);
        }
    }
}
