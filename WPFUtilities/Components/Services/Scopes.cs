namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// service lookup scopes
    /// </summary>
    public enum Scopes
    {
        /// <summary>
        /// service are searched at global level (application host)
        /// </summary>
        Global,

        /// <summary>
        /// services are searched from component throught the component hierarchy from bottom to up
        /// </summary>
        Local
    }
}
