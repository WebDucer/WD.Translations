﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
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
        protected ResourceManagersSource(params ResourceManager[] resourcemanagers)
        {
            ResourceManagers = resourcemanagers;
        }

        /// <summary>
        /// Initialize singleton instance
        /// </summary>
        /// <param name="resourcemanagers">Resource managers to initialize with</param>
        /// <returns>Initialized instance</returns>
        public static IResourceManagersSource Init(params ResourceManager[] resourcemanagers)
        {
            if (Current == null)
            {
                Current = new ResourceManagersSource(resourcemanagers);
            }
            else
            {
                throw new NotSupportedException("The initialization could be called only once");
            }

            return Current;
        }

        /// <summary>
        /// Initialize singleton instance
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <param name="assembly">Resource assembly</param>
        /// <returns>Initialized instance</returns>
        public static IResourceManagersSource Init(string resourceId, Assembly assembly)
        {
            return Init(new ResourceManager(resourceId, assembly));
        }

        /// <summary>
        /// Initialize singleton instance
        /// </summary>
        /// <param name="resouceCollection">Collection of resource IDs and corresponding assemblies</param>
        /// <returns>Initialized instance</returns>
        public static IResourceManagersSource Init(Dictionary<string, Assembly> resouceCollection)
        {
            return Init(resouceCollection
                .Select(s => new ResourceManager(s.Key, s.Value)).ToArray());
        }

        /// <summary>
        /// Current singleton of resource managers source
        /// </summary>
        public static IResourceManagersSource Current { get; protected set; }

        #region Implementation of IResourceManagersSource

        /// <inheritdoc />
        public virtual ResourceManager[] ResourceManagers { get; }

        #endregion
    }
}