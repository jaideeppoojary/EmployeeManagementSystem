﻿using EmployeeManagement.Data;
using EmployeeManagement_Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement_Repository
{
    public class UserRepository
    {
        private readonly EmployeeManagementContext dbContext;
        public UserRepository()
        {
            this.dbContext = new EmployeeManagementContext();
        }
        public List<User> GetAllUsersAsync()
        {
            return dbContext.Users.ToList();

        }
        public async Task<User> GetUserById(int Id)
        {
            return dbContext.Users.FirstOrDefault(x => x.UserId == Id);
        }
        public async Task<bool> Update(UserUpdateModel user)
        {

            var existingUser = dbContext.Users.Where(a => a.UserId == user.UserId).FirstOrDefault();

            if (existingUser != null)
            {
                existingUser.FirstName = user.UserFirstName;
                existingUser.LastName = user.UserLastName;
                existingUser.Email = user.UserEmail;
                existingUser.Phone = user.UserPhone;
                existingUser.RoleId = user.RoleId;
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(int UserId)
        {
            var user = await GetById(UserId);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
        private async Task<User> GetById(int UserId)
        {
            return dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
        }

    }
}
