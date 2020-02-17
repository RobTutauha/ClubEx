using System;
using System.Collections.Generic;
using System.Text;

namespace ClubEx
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
