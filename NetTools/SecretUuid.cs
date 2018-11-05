namespace NetTools
{
    public static class SecretUuid
    {
        static SecretUuid()
        {
            GuidBytes = new byte[16] { 0xB2, 0xC1, 0x34, 0x7C, 0xD6, 0x6F, 0x44, 0xA4, 0xB8, 0x11, 0x75, 0x7D, 0x9E, 0x53, 0x98, 0x20 };
            string GuidString = HashTools.GetString(GuidBytes);
            GuidString = GuidString.Substring(0, 8) + "-" + GuidString.Substring(8, 4) + "-" + GuidString.Substring(12, 4) + "-" + GuidString.Substring(16, 4) + "-" + GuidString.Substring(20, 12);
            Guid = new System.Guid("{" + GuidString + "}");
        }

        public static byte[] GuidBytes { get; private set; }
        public static System.Guid Guid { get; private set; }
    }
}
