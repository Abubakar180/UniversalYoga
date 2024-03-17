using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using SQLite;

#if USING_MVVMCROSS
using MvvmCross.Plugins.Sqlite.iOS;
#else
using SQLitePCL;
#endif

namespace UniversalYoga.Services
{
    public class Utils
    {
        /// <summary>
        /// Returns the proper database file path to initialize the SQLite connection. 
        /// </summary>

        public const string DatabaseFilename = "TodoSQLite7.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabaseFilePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
        public static SQLiteConnection CreateConnection()
        {
            #if PCL
               return new SQLiteConnection(new SQLitePlatformIOS(), DatabaseFilePath);
            #else
            return new SQLiteConnection(DatabaseFilePath);
            #endif
        }
    }
}
