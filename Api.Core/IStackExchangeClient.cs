using Api.Contract;

namespace Api.Core {
    public interface IStackExchangeClient {
        QuestionResponseDto Questions(int page);
    }
}
