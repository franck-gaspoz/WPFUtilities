using System;

namespace WPFUtilities.Components.Services.Command.Attributes
{
    /// <summary>
    /// unactivate the default behavior that consists 
    /// to resolve a parameter of type Type as a 
    /// service type reference, that implies dependency injection,
    /// but juste as a simple Type value, when the parameter type if Type
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class ServiceReferenceAttribute : Attribute
    {

    }
}
