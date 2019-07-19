using Api.Contract;
using Api.Core;
using RestSharp;

public class StackExchangeClient : IStackExchangeClient {
    private readonly RestClient _restClient;

    public StackExchangeClient() {
        _restClient = new RestClient("https://api.stackexchange.com/2.2");
    }

    public QuestionResponseDto Questions(int page) {
        //"https://api.stackexchange.com/2.2/questions?fromdate=1563408000&todate=1563494400&order=desc&sort=creation&site=stackoverflow"
        var client = new RestClient("https://api.stackexchange.com/2.2");
        var request = new RestRequest($"questions?site=stackoverflow&pagesize=100&page={page}");

        return client.Execute<QuestionResponseDto>(request).Data;
    }
}