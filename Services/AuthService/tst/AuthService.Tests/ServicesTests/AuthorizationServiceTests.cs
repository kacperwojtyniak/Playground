using AuthService.Interfaces;
using AuthService.Requests;
using AuthService.Responses;
using AuthService.Services;
using Database.Common.ContextProvider;
using Database.Common.Models;
using Moq;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Tests.ServicesTests
{
    public class AuthorizationServiceTests
    {

        [Fact]
        public void LogIn_ThrowsArgumentException_PasswordIsNullOrEmpty()
        {
            var request1 = new LogInRequest() { Login = "User1", Password = null };
            var request2 = new LogInRequest() { Login = "User1", Password = string.Empty };

            var usersRepositoryMock = new Mock<IUsersRepository>();
            var passwordValidatorMock = new Mock<IPasswordValidator>();
            var tokenServiceMock = new Mock<ITokenService>();

            var authorizationService = new AuthenticationService(usersRepositoryMock.Object, passwordValidatorMock.Object, tokenServiceMock.Object);


            Assert.ThrowsAsync<ArgumentException>(async () => await authorizationService.LogInAsync(request1));
            Assert.ThrowsAsync<ArgumentException>(async () => await authorizationService.LogInAsync(request2));
            usersRepositoryMock.Verify(x => x.GetUserByLoginAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void LogIn_ThrowsArgumentException_LoginIsNullOrEmpty()
        {

            var request1 = new LogInRequest() { Login = null, Password = "pwd" };
            var request2 = new LogInRequest() { Login = string.Empty, Password = "pwd2" };

            var usersRepositoryMock = new Mock<IUsersRepository>();
            var passwordValidatorMock = new Mock<IPasswordValidator>();
            var tokenServiceMock = new Mock<ITokenService>();

            var authorizationService = new AuthenticationService(usersRepositoryMock.Object, passwordValidatorMock.Object, tokenServiceMock.Object);

            Assert.ThrowsAsync<ArgumentException>(async () => await authorizationService.LogInAsync(request1));
            Assert.ThrowsAsync<ArgumentException>(async () => await authorizationService.LogInAsync(request2));
            usersRepositoryMock.Verify(x => x.GetUserByLoginAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task LogIn_Invokes_GetUserByLogin()
        {

            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock.Setup(x => x.GetUserByLoginAsync(It.IsAny<string>())).Returns(Task.FromResult(new UserModel()));

            var passwordValidatorMock = new Mock<IPasswordValidator>();
            passwordValidatorMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var tokenServiceMock = new Mock<ITokenService>();

            var request = new LogInRequest() { Login = "User", Password = "pwd" };

            var authorizationService = new AuthenticationService(usersRepositoryMock.Object, passwordValidatorMock.Object, tokenServiceMock.Object);

           await  authorizationService.LogInAsync(request);

            usersRepositoryMock.Verify(x => x.GetUserByLoginAsync(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public  async Task LogIn_InvokesIPasswordValidator_ValidatePassword()
        {

            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock.Setup(x => x.GetUserByLoginAsync(It.IsAny<string>())).Returns(Task.FromResult(new UserModel()));

            var passwordValidatorMock = new Mock<IPasswordValidator>();
            passwordValidatorMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var tokenServiceMock = new Mock<ITokenService>();

            var request = new LogInRequest() { Login = "User", Password = "pwd" };

            var authorizationService = new AuthenticationService(usersRepositoryMock.Object, passwordValidatorMock.Object, tokenServiceMock.Object);

            await authorizationService.LogInAsync(request);

            passwordValidatorMock.Verify(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void LogIn_ThrowsArgumentException_PasswordValidationFailed()
        {

            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock.Setup(x => x.GetUserByLoginAsync(It.IsAny<string>())).Returns(Task.FromResult(new UserModel()));

            var passwordValidatorMock = new Mock<IPasswordValidator>();
            passwordValidatorMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var tokenServiceMock = new Mock<ITokenService>();

            var request = new LogInRequest() { Login = "User", Password = "pwd" };

            var authorizationService = new AuthenticationService(usersRepositoryMock.Object, passwordValidatorMock.Object, tokenServiceMock.Object);

            

            Assert.ThrowsAsync<ArgumentException>(async ()=> await authorizationService.LogInAsync(request));

        }
    }
}
