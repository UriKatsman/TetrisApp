using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AdminDb : UserDb
    {
        static private AdminList list = new AdminList();

        public static Admin SelectById(int id)
        {
            AdminDb db = new AdminDb();
            list = db.SelectAll();

            Admin g = list.Find(item => item.Id == id);
            return g;
        }
        public AdminList SelectAll()
        {
            command.CommandText = $"SELECT userTbl.*, AdminTbl.AmountBanned FROM (AdminTbl INNER JOIN userTbl ON AdminTbl.ID = userTbl.ID)";
            AdminList groupList = new AdminList(base.Select());            
            
            return groupList;
        }
        protected override Base CreateModel(Base entity)
        {
            Admin adm = entity as Admin;            
            adm.AmountBanned = int.Parse(reader["AmountBanned"].ToString());

            base.CreateModel(entity);
            return adm;
        }
        protected override Base NewEntity()
        {
            return new Admin();
        }
        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            Admin u = entity as Admin;
            if (u != null)
            {
                string sqlStr = $"INSERT into AdminTbl ([AmountBanned],[ID]) values (@amountBanned,@ID)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@amountBanned", u.AmountBanned));
                command.Parameters.Add(new OleDbParameter("@amountBanned", u.Id));


            }
        }

        public override void Insert(Base entity)
        {
            Base reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(base.CreateInsertSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));                
            }
        }
        
        public override void Delete(Base entity)
        {
            Base reqEntity = this.NewEntity();
            if(entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
                deleted.Add(new ChangeEntity(base.CreateDeletedSQL, entity));
            }
        }
        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            Admin u = entity as Admin;
            if (u != null)
            {
                string sqlStr = $"Update AdminTbl SET " +
                                 "AmountBanned=@amountBanned " +
                                 "WHERE ID=@ID";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@amountBanned", u.AmountBanned));               
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }
    }
}
