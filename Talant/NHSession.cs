using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace talant
{
    public class NHSession
    {

            private static ISessionFactory sessionfactory;
            private static ISession session;

            static NHSession()
            {
            NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration().Configure();

                config.SetProperty("connection.connection_string", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

                sessionfactory = config.BuildSessionFactory();
            }

            public static ISession Getsession()
            {
                if (session == null || !session.IsOpen)
                    session = sessionfactory.OpenSession();
                return session;
            }       
    }
}