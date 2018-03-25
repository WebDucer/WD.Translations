using System.Globalization;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Plugin.Multilingual.Abstractions;
using Translations.Tests.TestResources;
using WD.Translations;
using WD.Translations.Abstractions;

namespace Translations.Tests
{
    [TestFixture]
    public class TranslationServiceTests
    {
        [TestCase("en", "Value 1")]
        [TestCase("de", "Wert 1")]
        [TestCase("ru", "Значение 1")]
        public void Translate_ForExistingKeyInCulture_ResturnsTranslation(string culture, string expected)
        {
            // Arrange
            const string translationKey = "Value_1";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo == new CultureInfo(culture));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetTransalation(translationKey);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("de")]
        [TestCase("ru")]
        [TestCase("fr")]
        public void Translate_ForExistingKey_NotForTheCulture_ResurnsTranslationOfDefaultlanguage(string culture)
        {
            // Arrange
            const string translationKey = "Value_2";
            const string expected = "Value 2";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo(culture));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetTransalation(translationKey);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void Translate_ForNotExistingKey_ResturnsMaskedTranslationKey()
        {
            // Arrange
            const string translationKey = "NO_KEY";
            const string expected = "[_NO_KEY_]";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo("en"));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetTransalation(translationKey);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void FormattedTranslate_ForNotExistingKey_ResturnsMaskedTranslationKey()
        {
            // Arrange
            const string translationKey = "NO_KEY";
            const string expected = "[_NO_KEY_]";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo("en"));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetFormattedTranslation(translationKey, 1);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("en", "Value 1")]
        [TestCase("de", "Wert 1")]
        [TestCase("ru", "Значение 1")]
        public void FormattedTranslate_ForExistingKeyInCulture_WithParameters_ResturnsTranslation(string culture, string expected)
        {
            // Arrange
            const string translationKey = "Value_1";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo(culture));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetFormattedTranslation(translationKey, 1);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase(null, "[__]")]
        [TestCase("", "[__]")]
        [TestCase("  ", "[_  _]")]
        [TestCase(" \r  \n  \r\n  \t   ", "[_ \r  \n  \r\n  \t   _]")]
        public void Translate_ForEmptyKey_ResturnsMaskedNull(string key, string expected)
        {
            // Arrange
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo("en"));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetTransalation(key);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase(null, "[__]")]
        [TestCase("", "[__]")]
        [TestCase("  ", "[_  _]")]
        [TestCase(" \r  \n  \r\n  \t   ", "[_ \r  \n  \r\n  \t   _]")]
        public void FormattedTranslate_ForNullKey_ResturnsMaskedNull(string key, string expected)
        {
            // Arrange
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo("en"));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetFormattedTranslation(key, 1);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void FormattedTranslation_ForKey_WithoutArgumet_ReturnTransaltion()
        {
            // Arrange
            const string translationKey = "Value_3";
            const string expected = "Value {0}";
            var resourceSource =
                Mock.Of<IResourceManagersSource>(s => s.ResourceManagers == new[] {TestTranslations.ResourceManager});
            var platformCulture = Mock.Of<IMultilingual>(s => s.CurrentCultureInfo== new CultureInfo("en"));
            var sut = new TranslationService(resourceSource, platformCulture);

            // Act
            var result = sut.GetFormattedTranslation(translationKey);

            // Assert
            result.Should().Be(expected);
        }
    }
}