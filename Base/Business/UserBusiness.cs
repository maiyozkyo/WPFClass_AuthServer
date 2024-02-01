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

        public async Task<UserDTO> AddUpdateUserAsync(string jsonUser)
        {
            if (string.IsNullOrEmpty(jsonUser))
            {
                return null;
            }

            var user = JsonConvert.DeserializeObject<UserDTO>(jsonUser);
            if (user != null)
            {
                var objUser = await Repository.GetOneAsync(x => x.RecID == user.RecID);
                if (objUser != null)
                {
                    objUser.IsTrial = user.IsTrial;
                    objUser.ModifiedOn = user.ModifiedOn;
                    objUser.ModifiedBy = user.ModifiedBy;
                }
                else
                {
                    objUser = new User
                    {
                        CreatedBy = user.CreatedBy,
                        CreatedOn = user.CreatedOn,
                        Email = user.Email,
                        UserName = user.UserName,
                        IsTrial = user.IsTrial,
                        Password = user.Password,
                    };
                    Repository.Add(objUser);
                }
                await UnitOfWork.SaveChangesAsync();
                user.Password = null;
                return user;
            }
            return null;
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
