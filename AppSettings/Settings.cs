using System;

namespace AppSettings
{
    public sealed  class Settings
    {
        public sealed class ConnectionStrings
        {
            public static string DbHost { get; set; }
            public static string DbName { get; set; }
        }
    }
}
