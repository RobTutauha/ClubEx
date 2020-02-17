using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClubEx.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace ClubEx.Droid {
    public class DatabaseConnection_Android : IDatabaseConnection 
    { 
        public SQLiteConnection DbConnection() 
        { 
            var dbName = "UserDatabase.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}