namespace CostsTest
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    //модель базы с 1 таблицей
    public class CostsDB : DbContext
    {
        public CostsDB()
            : base("name=CostsDB")
        {

        }

        public virtual DbSet<Entity> Entities { get; set; }
    }
}