using System;

namespace WPFUtilities.Components.Services.Command.Attributes
{
    /// <summary>
    /// indicates that a class is a set of public properties that should map a service command Execute method parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ServiceCommandParametersAttribute : Attribute
    {

    }
}
