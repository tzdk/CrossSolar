using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTests
    {
        public AnalyticsControllerTests()
        {
            _panelController = new PanelController(_panelRepositoryMock.Object);
            _analyticsController = new AnalyticsController(_analyticsRepositoryMock.Object, _panelRepositoryMock.Object);
        }

        private readonly PanelController _panelController;
        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();
        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();

        [Fact]
        public async Task Get_ShouldGetAnalytics()
        {
            // Act
            var result = await _analyticsController.Get("1");

            // Assert
            Assert.NotNull(result);

            var OkResult = result as OkObjectResult;
            Assert.NotNull(OkResult);
            Assert.Equal(200, OkResult.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldInsertAnalytics()
        {
            var panel = new PanelModel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB2222"
            };
            var oneHourElectricity = new OneHourElectricityModel
            {
                KiloWatt = 100,
                PanelId = "1",
                DateTime = DateTime.UtcNow
            };
            // Act
            var result = await _analyticsController.Post("1",oneHourElectricity);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
        [Fact]
        public async Task DayResults_ShouldGetList()
        {
            // Act
            var result = await _analyticsController.DayResults("1");

            // Assert
            Assert.NotNull(result);

            var OkResult = result as OkObjectResult;
            Assert.NotNull(OkResult);
            Assert.Equal(200, OkResult.StatusCode);
        }
    }
}