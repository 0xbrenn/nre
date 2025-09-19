using SqlSugar;
using System;
using System.IO;

namespace Sqlite.Db
{

    public class Repository<T> : SimpleClient<T> where T : class, new()
    {
        private static string GetPath
        {

            get
            {
#if DEBUG
                return Environment.CurrentDirectory + @"\tgmu.db";
#else
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TgMuApp\\tgmu.db");
#endif

            }
        }
        public static string ConnectionString = @"DataSource=" + GetPath;

        public Repository(ISqlSugarClient context = null) : base(context)
        {
            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = DbType.Sqlite,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = ConnectionString
                });

                base.Context.Aop.OnLogExecuting = (s, p) =>
                {
                    Console.WriteLine(s);
                };
                
            }
        }


    }
}
