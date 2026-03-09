using zTools.DTO.InVideo;
using System.Threading.Tasks;

namespace zTools.ACL.Interfaces
{
    public interface IInVideoClient
    {
        Task<InVideoResponse> GenerateVideoAsync(string prompt);
        Task<InVideoResponse> GenerateVideoAsync(InVideoRequest request);
    }
}
