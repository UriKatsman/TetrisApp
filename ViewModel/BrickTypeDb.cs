using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BrickTypeDb : BaseDb
    {
        static private BrickTypeList list = new BrickTypeList();
        protected override Base CreateModel(Base entity)
        {
            BrickType b = entity as BrickType;
            b.BrickTypeName = reader["type"].ToString();
            return base.CreateModel(entity);
        }

        public BrickTypeList SelectAll()
        {
            command.CommandText = $"SELECT * FROM BrickTypeTbl";
            BrickTypeList bList = new BrickTypeList(base.Select());
            return bList;
        }

        protected override Base NewEntity()
        {
            return new BrickType();
        }
        public static BrickType SelectById(int id)
        {
            BrickTypeDb db = new BrickTypeDb();
            list = db.SelectAll();

            BrickType g = list.Find(item => item.Id == id);
            return g;
        }
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            BrickType u = entity as BrickType;
            if (u != null)
            {
                string sqlStr = $"DELETE FROM brickTypeTbl where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", u.Id));
            }
        }
        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            BrickType u = entity as BrickType;
            if (u != null)
            {
                string sqlStr = $"INSERT INTO brickTypeTbl ([type]) values (@type)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@type", u.BrickTypeName));
                               
            }
        }

        public override void Delete(Base entity)
        {
            BoardComponentsDb uDB = new BoardComponentsDb();
            BoardComponentsList uList = uDB.SelectAll();

            foreach (BoardComponents u in uList)
                if (u.brickType.Id == entity.Id)
                    uDB.Delete(u);

            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            BrickType u = entity as BrickType;
            if (u != null)
            {
                string sqlStr = $"Update brickTypeTbl SET " +
                                 "[type]=@type " +
                                 "WHERE ID=@ID";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@type", u.BrickTypeName));
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }
    }
}
