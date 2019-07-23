using System.Collections.Generic;
using System.Linq;
using Api.Contract;
using Api.Contract.Enums;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using RestSharp;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class StackExchangeRequestHandlerUnitTests : UnitTestBase {
        [Fact]
        public void Execute_Always_CallsIStackExchangeResourceFactoryFetchResourceOnce() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var resourceFetcherMock = AutoFixture.Freeze<Mock<IStackExchangeResourceFactory>>();

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            resourceFetcherMock.Verify(x=>x.FetchResource(resourceEnum, parameters), Times.Once);
        }

        [Fact]
        public void Execute_Always_CallsIRestSharpWrapperCreateRestClientOnce() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();
            
            var restSharpWrapperMock = AutoFixture.Freeze<Mock<IRestSharpWrapper>>();

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            restSharpWrapperMock.Verify(x => x.CreateRestClient("https://api.stackexchange.com/2.2"), Times.Once);
        }

        [Fact]
        public void Execute_Always_CallsIRestSharpWrapperCreateRestRequestOnce() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var path = AutoFixture.Create<string>();
            AutoFixture.Freeze<Mock<IStackExchangeResourceFactory>>()
                .Setup(x => x.FetchResource(It.IsAny<StackExchangeResourceEnum>(), It.IsAny<List<object>>()))
                .Returns(path);

            var restSharpWrapperMock = AutoFixture.Freeze<Mock<IRestSharpWrapper>>();

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            restSharpWrapperMock.Verify(x => x.CreateRestRequest(path), Times.Once);
        }

        [Fact]
        public void Execute_Always_CallsIStackExchangeFilterCreator() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var filterCreatorMock = AutoFixture.Freeze<Mock<IStackExchangeFilterCreator>>();

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            filterCreatorMock.Verify(x => x.CreateFilter(), Times.Once);
        }

        [Fact]
        public void Execute_Always_CallsIRestRequestAddParameterForFilter() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var filter = AutoFixture.Create<string>();
            AutoFixture.Freeze<Mock<IStackExchangeFilterCreator>>()
                .Setup(x => x.CreateFilter())
                .Returns(filter);

            var restRequestMock = new Mock<IRestRequest>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestRequest(It.IsAny<string>()))
                .Returns(restRequestMock.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            restRequestMock.Verify(x => x.AddParameter("filter", filter), Times.Once);
        }

        [Theory]
        [InlineData("key", "cXOea6bOwSD2EuIw7XPIlA((")]
        [InlineData("site", "stackoverflow")]
        public void Execute_Always_CallsIRestRequestAddParameter(string key, object value) {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var restRequestMock = new Mock<IRestRequest>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestRequest(It.IsAny<string>()))
                .Returns(restRequestMock.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            restRequestMock.Verify(x=>x.AddParameter(key, value));
        }

        [Fact]
        public void Execute_Always_CallsIRestRequestAddParameterOncePerDataPassedIn() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var restRequestMock = new Mock<IRestRequest>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestRequest(It.IsAny<string>()))
                .Returns(restRequestMock.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            foreach (var dataItem in data) {
                restRequestMock.Verify(x => x.AddParameter(dataItem.Key, dataItem.Value));
            }
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsNull_ReturnsNull() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var client = new Mock<IRestClient>();
            client
                .Setup(x => x.Execute<ItemResponseDto<QuestionDto>>(It.IsAny<IRestRequest>()))
                .Returns((IRestResponse<ItemResponseDto<QuestionDto>>)null);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            var response = requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsResponseWithNullData_ReturnsNull() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var client = new Mock<IRestClient>();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<QuestionDto>>>()
                .Without(x => x.Data)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<QuestionDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            var response = requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsResponseWithZeroQuotaRemaining_ReturnsNull() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var client = new Mock<IRestClient>();
            var responseData = AutoFixture.Build<ItemResponseDto<QuestionDto>>()
                .With(x => x.QuotaRemaining, 0)
                .Create();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<QuestionDto>>>()
                .With(x => x.Data, responseData)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<QuestionDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            var response = requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsValidResponse_ReturnsResponseData() {
            // arrange
            var resourceEnum = AutoFixture.Create<StackExchangeResourceEnum>();
            var parameters = AutoFixture.CreateMany<object>().ToList();
            var data = AutoFixture.Create<Dictionary<string, object>>();

            var client = new Mock<IRestClient>();
            var responseData = AutoFixture.Build<ItemResponseDto<QuestionDto>>()
                .With(x => x.QuotaRemaining, RandomGenerator.Next(1, 100))
                .Create();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<QuestionDto>>>()
                .With(x => x.Data, responseData)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<QuestionDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var requestHandler = AutoFixture.Create<StackExchangeRequestHandler>();
            var response = requestHandler.Execute<QuestionDto>(resourceEnum, parameters, data);

            // assert
            response.Should().BeEquivalentTo(responseData.Items);
        }
    }
}
