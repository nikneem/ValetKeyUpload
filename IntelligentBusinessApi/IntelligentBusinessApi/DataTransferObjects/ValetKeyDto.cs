
namespace IntelligentBusinessApi.DataTransferObjects
{
    public sealed class ValetKeyDto

    {

        public string StorageUri { get; set; }
        public string     StorageAccessToken { get; set; }
        public string     Container { get; set; }
        public string Filename { get; set; }

    }
}
