using AutoMapper;

namespace AdvisorManagement.Api.Mappings
{
    public static class MappingProfiles
    {
        public static IEnumerable<Profile> ApiProfiles
        {
            get
            {
                yield return new AdvisorProfile();
            }
        }
    }
}
