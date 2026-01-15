using UserApi.Application.Validators;
using UserApi.Application.Commands.CreateUser;
using Xunit;
using FluentAssertions;

public class CreateUserValidatorTests
{
    [Fact]
    public void Should_Fail_When_FullName_Is_Empty()
    {
        var validator = new CreateUserCommandValidator();

        var cmd = new CreateUserCommand(
            "",
            "123456",
            "Calle 1",
            1
        );

        var result = validator.Validate(cmd);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Pass_When_Command_Is_Valid()
    {
        var validator = new CreateUserCommandValidator();

        var cmd = new CreateUserCommand(
             "Juan",
             "123456",
             "Calle 1",
             1
         );


     
        var result = validator.Validate(cmd);

        result.IsValid.Should().BeTrue();
    }
}
