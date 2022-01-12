using System;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// dependency services attributes: allow an assembly to be scaned for auto registering types in dependency injector
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class ServiceDependenciesAttribute : Attribute
    {

    }
}
