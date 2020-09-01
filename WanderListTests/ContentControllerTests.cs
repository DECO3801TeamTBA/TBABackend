using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WanderListAPI.Controllers;
using WanderListAPI.Data;
using WanderListAPI.Models;
using Xunit;

namespace WanderListTests
{
    /// <summary>
    /// Class for holding all Tests related to ContentController, maybe we could import seeddata and
    /// test it with that? Or just generate it on the fly like below?
    /// Might be able to extract some common operations to methods?
    /// </summary>
    public class ContentControllerTests
    {
        [Fact]
        public async Task Content_GET_WithDbContext_Async()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WanderListDbContext>()
            .UseInMemoryDatabase(databaseName: "WanderList")
            .Options;

            var logger = new Mock<ILogger<Content>>();

            using var context = new WanderListDbContext(options);

            var content1 = new Content()
            {
                ContentId = Guid.NewGuid(),
                Address = "testing1",
                Name = "tryme1"
            };

            var content2 = new Content()
            {
                ContentId = Guid.NewGuid(),
                Address = "testing2",
                Name = "tryme2"
            };

            var content3 = new Content()
            {
                ContentId = Guid.NewGuid(),
                Address = "testing3",
                Name = "tryme3"
            };

            context.Add(content1);
            context.Add(content2);
            context.Add(content3);
            context.SaveChanges();

            var controller = new ContentController(context, logger.Object);

            //Act
            var result = await controller.Get();

            //Assert
            // Verifying that ILogger is working is a bit of a rabbit hole.
            // For now it really isn't of interest... but it is technically possible
            var contentResult = Assert.IsAssignableFrom<IEnumerable<Content>>(result);

            Assert.NotNull(contentResult);

            Assert.Contains(content1, contentResult);
            Assert.Contains(content2, contentResult);
            Assert.Contains(content3, contentResult);
        }
    }
}