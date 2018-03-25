using System;
using System.Linq;
using System.Threading;
using Plugin.Multilingual;
using Plugin.Multilingual.Abstractions;
using WD.Translations.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WD.Translations
{
    /// <summary>
    ///     Extension for Localization in XAML files
    /// </summary>
    [ContentProperty(nameof(TranslationKey))]
    public class TranslationExtension : IMarkupExtension<string>
    {
        /// <summary>
        ///     Get all configured resource managers for the app
        /// </summary>
        protected static Lazy<IResourceManagersSource> ResourceSource =>
            new Lazy<IResourceManagersSource>(() => ResourceManagersSource.Current, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        ///     Culture for translation
        /// </summary>
        protected static Lazy<IMultilingual> PlatformCulture => new Lazy<IMultilingual>(() => CrossMultilingual.Current);

        /// <summary>
        ///     Format string to represent not translated value (default: '[_TranslationKey_]')
        /// </summary>
        protected virtual string NotFoundFormatString { get; } = "[_{0}_]";

        /// <summary>
        ///     Translation key to use for the translaion
        /// </summary>
        public string TranslationKey { get; set; }

        /// <summary>
        ///     Translate the translation key
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>Translated value</returns>
        public virtual string ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(TranslationKey))
            {
                return string.Format(NotFoundFormatString, string.Empty);
            }

            var translation = ResourceSource.Value.ResourceManagers
                .Select(s => s.GetString(TranslationKey, PlatformCulture.Value.CurrentCultureInfo))
                .FirstOrDefault(s => s != null);

            return translation ?? string.Format(NotFoundFormatString, TranslationKey);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}