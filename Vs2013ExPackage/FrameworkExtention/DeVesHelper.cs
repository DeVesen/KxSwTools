using System;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention
{
    public static class DeVesHelper
    {
        public static T GetDefaultValue<T>()
        {
            return (T)DeVesHelper.GetDefaultValue(typeof(T));
        }
        public static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
