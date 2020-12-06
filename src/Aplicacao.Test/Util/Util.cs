using Nancy.Json;
using System;
using System.Text;

namespace Aplication.Util
{
    public static class Util
    {
        public static string GetPath(this Array source)
        {
            if (source.Length == 0) return null;

            StringBuilder sb = new StringBuilder();

            foreach (var param in source)
            {
                sb.Append(param);
                sb.Append("/");
            }

            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public static string GetPath(this string source)
        {
            if (source.Length == 0) return null;

            StringBuilder sb = new StringBuilder();

            if (source.Length > 0)
            {
                sb.Append(source);
                sb.Append("/");
            }

            return sb.ToString();
        }

        public static string[] AsParameters(this string source)
        {
            return !string.IsNullOrEmpty(source) ? source.Split(',') : Array.Empty<string>();
        }
    }

    public static class JsonHelper
    {
        public static string ToJSON(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
