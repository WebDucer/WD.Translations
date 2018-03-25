using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using WD.Translations.Abstractions;

namespace WD.Translations
{
    /// <summary>
    /// Source fot app resource managers
    /// </summary>
    public class ResourceManagersSource : IResourceManagersSource
    {
        /// <summary>
        ///     Default contructor to initialize available resource managers
        /// </summary>
        /// <param name="resourcemanagers">Resource managers to initialize with</param>
        public ResourceManagersSource(params ResourceManager[] resourcemanagers)
        {
            if (Current != null)
            {
                throw new NotSupportedException("The custructor could be called only once");
            }

            ResourceManagers = resourcemanagers;
            Current = this;
        }

        /// <summary>
        ///     Constructor for initialization with resource id and assembly
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <param name="assembly">Resource assembly</param>
        public ResourceManagersSource(string resourceId, Assembly assembly) : this(new ResourceManager(resourceId, assembly))
        {
        }

        /// <summary>
        ///     Constructor for initialization with more than one resource manager from ID
        /// </summary>
        /// <param name="resouceCollection">Collection of resource IDs and corresponding assemblies</param>
        public ResourceManagersSource(Dictionary<string, Assembly> resouceCollection) : this(resouceCollection
            .Select(s => new ResourceManager(s.Key, s.Value)).ToArray())
        {
        }

        /// <summary>
        /// Current singleton of resource managers source
        /// </summary>
        public static IResourceManagersSource Current { get; private set; }

        #region Implementation of IResourceManagersSource

        /// <inheritdoc />
        public ResourceManager[] ResourceManagers { get; }

        #endregion
    }
}