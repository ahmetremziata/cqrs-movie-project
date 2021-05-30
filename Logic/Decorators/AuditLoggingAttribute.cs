using System;

namespace Logic.Decorators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class AuditLoggingAttribute : Attribute
    {
        public AuditLoggingAttribute()
        {
        }
    }
}