namespace ViewModels
{
    public static class Properties
    {
        public static bool FirstLaunch
        {
            get { return Settings.Default.FirstLaunch; }
            set
            {
                Settings.Default.FirstLaunch = value;
                Settings.Default.Save();
            }
        }

        public static int ShrinkagePercent
        {
            get { return Settings.Default.ShrinkagePercent; }
            set
            {
                Settings.Default.ShrinkagePercent = value;
                Settings.Default.Save();
            }
        }
    }
}
