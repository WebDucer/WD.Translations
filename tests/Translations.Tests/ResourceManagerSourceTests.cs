using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Translations.Tests.Mocks;
using Translations.Tests.TestResources;
using WD.Translations;

namespace Translations.Tests
{
    [TestFixture]
    public class ResourceManagerSourceTests
    {
        [SetUp]
        public void Init()
        {
            TestResourceManagersSource.Reset();
        }

        [TearDown]
        public void Cleanup()
        {
            TestResourceManagersSource.Reset();
        }

        [Test]
        public void Init_CalledTwice_Throws()
        {
            // Arrange
            var sut = ResourceManagersSource.Init(true, TestTranslations.ResourceManager);
            var sutAction = new Action(() => ResourceManagersSource.Init(true, TestTranslations.ResourceManager));

            // Act // Assert
            sutAction.Should().ThrowExactly<NotSupportedException>();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Init_CalledTwice_ThrowDeactivated_NotThrows(bool firstCall)
        {
            // Arrange
            var sut = ResourceManagersSource.Init(firstCall, TestTranslations.ResourceManager);
            var sutAction = new Action(() => ResourceManagersSource.Init(false, TestTranslations.ResourceManager));

            // Act // Assert
            sutAction.Should().NotThrow();
        }

        [Test]
        public void Init_WithResourceManager()
        {
            // Arrange / Act
            var sut = ResourceManagersSource.Init(true, TestTranslations.ResourceManager);

            // Assert
            sut.ResourceManagers.Length.Should().Be(1);
            sut.ResourceManagers[0].Should().Be(TestTranslations.ResourceManager);
        }

        [Test]
        public void Init_WithResourceIdAndAssembly()
        {
            // Arrange / Act
            var sut = ResourceManagersSource.Init(typeof(TestTranslations).FullName, typeof(TestTranslations).Assembly);

            // Assert
            sut.ResourceManagers.Length.Should().Be(1);
            sut.ResourceManagers[0].BaseName.Should().Be(typeof(TestTranslations).FullName);
        }

        [Test]
        public void Init_WithDictionary()
        {
            // Arrange / Act
            var sut = ResourceManagersSource.Init(new Dictionary<string, Assembly>(){{ typeof(TestTranslations).FullName, typeof(TestTranslations).Assembly}});

            // Assert
            sut.ResourceManagers.Length.Should().Be(1);
            sut.ResourceManagers[0].BaseName.Should().Be(typeof(TestTranslations).FullName);
        }
    }
}