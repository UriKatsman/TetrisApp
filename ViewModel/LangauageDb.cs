using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LanguageDb : BaseDb
    {
        static private LanguageList list = new LanguageList();
        protected override Base CreateModel(Base entity)
        {
            Language l = entity as Language;
            l.LanguageName = reader["language"].ToString();
            return base.CreateModel(entity);
        }

        public LanguageList SelectAll()
        {
            command.CommandText = $"SELECT * FROM languageTbl";
            LanguageList lList = new LanguageList(base.Select());
            return lList;
        }

        protected override Base NewEntity()
        {
            return new Language();
        }
        public static Language SelectById(int id)
        {
            LanguageDb db = new LanguageDb();
            list = db.SelectAll();

            Language g = list.Find(item => item.Id == id);
            return g;
        }
        protected override void CreateDeletedSQL(Base entity, OleDbCommand cmd)
        {
            Language u = entity as Language;
            if (u != null)
            {
                string sqlStr = $"DELETE FROM languageTbl where id=@pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", u.Id));
            }
        }
        protected override void CreateInsertSQL(Base entity, OleDbCommand cmd)
        {
            Language u = entity as Language;
            if (u != null)
            {
                //string sqlStr = $"INSERT INTO brickTypeTbl (type) values (@type)";
                string sqlStr = $"INSERT INTO languageTbl ([language]) values (@language)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@language", u.LanguageName));

            }
        }

        public override void Delete(Base entity)
        {
            UserDb uDB = new UserDb();
            UserList uList = uDB.SelectAll();

            foreach (User u in uList)
                if (u.language.Id == entity.Id)
                    uDB.Delete(u);

            Base reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));                
            }
        }
        protected override void CreateUpdateSQL(Base entity, OleDbCommand cmd)
        {
            Language u = entity as Language;
            if (u != null)
            {
                string sqlStr = $"Update languageTbl " +
                                 " SET [language]=@language " +
                                 "WHERE Id=@E";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@language", u.LanguageName));
                command.Parameters.Add(new OleDbParameter("@E", u.Id));
            }
        }
    }
}
