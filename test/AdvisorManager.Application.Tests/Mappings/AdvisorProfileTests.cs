using AutoMapper;
using FluentAssertions;
using AdvisorManager.Application.Models.Advisor;
using AdvisorManager.Domain;
using AdvisorManager.Application.Mappings;


namespace AdvisorManager.Application.Tests.Mappings
{
    public class AdvisorProfileTests
    {
        private readonly IMapper _mapper;

        public AdvisorProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AdvisorProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void AdvisorProfile_ShouldHaveValidConfiguration()
        {
            // Arrange & Act
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AdvisorProfile>());

            // Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Advisor_ShouldMapTo_AdvisorDto_WithMaskedSINAndPhoneNumber()
        {
            // Arrange
            var advisor = new Advisor
            {
                Id = Guid.NewGuid(),
                FullName = "Test Advisor",
                SIN = "123456789",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                HealthStatus = "Green"
            };

            // Act
            var advisorDto = _mapper.Map<AdvisorDto>(advisor);

            // Assert
            advisorDto.Should().NotBeNull();
            advisorDto.FullName.Should().Be(advisor.FullName);
            advisorDto.SIN.Should().Be("*********");  // Masked SIN
            advisorDto.PhoneNumber.Should().Be("******7890");  // Partially masked phone number
        }

        [Fact]
        public void AdvisorDto_ShouldMapTo_Advisor()
        {
            // Arrange
            var advisorDto = new AdvisorDto
            {
                Id = Guid.NewGuid(),
                FullName = "Test AdvisorDto",
                SIN = "987654321",
                PhoneNumber = "0987654321",
                Address = "456 Elm St",
                HealthStatus = "Yellow"
            };

            // Act
            var advisor = _mapper.Map<Advisor>(advisorDto);

            // Assert
            advisor.Should().NotBeNull();
            advisor.FullName.Should().Be(advisorDto.FullName);
            advisor.SIN.Should().Be(advisorDto.SIN);  // No masking for reverse mapping
            advisor.PhoneNumber.Should().Be(advisorDto.PhoneNumber);
            advisor.Address.Should().Be(advisorDto.Address);
            advisor.HealthStatus.Should().Be(advisorDto.HealthStatus);
        }
    }

}
