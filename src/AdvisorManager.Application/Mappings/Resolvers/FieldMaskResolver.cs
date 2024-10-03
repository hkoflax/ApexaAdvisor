using AutoMapper;

namespace AdvisorManager.Application.Mappings.Resolvers
{
    /// <summary>
    /// A value converter that masks part or all of a string for privacy purposes.
    /// </summary>
    public class FieldMaskResolver : IValueConverter<string, string>
    {
        private readonly bool _maskAll;
        private readonly int _characterToDisplay;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMaskResolver"/> class.
        /// </summary>
        /// <param name="maskAll">Indicates whether to mask the entire string.</param>
        /// <param name="characterToDisplay">The number of characters to display at the end of the string. Defaults to 4.</param>
        public FieldMaskResolver(bool maskAll = false, int characterToDisplay = 4)
        {
            _maskAll = maskAll;
            _characterToDisplay = characterToDisplay;
        }

        ///<inheritdoc />
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