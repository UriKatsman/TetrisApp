using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PlayerDb : UserDb
    {
        static private PlayerList list = new PlayerList();

        public static Player SelectById(int id)
        {
            PlayerDb db = new PlayerDb();
            list = db.SelectAll();

            Player g = list.Find(item => item.Id == id);
            return g;
        }
        public PlayerList SelectAll()
        {
            //command.CommandText = $"SELECT PlayerTbl.[ID], PlayerTbl.[TetrisHighScore], PlayerTbl.[TetrisCurrentScore], userTbl.[username], userTbl.[password], userTbl.[language], userTbl.[profilePicture] FROM (PlayerTbl INNER JOIN userTbl ON PlayerTbl.ID = userTbl.ID)";
            command.CommandText = $"SELECT PlayerTbl.[ID], PlayerTbl.[TetrisHighScore], PlayerTbl.[TetrisCurrentScore], userTbl.[username], userTbl.[password], userTbl.[language] FROM (PlayerTbl INNER JOIN userTbl ON PlayerTbl.ID = userTbl.ID)";
            PlayerList groupList = new PlayerList(base.Select());
            return groupList;
        }
        protected override Base CreateModel(Base entity)
        {            
            Player plr = entity as Player;
            plr.TetrisHighScore = int.Parse(reader["TetrisHighScore"].ToString());
            plr.TetrisCurrentScore = int.Parse(reader["TetrisCurrentScore"].ToString());
            
            base.CreateModel(entity);
            return plr;
        }
        protected override Base NewEntity()
        {
            return new Player();
        }
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            Player u = entity as Player;
            if (u != null)
            {
                string sqlStr = $"DELETE FROM PlayerTbl where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", u.Id));
            }
        }

        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            Player u = entity as Player;
            if (u != null)
            {
                string sqlStr = $"INSERT into PlayerTbl ([ID],[TetrisHighScore], [TetrisCurrentScore]) values (@id,@TetrisHighScore,@TetrisCurrentScore)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@id", u.Id));
                command.Parameters.Add(new OleDbParameter("@TetrisHighScore", u.TetrisHighScore));
                command.Parameters.Add(new OleDbParameter("@TetrisCurrentScore", u.TetrisCurrentScore));
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
            BoardComponentsDb bDB = new BoardComponentsDb();
            BoardComponentsList bList = bDB.SelectAll();

            foreach (BoardComponents b in bList)
                if (b.brickType.Id == entity.Id)
                    bDB.Delete(b);

            UserDb uDB = new UserDb();
            UserList uList = uDB.SelectAll();

            foreach (User u in uList)
                if (u.Id == entity.Id)
                    uDB.Delete(u,true);

            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }

        public override void Delete(Base entity, bool isFromFamily = false)
        {
            BoardComponentsDb bDB = new BoardComponentsDb();
            BoardComponentsList bList = bDB.SelectAll();

            foreach (BoardComponents b in bList)
                if (b.brickType.Id == entity.Id)
                    bDB.Delete(b);



            if (!isFromFamily)
            {
                UserDb uDB = new UserDb();
                UserList uList = uDB.SelectAll();

                foreach (User u in uList)
                    if (u.Id == entity.Id)
                        uDB.Delete(u,true);
            }


            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            Player u = entity as Player;
            if (u != null)
            {
                string sqlStr = $"Update PlayerTbl SET " +
                                 "TetrisHighScore=@TH, TetrisCurrentScore=@TC " +
                                 "WHERE ID=@ID";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@TH", u.TetrisHighScore));
                command.Parameters.Add(new OleDbParameter("@TC", u.TetrisCurrentScore));              
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }
    }
}
