using ShoesMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Abstractions
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity? GetOne(string username, string password);
    }
}
