using System;
using System.Reflection;

namespace Certificate.Templates
{
    public class Notification
    {
        private object _obj;
        private Type _type;
        private PropertyInfo[] _properties;

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

        public PropertyInfo[] GetProperties()
        {
            _properties = _properties ?? _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return _properties;
        }
    }
}
