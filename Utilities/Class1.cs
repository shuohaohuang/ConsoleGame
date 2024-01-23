namespace Utilities
{
    public class Utility
    {
        public static string NameMayus(string name)
        {
            name = name.ToLower();
            char[] chars = name.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            name = new string(chars);
            return name;
        }
    }
}
