namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// a component provided by a component host
    /// </summary>
    public interface IComponentHostService
    {
        /// <summary>
        /// component host that provided this
        /// </summary>
        IComponentHost ComponentHost { get; set; }
    }
}
