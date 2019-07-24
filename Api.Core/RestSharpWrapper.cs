using RestSharp;

namespace Api.Core {
    public class RestSharpWrapper : IRestSharpWrapper {
        public IRestRequest CreateRestRequest(string path) {
            return new RestRequest(path);
        }

        public IRestClient CreateRestClient(string url) {
            return new RestClient(url);
        }
    }
}