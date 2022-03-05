namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// parent host changed event args
    /// </summary>
    public class ParentHostChangedEventArgs
    {
        /// <summary>
        /// target host
        /// </summary>
        public IComponentHost Host { get; set; }

        /// <summary>
        /// old parent
        /// </summary>
        public IComponentHost OldParent { get; set; }

        /// <summary>
        /// new parent
        /// </summary>
        public IComponentHost NewParent { get; set; }
    }
}
