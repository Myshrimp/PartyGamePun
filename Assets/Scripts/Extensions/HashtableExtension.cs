using ExitGames.Client.Photon;

namespace ShrimpFPS.Extensions
{
    public static class HashtableExtension
    {
        public static void TryAddInt(this Hashtable hash, object key, int value)
        {
            if (hash.ContainsKey(key))
            {
                hash[key] = (int)hash[key] + value;
            }
            else hash[key] = value;
        }
        public static void TryAddFloat(this Hashtable hash, object key,float value)
        {
            if (hash.ContainsKey(key))
            {
                hash[key] = (float)hash[key] + value;
            }
            else hash[key] = value;
        }

        public static object TryGetValue(this Hashtable hash, object key)
        {
            if (hash.ContainsKey(key))
            {
                return hash[key];
            }

            return null;
        }
    }
}