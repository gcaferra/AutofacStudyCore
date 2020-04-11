using System.Net.Http;
using System.Threading.Tasks;

namespace AutofacStudyCore.Services
{
    public interface ITestService1
    {
        Task NotWorkingMethod();
    }

    public interface ITestService2
    {
        Task WorkingMethod();
    }

    public class TestService : ITestService1, ITestService2
    {
        private readonly IHttpClientFactory _factory;

        public TestService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task NotWorkingMethod()
        {
            var client = _factory.CreateClient();
            
            var result = await client.GetAsync("https://www.google.com/");
            result.EnsureSuccessStatusCode();
        }

        public async Task WorkingMethod()
        {
            var client = _factory.CreateClient("named");
            
            var result = await client.GetAsync("https://www.google.com/");
            result.EnsureSuccessStatusCode();        
        }
    }
}