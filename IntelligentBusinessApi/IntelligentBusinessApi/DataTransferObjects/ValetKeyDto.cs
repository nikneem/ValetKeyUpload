using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentBusinessApi.DataTransferObjects
{
    public sealed class ValetKeyDto

    {

        public string BlobName { get; set; }
        public string Credentials { get; set; }
        public Uri BlobUri { get; set; }

    }
}
