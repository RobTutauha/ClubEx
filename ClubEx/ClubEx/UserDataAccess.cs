using ClubEx;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClubEx
{
    class UserDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<User> Users { get; set; }
        public UserDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<User>();
            this.Users = new ObservableCollection<User>(database.Table<User>());
            AddNewUser();
        }

        //Create
        public void AddNewUser()
        {
            this.Users.Add(new User
            {
                UserFirstname = "Firstname",
                UserLastname = "Lastname",
                UserEmail = "Email Address",
                UserAccountNumber = "Account Number"
            });
        }

        //Read
        public IEnumerable<User> GetAdminUsers()
        {
            lock (collisionLock)
            {
                var query = from user in database.Table<User>()
                            where user.AdminUser == true
                            select user;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<User> GetNonAdminUsers()
        {
            lock (collisionLock)
            {
                var query = from user in database.Table<User>()
                            where user.AdminUser == false
                            select user;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<User> GetUsersBySubscription(bool userSubscription)
        {
            lock (collisionLock)
            {
                var query = from user in database.Table<User>()
                            where user.MemberSubscription == userSubscription
                            select user;
                return query.AsEnumerable();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            lock (collisionLock)
            {
                return database.Query<User>("SELECT * FROM Item").AsEnumerable();
            }
        }

        public User GetUser(int id)
        {
            lock (collisionLock)
            {
                return database.Table<User>().FirstOrDefault(user => user.Id == id);
            }
        }

        //Update
        public int SaveUser(User userInstance)
        {
            lock (collisionLock)
            {
                if (userInstance.Id != 0)
                {
                    database.Update(userInstance);
                    return userInstance.Id;
                }
                else
                {
                    database.Insert(userInstance);
                    return userInstance.Id;
                }
            }
        }
        public void SaveAllUsers()
        {
            lock (collisionLock)
            {
                foreach (var userInstance in this.Users)
                {
                    if (userInstance.Id != 0)
                    {
                        database.Update(userInstance);
                    }
                    else
                    {
                        database.Insert(userInstance);
                    }
                }
            }
        }

        public void ToggleSubscription(User userInstance)
        {
            lock (collisionLock)
            {
                if (userInstance.MemberSubscription == false)
                {
                    userInstance.MemberSubscription = true;
                    database.Update(userInstance);
                }
                else
                {
                    userInstance.MemberSubscription = false;
                    database.Update(userInstance);
                }
            }
        }

        //DELETE
        public int DeleteUser(User userInstance)
        {
            var id = userInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<User>(id);
                }
            }
            this.Users.Remove(userInstance);
            return id;
        }
        public void DeleteAllUsers()
        {
            lock (collisionLock)
            {
                database.DropTable<User>();
                database.CreateTable<User>();
            }
            this.Users = null;
            this.Users = new ObservableCollection<User>(database.Table<User>());
        }
    }
}
