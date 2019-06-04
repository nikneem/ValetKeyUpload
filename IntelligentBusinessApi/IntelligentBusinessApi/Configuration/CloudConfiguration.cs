using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentBusinessApi.Configuration
{
    public sealed class CloudConfiguration
    {
        public const string SettingName = "Cloud";

        public string StorageConnectionString { get; set; }

    }
}
