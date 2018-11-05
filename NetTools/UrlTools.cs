#if USE_RESTRICTED_FEATURES
using CSHTML5;
#endif
using System.Collections.Generic;

namespace NetTools
{
    public class UrlTools
    {
        public static bool IsUsingRestrictedFeatures
        {
            get
            {
#if USE_RESTRICTED_FEATURES
                return true;
#else
                return false;
#endif
            }
        }

        public static object GetDocumentUrl()
        {
#if USE_RESTRICTED_FEATURES
            return Interop.ExecuteJavaScript("document.URL");
#else
            return null;
#endif
        }

        public static string GetBaseUrl()
        {
            object Url = GetDocumentUrl();
            if (Url == null)
                return "";

            string UrlAsString = Url.ToString();
            int EndIndex = UrlAsString.IndexOf('?');
            if (EndIndex >= 0)
                UrlAsString = UrlAsString.Substring(0, EndIndex);

            while (UrlAsString.EndsWith("/"))
                UrlAsString = UrlAsString.Substring(0, UrlAsString.Length - 1);

            return UrlAsString;
        }

        public static IDictionary<string, string> GetQueryString()
        {
            IDictionary<string, string> Result = new Dictionary<string, string>();

            object Url = GetDocumentUrl();
            if (Url != null)
            {
                string UrlAsString = Url.ToString();

                int StartIndex;
                if ((StartIndex = UrlAsString.IndexOf('?')) >= 0)
                {
                    string QueryString = UrlAsString.Substring(StartIndex + 1);
                    foreach (string KeyValuePair in QueryString.Split('&'))
                    {
                        string Key, Value;

                        string[] Splitted = KeyValuePair.Split('=');
                        if (Splitted.Length >= 2)
                        {
                            Key = Splitted[0];
                            Value = Splitted[1];
                            for (int i = 2; i < Splitted.Length; i++)
                                Value += "=" + Splitted[i];
                        }
                        else
                        {
                            Key = KeyValuePair;
                            Value = null;
                        }

                        Result.Add(Key, Value);
                    }
                }
            }

            return Result;
        }
    }
}
