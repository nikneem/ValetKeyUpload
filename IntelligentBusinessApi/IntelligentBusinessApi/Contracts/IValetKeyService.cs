using System;
using System.Threading.Tasks;
using IntelligentBusinessApi.DataTransferObjects;

namespace IntelligentBusinessApi.Contracts
{
    public interface IValetKeyService
    {
        Task<ValetKeyDto> RegisterValetKey(string blobName);
    }
}