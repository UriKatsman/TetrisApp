using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BoardComponentsDb : BaseDb
    {
        static private BoardComponentsList list = new BoardComponentsList();

        public static BoardComponents SelectById(int id)
        {
            BoardComponentsDb db = new BoardComponentsDb();
            list = db.SelectAll();

            BoardComponents g = list.Find(item => item.Id == id);
            return g;
        }
        public BoardComponentsList SelectAll()
        {
            command.CommandText = $"SELECT * FROM BoardComponentsTbl";
            BoardComponentsList groupList = new BoardComponentsList(base.Select());
            return groupList;
        }

        protected override Base CreateModel(Base entity)
        {
            BoardComponents bc = entity as BoardComponents;
            bc.player = PlayerDb.SelectById(int.Parse(reader["player"].ToString()));
            bc.Row = int.Parse(reader["row"].ToString());
            bc.Col = int.Parse(reader["column"].ToString());
            bc.brickType = BrickTypeDb.SelectById(int.Parse(reader["type"].ToString()));
            base.CreateModel(entity);
            return bc;
        }
        protected override Base NewEntity()
        {
            return new BoardComponents();
        }
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            BoardComponents u = entity as BoardComponents;
            if (u != null)
            {
                string sqlStr = $"DELETE FROM BoardComponentsTbl where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", u.Id));
            }
        }
        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            BoardComponents u = entity as BoardComponents;
            if (u != null)
            {                
                string sqlStr = $"INSERT INTO BoardComponentsTbl ([player],[row],[column],[type]) VALUES (@player,@row,@column,@btype)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@player", u.player.Id));
                command.Parameters.Add(new OleDbParameter("@row", u.Row));
                command.Parameters.Add(new OleDbParameter("@column", u.Col));
                command.Parameters.Add(new OleDbParameter("@btype", u.brickType.Id));

            }
        }
        //protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        //{
        //    BoardComponents u = entity as BoardComponents;
        //    if (u != null)
        //    {

        //        //string sqlStr = $"INSERT INTO BoardComponentsTbl (player,[row],[column],[type]) VALUES (2,1,1,7)";
        //        string sqlStr = $"INSERT INTO BoardComponentsTbl ([player],[row],[column],[type]) VALUES (@player,@row,@column,@btype)";

        //        command.CommandText = sqlStr;
        //        command.Parameters.Add(new OleDbParameter("@player", u.player.Id));
        //        command.Parameters.Add(new OleDbParameter("@row", u.Row));
        //        command.Parameters.Add(new OleDbParameter("@column", u.Col));
        //        command.Parameters.Add(new OleDbParameter("@btype", u.brickType.Id));

        //    }
        //}
        public override void Delete(Base entity)
        {            

            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            BoardComponents u = entity as BoardComponents;
            if (u != null)
            {
                string sqlStr = $"Update BoardComponentsTbl SET " +
                                 "player=@player,row=@row,column=@column,[type]=@type " +
                                 "WHERE ID=@ID";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@player", u.player.Id));
                command.Parameters.Add(new OleDbParameter("@row", u.Row));
                command.Parameters.Add(new OleDbParameter("@col", u.Col));
                command.Parameters.Add(new OleDbParameter("@type", u.brickType.Id));
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }

    }
}

