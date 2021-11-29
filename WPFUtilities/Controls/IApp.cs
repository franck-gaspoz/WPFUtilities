namespace WPFUtilities.Controls
{
    public interface IApp
    {
        bool IsBuzy { get; set; }

        void NotifyWindowDisplayed(object sender);
    }
}
