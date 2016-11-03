using System;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention
{
    public static class DeVesValidator
    {
        public static bool IsTypeOf<T>(params Type[] types)
        {
            return DeVesValidator.IsTypeOf(typeof(T), types);
        }
        public static bool IsTypeOf(Type t, params Type[] types)
        {
            if (types != null)
            {
                foreach (var _type in types)
                {
                    if (t == _type)
                        return true;
                }
            }
            return false;
        }
    }
}
