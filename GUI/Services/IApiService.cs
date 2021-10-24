using System.Threading.Tasks;

namespace GUI.Services
{
    public interface IApiService
    {
        public Task<string> GetTest();
    }
}