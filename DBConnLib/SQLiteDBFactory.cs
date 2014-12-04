using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Iesi.Collections;
using System.Data.SQLite;

namespace DBConnLib
{
    public class SQLiteDBFactory
    {
        private ISessionFactory _sf;
        private ISession _session;

        public SQLiteDBFactory()
        {
            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.Properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            cfg.Properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            cfg.Properties.Add("connection.connection_string", GetConnectionString());
            cfg.AddAssembly("AuditLogDB");

            _sf = cfg.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            _session = _sf.OpenSession();

            return _session;
        }

        public void CloseSession()
        {
            _session.Close();
        }

        private string GetConnectionString()
        {
            string assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (System.IO.File.Exists(assemblyPath + "\\debug"))
            {
                return "Data Source=|DataDirectory|AuditLogDB.db; Version=3";
            }
            else
            {
                return "Data Source=|DataDirectory|" +
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) +
                    "\\VariantExporter\\" + "AuditLogDB.db; Version=3";
            }
        }
    }
}
