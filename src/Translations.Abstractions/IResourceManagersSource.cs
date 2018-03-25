using System.Resources;

namespace WD.Translations.Abstractions
{
    /// <summary>
    ///     Service to get a list of resource managers of the app
    /// </summary>
    public interface IResourceManagersSource
    {
        /// <summary>
        ///     Available resource managers
        /// </summary>
        ResourceManager[] ResourceManagers { get; }
    }
}