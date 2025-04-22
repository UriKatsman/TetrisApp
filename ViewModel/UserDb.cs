using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserDb : BaseDb
    {
        static private UserList list = new UserList();

        public static User SelectById(int id)
        {
            UserDb db = new UserDb();
            list = db.SelectAll();

            User g = list.Find(item => item.Id == id);
            return g;
        }
        public UserList SelectAll()
        {
            command.CommandText = $"SELECT * FROM userTbl";
            UserList groupList = new UserList(base.Select());
            return groupList;
        }

        protected override Base CreateModel(Base entity)
        {
            User usr = entity as User;
            usr.UserName = reader["username"].ToString();
            usr.Password = reader["password"].ToString();
            //usr.ProfilePicture = reader["profilePicture"].ToString();
            usr.language = LanguageDb.SelectById(int.Parse(reader["language"].ToString()));

            //C:\Users\student.HP-6HYJCV2\Downloads\TetrisApp (3)\TetrisApp\ViewModel\Images\firstProfile.jpg
            /*
            string path=Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location
            + "/../../../../../../TetrisApp/ViewModel/images/");

            string imagePath = path + reader["profilePicture"].ToString();
            string base64String = ImageToBase64Converter.ImageToBase64(imagePath);
            //usr.ProfilePicture = base64String;
            //*/
            
            base.CreateModel(entity);
            return usr;
        }
        protected override Base NewEntity()
        {
            return new User();
        }
        
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            User u = entity as User;
            if (u !=null)
            {
                string sqlStr = $"DELETE FROM userTbl where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid",u.Id));
            }
        }

        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            User u = entity as User;
            if (u != null)
            {
                //string sqlStr = $"INSERT into userTbl ([language], [username], [password], [profilePicture]) values (@language,@username, @password, @profilePicture)";
                string sqlStr = $"INSERT into userTbl ([language], [username], [password]) values (@language,@username, @password)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@language", u.language.Id));
                command.Parameters.Add(new OleDbParameter("@username", u.UserName));
                command.Parameters.Add(new OleDbParameter("@password", u.Password));
                //command.Parameters.Add(new OleDbParameter("@profilePicture", u.ProfilePicture));
            }
        }
        public override void Delete(Base entity)
        {
            PlayerDb uDB = new PlayerDb();
            PlayerList uList = uDB.SelectAll();

            foreach (Player u in uList)
                if (u.Id == entity.Id)
                    uDB.Delete(u);

            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        public virtual void Delete(Base entity, bool isFromFamily = false)
        {
            if (!isFromFamily) 
            { 
            PlayerDb uDB = new PlayerDb();
            PlayerList uList = uDB.SelectAll();

            foreach (Player u in uList)
                if (u.Id == entity.Id)
                    uDB.Delete(u);
            }
            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }

        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            User u = entity as User;
            if (u != null) 
            {
                //string sqlStr = $"Update userTbl SET " +
                //                 "[language]=@language,[username]=@username,[password]=@password,[profilePicture]=@profilePicture " +
                //                 "WHERE ID=@ID";
                string sqlStr = $"Update userTbl SET " +
                                "[language]=@language,[username]=@username,[password]=@password " +
                                "WHERE ID=@ID";


                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@language",u.language.Id));
                command.Parameters.Add(new OleDbParameter("@username", u.UserName));
                command.Parameters.Add(new OleDbParameter("@password", u.Password));
                //command.Parameters.Add(new OleDbParameter("@ProfilePicture", u.ProfilePicture));
                command.Parameters.Add(new OleDbParameter("@ID", u.Id));
            }
        }
    }
}
