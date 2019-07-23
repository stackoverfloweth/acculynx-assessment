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
    public class StackExchangeFilterCreatorUnitTests : UnitTestBase {
        [Fact]
        public void CreateFilter_Always_CallsIStackExchangeResourceFactoryFetchResourceOnce() {
            // arrange
            var resourceFactoryMock = AutoFixture.Freeze<Mock<IStackExchangeResourceFactory>>();

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            filterCreator.CreateFilter();

            // assert
            resourceFactoryMock.Verify(x=>x.FetchResource(StackExchangeResourceEnum.CreateFilter), Times.Once);
        }

        [Fact]
        public void CreateFilter_Always_CallsIRestSharpWrapperCreateRestRequestOnce() {
            // arrange
            var path = AutoFixture.Create<string>();
            AutoFixture.Freeze<Mock<IStackExchangeResourceFactory>>()
                .Setup(x => x.FetchResource(It.IsAny<StackExchangeResourceEnum>()))
                .Returns(path);

            var restSharpWrapperMock = AutoFixture.Freeze<Mock<IRestSharpWrapper>>();

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            filterCreator.CreateFilter();

            // assert
            restSharpWrapperMock.Verify(x => x.CreateRestRequest(path), Times.Once);
        }

        [Fact]
        public void CreateFilter_Always_CallsIRestRequestAddParameterOnce() {
            // arrange
            var request = new Mock<IRestRequest>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestRequest(It.IsAny<string>()))
                .Returns(request.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            filterCreator.CreateFilter();

            // assert
            request.Verify(x=>x.AddParameter("include", "question.accepted_answer_id;question.body;answer.body"), Times.Once);
        }

        [Fact]
        public void CreateFilter_Always_CallsIRestSharpWrapperCreateRestClientOnce() {
            // arrange
            var restSharpWrapperMock = AutoFixture.Freeze<Mock<IRestSharpWrapper>>();

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            filterCreator.CreateFilter();

            // assert
            restSharpWrapperMock.Verify(x => x.CreateRestClient("https://api.stackexchange.com/2.2"), Times.Once);
        }

        [Fact]
        public void CreateFilter_Always_CallsIRestClientExecuteOnce() {
            // arrange
            var request = new Mock<IRestRequest>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestRequest(It.IsAny<string>()))
                .Returns(request.Object);

            var client = new Mock<IRestClient>();
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            filterCreator.CreateFilter();

            // assert
            client.Verify(x=>x.Execute<ItemResponseDto<FilterDto>>(request.Object), Times.Once);
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsNull_ReturnsNull() {
            // arrange
            var client = new Mock<IRestClient>();
            client
                .Setup(x => x.Execute<ItemResponseDto<FilterDto>>(It.IsAny<IRestRequest>()))
                .Returns((IRestResponse<ItemResponseDto<FilterDto>>)null);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            var response = filterCreator.CreateFilter();

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsResponseWithNullData_ReturnsNull() {
            // arrange
            var client = new Mock<IRestClient>();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<FilterDto>>>()
                .Without(x => x.Data)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<FilterDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            var response = filterCreator.CreateFilter();

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsResponseWithZeroQuotaRemaining_ReturnsNull() {
            // arrange
            var client = new Mock<IRestClient>();
            var data = AutoFixture.Build<ItemResponseDto<FilterDto>>()
                .With(x => x.QuotaRemaining, 0)
                .Create();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<FilterDto>>>()
                .With(x => x.Data, data)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<FilterDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            var response = filterCreator.CreateFilter();

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsResponseWithEmptyItems_ReturnsNull() {
            // arrange
            var client = new Mock<IRestClient>();
            var data = AutoFixture.Build<ItemResponseDto<FilterDto>>()
                .With(x => x.QuotaRemaining, RandomGenerator.Next(1, 100))
                .With(x=>x.Items, new List<FilterDto>())
                .Create();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<FilterDto>>>()
                .With(x => x.Data, data)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<FilterDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            var response = filterCreator.CreateFilter();

            // assert
            response.Should().BeNull();
        }

        [Fact]
        public void CreateFilter_WhenIRestClientExecuteReturnsValidResponse_ReturnsResponseData() {
            // arrange
            var client = new Mock<IRestClient>();
            var data = AutoFixture.Build<ItemResponseDto<FilterDto>>()
                .With(x => x.QuotaRemaining, RandomGenerator.Next(1, 100))
                .Create();
            var restResponse = AutoFixture.Build<RestResponse<ItemResponseDto<FilterDto>>>()
                .With(x => x.Data, data)
                .Create();
            client
                .Setup(x => x.Execute<ItemResponseDto<FilterDto>>(It.IsAny<IRestRequest>()))
                .Returns(restResponse);
            AutoFixture.Freeze<Mock<IRestSharpWrapper>>()
                .Setup(x => x.CreateRestClient(It.IsAny<string>()))
                .Returns(client.Object);

            // act
            var filterCreator = AutoFixture.Create<StackExchangeFilterCreator>();
            var response = filterCreator.CreateFilter();

            // assert
            response.Should().BeEquivalentTo(data.Items.FirstOrDefault()?.Filter);
        }
    }
}
