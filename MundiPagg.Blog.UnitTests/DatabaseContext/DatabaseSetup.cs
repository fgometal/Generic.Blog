using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MundiPagg.Blog.Domain.Entities;
using MundiPagg.Blog.Repository;
using MundiPagg.Blog.Repository.DatabaseContext;
using MundiPagg.Blog.Service;

namespace MundiPagg.Blog.WebUI.UnitTests.DatabaseContext
{
    /// <summary>
    /// Classe de inicilaização da base de testes.
    /// </summary>
    public static class DatabaseSetup
    {
        /// <summary>
        /// Inidica se o banco já foi recriado
        /// </summary>
        private static bool dbDeletedOnce = false;
        /// <summary>
        /// Instância estática para o banco
        /// </summary>
        private static readonly BlogDBContext databaseContext = new BlogDBContext();
        /// <summary>
        /// inicialização dos serviços.
        /// </summary>
        public static UserService UserService = new UserService();
        public static PostService PostService = new PostService();
        /// <summary>
        /// Método de inicialização da base.
        /// </summary>
        public static void DatabaseStartUp()
        {
            // Indica o caminho para a App_Data aonde está o .mdf da base
            var appDataPath = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().Length - 9) + "App_Data";
            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);

            if (!dbDeletedOnce)
            {
                // Realiza o drop and create da base.
                if (databaseContext.Database.Exists())
                    databaseContext.Database.Delete();

                dbDeletedOnce = true;
            }
            // Passam-se as referências dos repositórios para os serviços
            UserService.Repository = new UserRepository();
            PostService.Repository = new PostRepository();
            // Popula a base com dados para teste.
            AddUsers();
            AddPosts();
        }
        /// <summary>
        /// Adiciona usuários para teste no banco caso não existam.
        /// </summary>
        public static void AddUsers()
        {
            var users = new List<User>
            {
                    new User
                    {
                        Login = "userone",
                        Email = "user1@test.com",
                        FirstName = "User",
                        LastName = "One",
                        AboutMe = "I´m a dummy user.",
                        IsAdmin = false,
                        IsActive = true,
                        DateRegistered = DateTime.Now
                    },
                    new User
                    {
                        Login = "usertwo",
                        Email = "user2@test.com",
                        FirstName = "User",
                        LastName = "Two",
                        AboutMe = "I´m a dummy user.",
                        IsAdmin = false,
                        IsActive = true,
                        DateRegistered = DateTime.Now
                    },
                    new User
                    {
                        Login = "userthree",
                        Email = "user3@test.com",
                        FirstName = "User",
                        LastName = "Three",
                        AboutMe = "I´m a dummy user.",
                        IsAdmin = false,
                        IsActive = true,
                        DateRegistered = DateTime.Now
                    },
                    new User
                    {
                        Login = "userfour",
                        Email = "user4@test.com",
                        FirstName = "User",
                        LastName = "Four",
                        AboutMe = "I´m a dummy user.",
                        IsAdmin = false,
                        IsActive = true,
                        DateRegistered = DateTime.Now
                    }
            };

            foreach (var item in users)
            {
                var existentUser = UserService.GetAll().FirstOrDefault(x => x.Login == item.Login);

                if (existentUser == null)
                    UserService.Save(item);
            }

        }
        /// <summary>
        /// Adiciona posts para teste no banco caso não existam.
        /// </summary>
        public static void AddPosts()
        {
            var posts = new List<Post>
            {
                    new Post
                    {
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        IsActive = true,
                        PostContent = "Content",
                        PublishDate = DateTime.Now,
                        Summary = "Summary",
                        Tags = "tags",
                        Title = "Post 1",
                        User = UserService.GetByLogin("userone"),
                    },
                    new Post
                    {
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        IsActive = true,
                        PostContent = "Content",
                        PublishDate = DateTime.Now,
                        Summary = "Summary",
                        Tags = "tags",
                        Title = "Post 2",
                        User = UserService.GetByLogin("userone"),
                    },
                    new Post
                    {
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        IsActive = true,
                        PostContent = "Content",
                        PublishDate = DateTime.Now,
                        Summary = "Summary",
                        Tags = "tags",
                        Title = "Post 3",
                        User = UserService.GetByLogin("userone"),
                    },
                    new Post
                    {
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        IsActive = true,
                        PostContent = "Content",
                        PublishDate = DateTime.Now,
                        Summary = "Summary",
                        Tags = "tags",
                        Title = "Post 4",
                        User = UserService.GetByLogin("usertwo"),
                    },
                    new Post
                    {
                        Commentaries = new Collection<PostCommentary>(),
                        EditDate = DateTime.Now,
                        IsActive = true,
                        PostContent = "Content",
                        PublishDate = DateTime.Now,
                        Summary = "Summary",
                        Tags = "tags",
                        Title = "Post 5",
                        User = UserService.GetByLogin("usertwo"),
                    }
            };

            foreach (var item in posts)
            {
                var existentPost = PostService.GetAll().FirstOrDefault(x => x.Title == item.Title);

                if (existentPost == null)
                    PostService.Save(item);
            }
        }
    }
}
