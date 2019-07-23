﻿using RestSharp;

namespace Api.Core {
    public interface IStackExchangeFilterCreator {
        string CreateFilter(IRestClient restClient);
    }
}