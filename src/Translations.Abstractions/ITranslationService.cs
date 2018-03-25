namespace WD.Translations.Abstractions
{
    /// <summary>
    ///     Service for translations in view models
    /// </summary>
    public interface ITranslationService
    {
        /// <summary>
        ///     Get simple translation for the given translation key
        /// </summary>
        /// <param name="translationKey">Translation key</param>
        /// <returns>Translated value</returns>
        string GetTransalation(string translationKey);

        /// <summary>
        /// </summary>
        /// <param name="translationParameters">Parameters </param>
        /// <param name="translationKey">Translation key</param>
        /// <returns>Translated value</returns>
        string GetFormattedTranslation(string translationKey, params object[] translationParameters);
    }
}