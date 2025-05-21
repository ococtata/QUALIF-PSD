using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Infrastructure.Repositories
{
    public class Database
    {
        private static LocalDatabaseEntities1 instance;

        public static LocalDatabaseEntities1 GetInstance()
        {
            if(instance == null)
            {
                instance = new LocalDatabaseEntities1();
            }

            return instance;
        }

    }
}