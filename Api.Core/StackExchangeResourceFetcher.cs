using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                    return "questions";
                default:
                    throw new ArgumentOutOfRangeException(nameof(stackExchangeResourceEnum), stackExchangeResourceEnum, null);
            }
        }
    }
}
