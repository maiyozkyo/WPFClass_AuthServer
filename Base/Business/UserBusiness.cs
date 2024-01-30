using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shoping.Data_Access.DTOs;
using Shoping.Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Business
{
    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        public UserBusiness(IConfiguration configuration, string type) : base(configuration, type)
        {
        }

        public async Task<UserDTO> GetUserAsync(string email, string password)
        {
            try
            {
                var user = await Repository.GetOneAsync(x => x.Email == email && password == x.Password);
                var userDTO = new UserDTO();
                userDTO = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(user));
                return userDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

    }
}
