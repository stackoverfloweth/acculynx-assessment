using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeResourceFetcher : IStackExchangeResourceFetcher {
        public string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum)
        {
            switch (stackExchangeResourceEnum)
            {
                case StackExchangeResourceEnum.Question:
                    return "questions?site=stackoverflow&pagesize=100";
                default:
                    throw new ArgumentOutOfRangeException(nameof(stackExchangeResourceEnum), stackExchangeResourceEnum, null);
            }
        }
    }
}
