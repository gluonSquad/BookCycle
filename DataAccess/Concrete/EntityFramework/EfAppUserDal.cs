using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAppUserDal :  IAppUserDal
    {
        public List<AppUser> GetNotAdmin()
        {
            using var context = new BookCycleContext();

            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId,
                (resultUser, resultUserRole) => new
                {
                    user = resultUser,
                    userRole = resultUserRole
                }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
                (resultTable, resultRole) => new
                {
                    user = resultTable.user,
                    userRoles = resultTable.userRole,
                    roles = resultRole
                }).Where(I=>I.roles.Name == "Member").Select(I=>new AppUser()
            {
                Id = I.user.Id,
                FirstName = I.user.FirstName,
                LastName = I.user.LastName,
                ProfileImageFile = I.user.ProfileImageFile,
                Email =  I.user.Email,
                UserName = I.user.UserName
            }).ToList();

        }
        public List<AppUser> GetNotAdmin(string searchWord , int currentPage)
        {
            using var context = new BookCycleContext();

            var result = context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId,
                (resultUser, resultUserRole) => new
                {
                    user = resultUser,
                    userRole = resultUserRole
                }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
                (resultTable, resultRole) => new
                {
                    user = resultTable.user,
                    userRoles = resultTable.userRole,
                    roles = resultRole
                }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                FirstName = I.user.FirstName,
                LastName = I.user.LastName,
                ProfileImageFile = I.user.ProfileImageFile,
                Email = I.user.Email,
                UserName = I.user.UserName
            });


            if(!string.IsNullOrWhiteSpace(searchWord))
            {
                result =result.Where(I =>
                    I.FirstName.ToLower().Contains(searchWord.ToLower()) ||
                    I.LastName.ToLower().Contains(searchWord.ToLower()));
            }

            result = result.Skip((currentPage - 1) * 3).Take(3);

            return result.ToList();
        }

    }
}
