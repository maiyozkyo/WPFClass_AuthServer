﻿using Shoping.Data_Access.DTOs;
using Shoping.Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Business
{
    public interface IUserBusiness
    {
        public Task<UserDTO> AddUpdateUserAsync(string jsonUser);
        public Task<UserDTO> GetUserAsync(string email, string password);
    }
}
