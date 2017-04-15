using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMIWrapper
{
    public class WMIWrapperBase : IDisposable
    {
        protected ManagementObject mo { get; private set; }
        public WMIWrapperBase(ManagementObject mo)
        {
            this.mo = mo;
        }
        protected T? GetNullableProperty<T>(string PropertyName, Func<object, T?> Converter = null)
            where T : struct
        {
            try
            {
                var obj = mo.Properties[PropertyName].Value;
                return Converter == null ? (T)obj
                    : Converter(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected T GetProperty<T>(string PropertyName, T defaultValue = default(T), Func<object, T> Converter = null)
        {
            try
            {
                var obj = mo.Properties[PropertyName].Value;
                return obj == null ? defaultValue
                    : Converter == null ? (T)obj
                    : Converter(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        protected T GeProperty<T>(string PropertyName, Func<T> GetDefaultValue, Func<object, T> Converter = null)
        {
            try
            {
                var obj = mo.Properties[PropertyName].Value;
                return obj == null ? GetDefaultValue()
                    : Converter == null ? (T)obj
                    : Converter(obj);
            }
            catch (Exception)
            {
                return GetDefaultValue();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        bool isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    mo?.Dispose();
                }
            }
            isDisposed = true;
        }
        ~WMIWrapperBase()
        {
            Dispose(false);
        }
    }
}
