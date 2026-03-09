using zTools.DTO.InVideo;
using System.Threading.Tasks;

namespace zTools.Domain.Services.Interfaces
{
    public interface IInVideoService
    {
        Task<InVideoResponse> GenerateVideoAsync(string prompt);
        Task<InVideoResponse> GenerateVideoAsync(InVideoRequest request);
    }
}
