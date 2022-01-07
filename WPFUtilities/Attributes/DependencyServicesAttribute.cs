using System;

namespace WPFUtilities.Attributes
{
    /// <summary>
    /// dependency services attributes: allow an assembly to be scaned for auto registering types in dependency injector
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class DependencyServicesAttribute : Attribute
    {

    }
}
