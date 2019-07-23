using RestSharp;

namespace Api.Core {
    public interface IRestSharpWrapper {
        IRestClient CreateRestClient(string url);
        IRestRequest CreateRestRequest(string path);
    }
}