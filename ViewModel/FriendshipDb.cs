using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class FriendshipDb :BaseDb
    {
        static private FriendshipList list = new FriendshipList();
        protected override Base CreateModel(Base entity)
        {
            Friendship b = entity as Friendship;
            b.player1 = PlayerDb.SelectById(int.Parse(reader["Player1"].ToString()));
            b.player2 = PlayerDb.SelectById(int.Parse(reader["Player2"].ToString()));
            b.isAccepted = bool.Parse(reader["isAccepted"].ToString());
            return base.CreateModel(entity);
        }

        public FriendshipList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Friendship";
            FriendshipList bList = new FriendshipList(base.Select());
            return bList;
        }

        protected override Base NewEntity()
        {
            return new Friendship();
        }
        public static Friendship SelectById(int id)
        {
            FriendshipDb db = new FriendshipDb();
            list = db.SelectAll();

            Friendship g = list.Find(item => item.Id == id);
            return g;
        }
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            Friendship u = entity as Friendship;
            if (u != null)
            {
                string sqlStr = $"DELETE FROM Friendship where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", u.Id));
            }
        }
        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            Friendship u = entity as Friendship;
            if (u != null)
            {
                string sqlStr = $"INSERT INTO Friendship ([player1],[player2],[isAccepted]) values (@player1,@player2,@isAccepted)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@player1", u.player1));
                command.Parameters.Add(new OleDbParameter("@player2", u.player2));
                command.Parameters.Add(new OleDbParameter("@isAccepted", u.isAccepted));

            }
        }

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
            Friendship u = entity as Friendship;
            if (u != null)
            {
                string sqlStr = $"Update Friendship SET " +
                                 "[player1]=@player1, [player2]=@player2, [isAccepted]=@isAccepted " +
                                 "WHERE ID=@ID";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@player1", u.player1));
                command.Parameters.Add(new OleDbParameter("@player2", u.player2));
                command.Parameters.Add(new OleDbParameter("@isAccepted", u.isAccepted));
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }
    }

}
