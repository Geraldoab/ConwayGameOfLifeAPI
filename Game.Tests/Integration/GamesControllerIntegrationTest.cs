using ConwayGameOfLife;
using FluentAssertions;
using Game.Application.Contracts;
using Game.Application.Contracts.Core;
using Game.Infra.Data.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Game.Tests.Integration
{
    public class GamesControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GamesControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/boards/00000000-0000-0000-0000-000000000000")]
        public async Task Get_Board_returning_bad_request(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/boards/11513fa0-66b0-4cf9-99e7-77a0dd402a02")]
        [InlineData("/boards/941356a9-3cfe-4c47-9c98-53b63f62e238")]
        [InlineData("/boards/c3086c32-d897-40c6-8f70-ce1f737fc2eb")]
        [InlineData("/boards/7dcdf057-c9fb-438e-be06-69aab1cffd1f")]
        [InlineData("/boards/3c2be2c6-7575-4531-95ca-254c9f3bc054")]
        public async Task Get_Board_returning_not_found(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Get_Board_successfully")]
        public async Task Get_Board_successfully()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Get Next Board
            var customResult = await GenerateNextBoard(client);

            // Assert
            customResult.Should().NotBeNull();

            if (customResult != null)
            {
                var board = JsonConvert.DeserializeObject<BoardResponse?>(Convert.ToString(customResult.Data));
                
                // Act
                var response = await client.GetAsync($"/boards/{board.Grid.Id}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact(DisplayName = "Post_Board_with_invalid_payload")]
        public async Task Post_Board_with_invalid_payload()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newBoard = new BoardStatePostRequest(new BoardRequest()
            {
                Grid = new GridRequest()
                {
                    Width = 10,
                    Height = 9
                }
            });

            // Act
            var response = await client.PostAsync("/boards", 
                new StringContent(JsonConvert.SerializeObject(newBoard), 
                Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Post_Board_successfully")]
        public async Task Post_Board_successfully()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            int[,] array2D = new int[10, 10];
            var random = new Random();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    array2D[i, j] = random.Next(2);

            var newBoard = new BoardStatePostRequest(new BoardRequest()
            {
                Grid = new GridRequest()
                {
                    Width = 10,
                    Height = 10,
                    Cells = array2D
                }
            });

            // Act
            var response = await client.PostAsync("/boards", 
                new StringContent(JsonConvert.SerializeObject(newBoard),
                Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            var responseJson = await response.Content.ReadAsStringAsync();
            var customResult = JsonConvert.DeserializeObject<CustomResult?>(responseJson);
            if(customResult != null)
            {
                Guid boardGuid;
                Guid.TryParse(Convert.ToString(customResult.Data), out boardGuid);

                // Assert
                boardGuid.Should().NotBeEmpty();
            }

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData("/boards/00000000-0000-0000-0000-000000000000")]
        public async Task Delete_Board_returning_bad_request(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/boards/11513fa0-66b0-4cf9-99e7-77a0dd402a02")]
        [InlineData("/boards/941356a9-3cfe-4c47-9c98-53b63f62e238")]
        [InlineData("/boards/c3086c32-d897-40c6-8f70-ce1f737fc2eb")]
        [InlineData("/boards/7dcdf057-c9fb-438e-be06-69aab1cffd1f")]
        [InlineData("/boards/3c2be2c6-7575-4531-95ca-254c9f3bc054")]
        public async Task Delete_Board_returning_not_found(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact(DisplayName = "Delete_Board_successfully")]
        public async Task Delete_Board_successfully()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Get Next Board
            var customResult = await GenerateNextBoard(client);

            // Assert
            customResult.Should().NotBeNull();

            if (customResult != null)
            {
                var board = JsonConvert.DeserializeObject<BoardResponse?>(Convert.ToString(customResult.Data));

                // Act
                var response = await client.DeleteAsync($"/boards/{board.Grid.Id}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact(DisplayName = "Get_NextBoardState_successfully")]
        public async Task Get_NextBoardState_successfully()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/boards/states-next");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/boards/states-away/-1")]
        [InlineData("/boards/states-away/0")]
        [InlineData("/boards/states-away/101")]
        public async Task Get_BoardStatesAway_with_invalid_number_of_iterations(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/boards/states-away/1")]
        [InlineData("/boards/states-away/10")]
        [InlineData("/boards/states-away/100")]
        public async Task Get_BoardStatesAway_successfully(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/boards/states-final/-1")]
        [InlineData("/boards/states-final/0")]
        [InlineData("/boards/states-final/101")]
        public async Task Get_BoardStatesFinal_with_invalid_number_of_iterations(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<CustomResult?> GenerateNextBoard(HttpClient client)
        {
            var nextBoardResponse = await client.GetAsync("/boards/states-next");
            var responseJson = await nextBoardResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomResult?>(responseJson);
        }
    }
}
