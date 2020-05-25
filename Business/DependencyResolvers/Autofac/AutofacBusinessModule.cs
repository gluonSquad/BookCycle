using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookManager>().As<IBookService>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<AuthorManager>().As<IAuthorService>();
            builder.RegisterType<EfBookDal>().As<IBookDal>();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<EfQuotationDal>().As<IQuotationDal>();
            builder.RegisterType<EfReviewDal>().As<IReviewDal>();

            builder.RegisterType<AppUserManager>().As<IAppUserService>();
            builder.RegisterType<EfAppUserDal>().As<IAppUserDal>();



            builder.RegisterType<BookAppUserManager>().As<IBookAppUserService>();
            builder.RegisterType<QuotationManager>().As<IQuotationService>();
            builder.RegisterType<CommentManager>().As<ICommentService>();
            builder.RegisterType<FileManager>().As<IFileService>();
            builder.RegisterType<EfBookAppUserDal>().As<IBookAppUserDal>();
            //builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}
