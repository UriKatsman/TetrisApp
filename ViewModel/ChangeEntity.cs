using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ChangeEntity
    {
        private Base entity;
        private CreateSql createSql;
           

        public ChangeEntity(CreateSql createSql, Base entity)
        {
            this.CreateSql = createSql;
            this.Entity = entity;
        }
        

        public Base Entity { get; set; }
        public CreateSql CreateSql { get; set; }
    }
    public delegate void CreateSql(Base entity, OleDbCommand command);
}
