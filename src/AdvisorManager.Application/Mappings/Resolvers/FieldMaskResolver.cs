using AutoMapper;

namespace AdvisorManager.Application.Mappings.Resolvers
{
    public class FieldMaskResolver : IValueConverter<string, string>
    {
        private readonly bool _maskAll;
        private readonly int _characterToDisplay;

        public FieldMaskResolver(bool maskAll = false, int characterToDisplay = 4)
        {
            _maskAll = maskAll;
            _characterToDisplay = characterToDisplay;
        }

        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(sourceMember) && sourceMember.Length > 0)
            {
                if (_maskAll)
                    return new string('*', sourceMember.Length);

                var charToDisplay = Math.Min(_characterToDisplay, sourceMember.Length);
                var maskChars = new string('*', sourceMember.Length - charToDisplay);
                return $"{maskChars}{sourceMember[^charToDisplay..]}";
            }

            return string.Empty;
        }
    }
}