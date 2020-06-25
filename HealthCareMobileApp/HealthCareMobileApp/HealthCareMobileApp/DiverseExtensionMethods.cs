
namespace HealthCareMobileApp
{
    public static class DiverseExtensionMethods
    {
        public static bool IsAddress(this string chars) => 
            (!string.IsNullOrEmpty(chars) && chars.Length == 42 && chars.IsHex());
        public static bool IsHex(this string chars)
        {
            bool isHex;
            foreach (var c in chars)
            {
                isHex = ((c >= '0' && c <= '9') ||
                         (c >= 'a' && c <= 'f') ||
                         (c >= 'A' && c <= 'F')|| c=='x');

                if (!isHex)
                    return false;
            }
            return true;
        }
        public static bool IsZero(this string str)
        {
            return str.ToLower() == "0x0000000000000000000000000000000000000000";
        }

        public static float GweiToEther(this ulong gwei)
        {
            return (float)gwei / 1000000000;
        }
    }
}
