using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Tests.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.Controllers
{
    public class PersonControllerTests
    {
        private ILogger<PersonController> fakeLogger;
        private IPersonService fakePersonService;
        private IMapper fakeMapper;
        private PersonController personController;
        private PersonServicesTest personServicesTest;
        private HttpClient httpClient;

        public PersonControllerTests()
        {
            if (personServicesTest == null)
                personServicesTest = new PersonServicesTest();

            httpClient = new HttpClient();

            this.fakeLogger = A.Fake<ILogger<PersonController>>();
            this.fakePersonService = A.Fake<IPersonService>();
            this.fakeMapper = A.Fake<IMapper>();
            personController = this.CreatePersonController();
            personController.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();


        }

        private PersonController CreatePersonController()
        {
            return new PersonController(
                this.fakeLogger,
                this.fakePersonService,
                this.fakeMapper);
        }

        [Fact]
        public void GetById_StateUnderTest_ExpectedBehaviorAsync()
        {
            // Arrange
            using var application = new WebApplicationFactory<UKParliament.CodeTest.Web.Program>();
            using var client = application.CreateClient();

            var response = client.GetAsync("/api/person/1").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void GetByList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            using var application = new WebApplicationFactory<UKParliament.CodeTest.Web.Program>();
            using var client = application.CreateClient();

            var response = client.GetAsync("/api/person").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void AddPerson_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            using var application = new WebApplicationFactory<UKParliament.CodeTest.Web.Program>();
            using var client = application.CreateClient();

            PersonViewModel model = new PersonViewModel();
            model.Address = "Test";
            model.City = "Test";
            model.Email = "Test";
            model.Phone = "Test";
            model.Name = "Test for Job";
            model.PostCode = "Test";

            var response = client.PostAsJsonAsync($"/api/person/create/", model).Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public void UpdatePerson_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            using var application = new WebApplicationFactory<UKParliament.CodeTest.Web.Program>();
            using var client = application.CreateClient();

            var person = client.GetFromJsonAsync<PersonViewModel>("/api/person/2").Result;

            if (person != null)
            {
                PersonViewModel model = new PersonViewModel();
                model.Address = person.Address;
                model.City = person.City;
                model.Email = person.Email;
                model.Phone = person.Phone;
                model.Name = "Test for Job";
                model.PostCode = person.PostCode;

                var response = client.PutAsJsonAsync($"/api/person/update/{person.Id}", model).Result;

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                // Assert
                Assert.True(response.StatusCode == HttpStatusCode.OK);
            }
        }

        [Fact]
        public void DeletePerson_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            using var application = new WebApplicationFactory<UKParliament.CodeTest.Web.Program>();
            using var client = application.CreateClient();

            var response = client.DeleteAsync("/api/person/delete/1").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
