using System.Threading.Tasks;

namespace InstantEatService.Services
{
    public interface IClientService
    {
        Task<bool> PatchClientName(string phoneNumber, string name);
        Task<bool> PostClient(string phoneNumber);
    }
}