using System;
using System.Reflection;

namespace Email.Templates
{
    public class Notification
    {
        private object _obj;
        private Type _type;

        public Notification(object obj)
        {
            _obj = obj;
            _type = obj.GetType();
        }

        public string this[string key]
        {
            get
            {
                var property = _type.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (object.Equals(property, null))
                    return null;

                var value = property.GetValue(_obj);
                if (object.Equals(value, null))
                    return null;

                return value.ToString();
            }
        }
    }
}
