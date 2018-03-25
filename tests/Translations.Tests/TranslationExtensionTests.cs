using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Translations.Tests.Mocks;
using Translations.Tests.TestResources;
using WD.Translations;
using Xamarin.Forms.Xaml;

namespace Translations.Tests
{
    [TestFixture]
    public class TranslationExtensionTests
    {
        [SetUp]
        public void Init()
        {
            ResourceManagersSource.Init(TestTranslations.ResourceManager);
        }

        [TearDown]
        public void Cleanup()
        {
            TestResourceManagersSource.Reset();
        }

        [TestCase("en", "Value 1")]
        [TestCase("de", "Wert 1")]
        [TestCase("ru", "Значение 1")]
        public void Translate_ForExistingKeyInCulture_ResturnsTranslation(string culture, string expected)
        {
            // Arrange
            const string translationKey = "Value_1";
            var cultureInfo = Plugin.Multilingual.CrossMultilingual.Current;
            cultureInfo.CurrentCultureInfo = new CultureInfo(culture);
            var serviceProvider = Mock.Of<IServiceProvider>();
            var sut = new TranslationExtension {TranslationKey = translationKey};

            // Act
            var result = sut.ProvideValue(serviceProvider);
            var result2 = ((IMarkupExtension) sut).ProvideValue(serviceProvider);

            // Assert
            result.Should().Be(expected);
            result2.Should().Be(expected);
        }

        [TestCase("de")]
        [TestCase("ru")]
        [TestCase("fr")]
        public void Translate_ForExistingKey_NotForTheCulture_ResurnsTranslationOfDefaultlanguage(string culture)
        {
            // Arrange
            const string translationKey = "Value_2";
            const string expected = "Value 2";
            var cultureInfo = Plugin.Multilingual.CrossMultilingual.Current;
            cultureInfo.CurrentCultureInfo = new CultureInfo(culture);
            var serviceProvider = Mock.Of<IServiceProvider>();
            var sut = new TranslationExtension {TranslationKey = translationKey};

            // Act
            var result = sut.ProvideValue(serviceProvider);
            var result2 = ((IMarkupExtension) sut).ProvideValue(serviceProvider);

            // Assert
            result.Should().Be(expected);
            result2.Should().Be(expected);
        }

        [Test]
        public void Translate_ForNotExistingKey_ResturnsMaskedTranslationKey()
        {
            // Arrange
            const string translationKey = "NO_KEY";
            const string expected = "[_NO_KEY_]";
            var cultureInfo = Plugin.Multilingual.CrossMultilingual.Current;
            cultureInfo.CurrentCultureInfo = new CultureInfo("en");
            var serviceProvider = Mock.Of<IServiceProvider>();
            var sut = new TranslationExtension {TranslationKey = translationKey};

            // Act
            var result = sut.ProvideValue(serviceProvider);
            var result2 = ((IMarkupExtension) sut).ProvideValue(serviceProvider);

            // Assert
            result.Should().Be(expected);
            result2.Should().Be(expected);
        }

        [TestCase(null, "[__]")]
        [TestCase("", "[__]")]
        [TestCase("  ", "[_  _]")]
        [TestCase(" \r  \n  \r\n  \t   ", "[_ \r  \n  \r\n  \t   _]")]
        public void Translate_ForEmptyKey_ResturnsMaskedNull(string key, string expected)
        {
            // Arrange
            var cultureInfo = Plugin.Multilingual.CrossMultilingual.Current;
            cultureInfo.CurrentCultureInfo = new CultureInfo("en");
            var serviceProvider = Mock.Of<IServiceProvider>();
            var sut = new TranslationExtension {TranslationKey = key};

            // Act
            var result = sut.ProvideValue(serviceProvider);
            var result2 = ((IMarkupExtension) sut).ProvideValue(serviceProvider);

            // Assert
            result.Should().Be(expected);
            result2.Should().Be(expected);
        }
    }
}