using Api.Contract.Enums;
using System;

namespace Api.Core {
    public class StackExchangeResourceFetcher : IStackExchangeResourceFetcher {
        public string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum, object argument)
        {
            switch (stackExchangeResourceEnum)
            {
                case StackExchangeResourceEnum.GetQuestions:
                    return $"questions";
                case StackExchangeResourceEnum.LookupQuestions:
                    return $"questions/{argument}";
                case StackExchangeResourceEnum.GetQuestionAnswers:
                    return $"questions/{argument}/answers";
                case StackExchangeResourceEnum.CreateFilter:
                    return "filters/create";
                default:
                    throw new ArgumentOutOfRangeException(nameof(stackExchangeResourceEnum), stackExchangeResourceEnum, null);
            }
        }

        public string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum) {
            return FetchResource(stackExchangeResourceEnum, null);
        }
    }
}
