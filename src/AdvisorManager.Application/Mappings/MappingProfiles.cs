using AutoMapper;

namespace AdvisorManager.Application.Mappings
{
    /// <summary>
    /// A static class that provides all AutoMapper profiles for mapping configurations.
    /// </summary>
    public static class MappingProfiles
    {
        /// <summary>
        /// Gets an enumerable collection of all mapping profiles.
        /// </summary>
        /// <value>
        /// An enumeration of <see cref="Profile"/> objects, which include all mapping profiles used by the application.
        /// </value>
        public static IEnumerable<Profile> All
        {
            get
            {
                yield return new AdvisorProfile();
            }
        }
    }
}
