using Api.Contract.Enums;
using System;
using System.Collections.Generic;

namespace Api.Core {
    public class StackExchangeResourceFactory : IStackExchangeResourceFactory {
        public string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters) {
            switch (stackExchangeResourceEnum) {
                case StackExchangeResourceEnum.GetQuestions:
                    return "questions";
                case StackExchangeResourceEnum.LookupQuestions:
                    return $"questions/{parameters[0]}";
                case StackExchangeResourceEnum.GetQuestionAnswers:
                    return $"questions/{parameters[0]}/answers";
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