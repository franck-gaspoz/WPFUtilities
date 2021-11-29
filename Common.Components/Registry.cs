namespace Common.Components
{
    public class Registry
        : ModelBase
    {
        static Registry _instance = null;
        public static Registry Instance => _instance ?? (_instance = new Registry());

        protected Registry() { }


        string _a = "";
        public string A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
                NotifyPropertyChanged();
            }
        }

        string _b = "";
        public string B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
                NotifyPropertyChanged();
            }
        }
    }
}
