using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Timelogger.Api.Controllers;
using Timelogger.Api.Repository;
using Timelogger.Api.Services;
using Timelogger.Entities;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private static readonly TimeRegistration mockTimeReg1 = new TimeRegistration
        {
            TimeRegistrationId = 1,
            Title = "Test Title",
            Description = "Test Description",
            TimeSpent = 35
        };

        private readonly Project mockProject1 = new Project
        {
            Id = 1,
            Name = "Test",
            Deadline = DateTime.Today.AddMonths(2),
            Complete = false,
            TimeRegistrations = new List<TimeRegistration>
            {
                mockTimeReg1
            }
        };

        private readonly Project mockProject2 = new Project
        {
            Id = 2,
            Name = "Test",
            Deadline = DateTime.Today.AddMonths(4),
            Complete = true,
            TimeRegistrations = new List<TimeRegistration>()
        };

        [Test]
        public void HelloWorld_ShouldReply_HelloBack()
        {
            ProjectsController sut = new ProjectsController(null);

            string actual = sut.HelloWorld();

            Assert.AreEqual("Hello Back!", actual);
        }

        [Test]
        public void Get_ShouldGive_OkResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Project> { mockProject1, mockProject2 });
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.Get();
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Get_ShouldGive_NotFoundResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Project>());
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.Get();
            var notFoundResult = result as NotFoundObjectResult;

            // assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void GetById_ShouldGive_OkResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(mockProject1);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);
            int id = 1;

            // act
            var result = sut.GetById(id);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetById_ShouldGive_NotFoundResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns((Project)null);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.GetById(1);
            var notFoundResult = result as NotFoundObjectResult;

            // assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void GetOrderedByDeadline_ShouldGive_OkResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Project> { mockProject1, mockProject2 });
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.GetOrderedByDeadline();
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetOrderedByDeadline_ShouldGive_NotFoundResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Project>());
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.GetOrderedByDeadline();
            var notFoundResult = result as NotFoundObjectResult;

            // assert
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Add_ShouldGive_OkResult()
        {
            // arrange
            Project project = new Project
            {
                Id = 3,
                Name = "Test",
                Deadline = DateTime.Now.AddMonths(2),
                Complete = false,
                TimeRegistrations = new List<TimeRegistration>()
            };
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.Add(project))
                .Returns(project);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.Add(project);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Add_DeadlineInThePast_ShouldGive_BadRequestResult()
        {
            // arrange
            Project project = new Project
            {
                Id = 3,
                Name = "Test",
                Deadline = DateTime.Now.AddMonths(-4),
                Complete = false,
                TimeRegistrations = new List<TimeRegistration>()
            };
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.Add(project);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Deadline has to be in the future", badRequestResult.Value);
        }

        [Test]
        public void Add_ProjectAlreadyExists_ShouldGive_BadRequestResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.Add(mockProject1))
                .Returns((Project)null);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.Add(mockProject1);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Project already exists in Database", badRequestResult.Value);
        }

        [Test]
        public void RegisterTime_ShouldGive_OkResult()
        {
            // arrange
            TimeRegistration timeRegistration = new TimeRegistration
            {
                TimeRegistrationId = 2,
                Title = "Test Title",
                Description = "Test Description",
                TimeSpent = 35
            };
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.RegisterTime(mockProject1, timeRegistration))
                .Returns(mockProject1);
            projectRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(mockProject1);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);


            // act
            var result = sut.RegisterTime(1, timeRegistration);
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void RegisterTime_TimeTooLow_ShouldGive_BadRequestResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);
            TimeRegistration timeRegistration = new TimeRegistration
            {
                TimeRegistrationId = 3,
                Title = "Test Title",
                Description = "Test Description",
                TimeSpent = 20
            };

            // act
            var result = sut.RegisterTime(1, timeRegistration);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Individual time registrations should be 30 minutes or longer", badRequestResult.Value);
        }

        [Test]
        public void RegisterTime_ProjectComplete_ShouldGive_BadRequestResult()
        {
            // arrange
            TimeRegistration timeRegistration = new TimeRegistration
            {
                TimeRegistrationId = 2,
                Title = "Test Title",
                Description = "Test Description",
                TimeSpent = 35
            };
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.RegisterTime(mockProject2, timeRegistration))
                .Returns(mockProject2);
            projectRepositoryMock
                .Setup(x => x.GetById(2))
                .Returns(mockProject2);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.RegisterTime(2, timeRegistration);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("You can not register time to a complete project", badRequestResult.Value);
        }

        [Test]
        public void RegisterTime_TimeRegistrationExists_ShouldGive_BadRequestResult()
        {
            // arrange
            var projectRepositoryMock = new Mock<IProjectsRepository>();
            projectRepositoryMock
                .Setup(x => x.RegisterTime(mockProject1, mockTimeReg1))
                .Returns(mockProject1);
            projectRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(mockProject1);
            var projectService = new ProjectsService(projectRepositoryMock.Object);
            ProjectsController sut = new ProjectsController(projectService);

            // act
            var result = sut.RegisterTime(1, mockTimeReg1);
            var badRequestResult = result as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual($"Time Registration already exists in Project with ID {mockProject1.Id}", badRequestResult.Value);
        }
    }
}
