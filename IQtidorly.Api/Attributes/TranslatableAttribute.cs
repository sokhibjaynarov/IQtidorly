using System;

namespace IQtidorly.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class TranslatableAttribute : Attribute
    {
    }
}
