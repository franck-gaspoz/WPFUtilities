using System;

namespace WPFUtilities.Attributes
{
    /// <summary>
    /// hosted services attributes: allow component to be scaned for auto registering types in dependency injector
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class InjectableServicesAttribute : Attribute
    {

    }
}
