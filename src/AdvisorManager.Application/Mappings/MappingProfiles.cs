using AutoMapper;

namespace AdvisorManager.Application.Mappings
{
    public static class MappingProfiles
    {
        public static IEnumerable<Profile> All
        {
            get
            {
                yield return new AdvisorProfile();
            }
        }
    }
}
