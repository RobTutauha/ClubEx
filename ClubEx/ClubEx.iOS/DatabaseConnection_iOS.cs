using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using SQLite;
using System.IO;

namespace ClubEx.iOS 
{
    class DatabaseConnection_iOS : IDatabaseConnection 
    {
        public SQLiteConnection DbConnection() 
        {
            var dbName = "UserDatabase.db3";
            string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", dbName);
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}