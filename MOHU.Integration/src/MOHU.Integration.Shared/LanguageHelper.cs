namespace MOHU.Integration.Shared
{
    public class LanguageHelper
    {
        public static bool IsArabic { get { return Thread.CurrentThread.CurrentCulture.Name.Contains("ar"); } }
    }
}
