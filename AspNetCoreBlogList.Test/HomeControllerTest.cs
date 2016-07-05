using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreBlogList.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Error_ReturnsErrorView()
        {
            // Arrange
            var controller = new HomeController();
            var errorView = "~/Views/Shared/Error.cshtml";

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal(errorView, viewResult.ViewName);
        }
    }
}