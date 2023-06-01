using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Oprosnik.Models
{

    public class KadrydbContext: DbContext
    {
        public KadrydbContext() : base("Kadrydb" ) { }       
         
        public List<Kurs> GetKurses()
        {
            return this.Database.SqlQuery<Kurs>("exec dbo.ListKurs").ToList();
        }
    }
    
    public class OprosdbContext : DbContext
    {
        public OprosdbContext() : base("Oprosdb") { }

        public string GetPassword()
        {
            return this.Database.SqlQuery<string>("exec dbo.GetPassword").FirstOrDefault();
        }
        
        public List<string> GetTeachers()   //только актуальные преподаватели
        {
            return this.Database.SqlQuery<string>("exec dbo.GetTeachers").ToList();
        }

      
        public DbSet<Template> Templates { get; set; }
        public DbSet<Anketa> Anketas { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }

        /*
        addTemplate
        DeleteTamplate
        AddAnketa
        DeleteAnketa*/

    }










}