namespace NetTools
{
    public static class HashTools
    {
        public static bool TryParse(string hashString, out byte[] hash)
        {
            if (hashString == null || hashString.Length < 2)
            {
                hash = null;
                return false;
            }

            hash = new byte[hashString.Length / 2];

            for (int i = 0; i < hash.Length; i++)
            {
                byte bh, bl;
                if (!TryParseHex(hashString[(i * 2) + 0], out bh) || !TryParseHex(hashString[(i * 2) + 1], out bl))
                    return false;

                hash[i] = (byte)(bh * 16 + bl);
            }

            return true;
        }

        public static bool TryParseHex(char c, out byte b)
        {
            if (c >= '0' && c <= '9')
            {
                b = (byte)(c - '0');
                return true;
            }

            else if (c >= 'a' && c <= 'f')
            {
                b = (byte)(c - 'a' + 10);
                return true;
            }

            else if (c >= 'A' && c <= 'F')
            {
                b = (byte)(c - 'A' + 10);
                return true;
            }

            else
            {
                b = 0;
                return false;
            }
        }

        public static string GetString(byte[] hash)
        {
            string HashString = "";

            if (hash != null)
                for (int i = 0; i < hash.Length; i++)
                    HashString += hash[i].ToString("X2");

            return HashString;
        }
    }
}
