using System;

namespace ConnectDB
{
    public static class ParameterHelper
    {
        public static object GetNullableValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            return obj;
        }
    }
}
