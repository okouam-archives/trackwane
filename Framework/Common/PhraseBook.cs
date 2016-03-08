namespace Trackwane.Framework.Common
{
    public static class PhraseBook
    {
        public static string Generate(string msg, params object[] values)
        {
            return string.Format(msg, values);
        }
    }
}
