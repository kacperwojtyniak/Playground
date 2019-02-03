using AuthService.Repositories;
using Database.Common.ContextProvider;
using Database.Common.Contexts;
using Database.Common.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace AuthService.Tests.ServicesTests
{
    public class UsersRepositoryTests
    {

        [Fact]
        public void GetUserByLogin_ThrowsArgumentException_UserNotFound()
        {
            var contextMock = new Mock<IMongoContext<BaseModel>>();
            contextMock.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<BaseModel, bool>>>())).Throws(new Exception());
            var contextProviderMock = new Mock<IContextProvider>();
            contextProviderMock.Setup(x => x.GetContext<BaseModel>()).Returns(contextMock.Object);

            var usersRepository = new UsersRepository(contextProviderMock.Object);

            Assert.ThrowsAsync<ArgumentException>(() => usersRepository.GetUserByLoginAsync(It.IsAny<string>()));
        }
    }
}
