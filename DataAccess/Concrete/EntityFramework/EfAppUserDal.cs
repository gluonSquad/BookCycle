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
    public class EfAppUserDal : EfEntityRepositoryBase<AppUser>, IAppUserDal
    {
        public EfAppUserDal(BookCycleContext bookCycleContext) : base(bookCycleContext)
        {

        }
        public List<AppUser> GetNotAdmin()
        {
            

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
        public List<AppUser> GetNotAdmin(out int totalPage,string searchWord , int currentPage)
        {
          

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


            totalPage = (int)Math.Ceiling((double)result.Count() / 5);

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                result =result.Where(I =>
                    I.FirstName.ToLower().Contains(searchWord.ToLower()) ||
                    I.LastName.ToLower().Contains(searchWord.ToLower()));

                totalPage = (int)Math.Ceiling((double)result.Count() / 5);
            }

            result = result.Skip((currentPage - 1) * 5).Take(5);

            return result.ToList();
        }

    }
}
