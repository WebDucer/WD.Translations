using System;
using System.Globalization;
using System.Resources;

namespace WD.Translations.Abstraction
{
    /// <summary>
    ///     Platform specific culture info
    /// </summary>
    public interface IPlatformCultureInfo
    {
        /// <summary>
        ///     Get the current culture of the OS
        /// </summary>
        /// <returns>Cultture of running OS</returns>
        CultureInfo PlatformCulture { get; }

        /// <summary>
        ///     Get the current set culture for the app (default: same as platform culture)
        /// </summary>
        /// <returns></returns>
        CultureInfo AppCulture { get; }

        /// <summary>
        ///     Set explicite culture of the app
        /// </summary>
        /// <param name="culture">New culture to set as app culture</param>
        /// <param name="updateResourceManagers">Action to update the culture of app resouce managers</param>
        void SetAppCulture(CultureInfo culture, Action<ResourceManager> updateResourceManagers = null);
    }
}