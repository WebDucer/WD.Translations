using System.Linq;
using WD.Translations.Abstraction;

namespace WD.Translations
{
    /// <inheritdoc />
    public class TranslationService : ITranslationService
    {
        /// <inheritdoc />
        public TranslationService(IResourceManagersSource resourceSource, IPlatformCultureInfo platformCulture)
        {
            ResourceSource = resourceSource;
            PlatformCulture = platformCulture;
        }

        /// <summary>
        ///     Source for resources
        /// </summary>
        public IResourceManagersSource ResourceSource { get; }

        /// <summary>
        ///     Platform cuture
        /// </summary>
        public IPlatformCultureInfo PlatformCulture { get; }

        /// <summary>
        ///     Format string to represent not translated value (default: '[_TranslationKey_]')
        /// </summary>
        protected virtual string NotFoundFormatString { get; } = "[_{0}_]";

        /// <summary>
        ///     Get the translation (or NULL, if none found)
        /// </summary>
        /// <param name="translationKey">Translation key</param>
        /// <returns>translated value or NULL</returns>
        protected virtual string GetBaseTranslation(string translationKey)
        {
            if (string.IsNullOrWhiteSpace(translationKey))
            {
                return null;
            }

            var translation = ResourceSource.ResourceManagers
                .Select(s => s.GetString(translationKey, PlatformCulture.AppCulture))
                .FirstOrDefault(s => s != null);

            return translation;
        }

        #region Implementation of ITranslationService

        /// <inheritdoc />
        public virtual string GetTransalation(string translationKey)
        {
            return GetBaseTranslation(translationKey) ?? string.Format(NotFoundFormatString, translationKey);
        }

        /// <inheritdoc />
        public virtual string GetFormattedTranslation(string translationKey, params object[] translationParameters)
        {
            var translation = GetBaseTranslation(translationKey);

            if (translation == null)
            {
                return string.Format(NotFoundFormatString, translationKey);
            }

            if (translationParameters == null || translationParameters.Length == 0)
            {
                return translation;
            }

            return string.Format(translation, translationParameters);
        }

        #endregion
    }
}