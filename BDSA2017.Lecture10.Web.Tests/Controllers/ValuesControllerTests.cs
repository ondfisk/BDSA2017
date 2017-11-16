using BDSA2017.Lecture10.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BDSA2017.Lecture10.Web.Tests.Controllers
{
    public class ValuesControllerTests
    {
        [Fact(DisplayName = "Controller has AllowAnonymousAttribute")]
        public void Controller_has_AllowAnonymousAttribute()
        {
            var type = typeof(ValuesController);

            var authorizeAttribute = type.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(AllowAnonymousAttribute));

            Assert.NotNull(authorizeAttribute);
        }

        // This is not needed but illustrates that [AllowAnonymous] can be added to either the controller or the method
        [Fact(DisplayName = "Get has AllowAnonymousAttribute")]
        public void Get_has_AllowAnonymousAttribute()
        {
            var type = typeof(ValuesController);

            var authorizeAttribute = type.GetMethod(nameof(ValuesController.Get)).CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(AllowAnonymousAttribute));

            Assert.NotNull(authorizeAttribute);
        }
    }
}
