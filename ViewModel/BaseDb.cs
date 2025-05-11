using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
//C:\Users\student.HP-6HYJCV2\source\repos\TetrisApp3\ViewModel\BaseDb.cs
//C: \Users\student.HP - 6HYJCV2\AppData\Local\SourceServer\4fa36f4581b56fbdc4426b4f2c3a127815a8bd31dd9a7ffe51e21841345fe399\ViewModel\BaseDb.cs
namespace ViewModel
{
    public abstract class BaseDb
    {
        private static bool formated = false;

        //protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\urika\Source\Repos\TetrisApp3\ViewModel\TetrisNMore.accdb";

        protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location
            + "/../../../../../ViewModel/TetrisNMore.accdb");


        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

        public static List<ChangeEntity> inserted = new List<ChangeEntity>();
        public static List<ChangeEntity> deleted = new List<ChangeEntity>();
        public static List<ChangeEntity> updated = new List<ChangeEntity>();

        protected abstract void CreateInsertSQL(Base entity, OleDbCommand cmd);
        protected abstract void CreateUpdateSQL(Base entity, OleDbCommand cmd);
        protected abstract void CreateDeletedSQL(Base entity, OleDbCommand cmd);

        protected abstract Base NewEntity();

        public virtual void Insert(Base entity)
        {
            Base reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSQL, entity));
            }
        }        

        public virtual void Update(Base entity)
        {
            Base reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSQL, entity));
            }
        }

        public virtual void Delete(Base entity)
        {
            Base reqEntity = this.NewEntity();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }

        protected List<Base> Select()
        {
            List<Base> list = new List<Base>();

            try
            {
                command.Connection = connection;
                // command.CommandTGext = sqlStr;
                connection.Open();
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Base entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(
                    e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return list;
        }

        protected async Task<List<Base>> SelectAsync(string sqlStr)
        {
            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            List<Base> list = new List<Base>();

            try
            {
                command.Connection = connection;
                command.CommandText = sqlStr;
                connection.Open();
                this.reader = (OleDbDataReader)await command.ExecuteReaderAsync();


                while (reader.Read())
                {
                    Base entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return list;
        }
        public BaseDb()
        {            
            connection = new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = connection;
            command.CommandTimeout = 600;
        }

        public async Task<int> SaveChanges()
        {
          
            int records_affected = 0;

            try
            {
                command.Connection = connection;
                //if (connection.State != ConnectionState.Open)
                try
                {
                    connection.Open();
                }
                catch (Exception e) { Debug.Write(e.Message); }
                
                
                
                foreach (var entity in inserted)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command); //cmd.CommandText = CreateInsertSQL(entity.Entity);
                    records_affected +=await command.ExecuteNonQueryAsync();

                    command.CommandText = "Select @@Identity";
                    entity.Entity.Id = (int)(await command.ExecuteScalarAsync());
                }
                
                foreach (var entity in updated)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command); //cmd.CommandText = CreateUpdateSQL(entity.Entity);
                    records_affected += await command.ExecuteNonQueryAsync();
                }
                

                foreach (var entity in deleted)
                {
                    command.Parameters.Clear();
                    entity.CreateSql(entity.Entity, command);

                    records_affected += await command.ExecuteNonQueryAsync();
                }

               
            }
            catch (Exception ex)
            {
                var eee = ex;
                var xx = connection;
                
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n SQL:" + command.CommandText);
            }
            finally
            {
                inserted.Clear();
                updated.Clear();
                deleted.Clear();

                if (reader != null) reader.Close();
                if (connection.State == ConnectionState.Open) 
                    connection.Close();                
            }

            return records_affected;
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    int records_affected = 0;
        //    OleDbCommand command = new OleDbCommand();
        //    try
        //    {
        //        command.Connection = this.connection;
        //        this.connection.Open();

        //        foreach (var item in inserted)
        //        {
        //            command.Parameters.Clear();
        //            item.CreateSql(item.Entity, command);
        //            records_affected += await command.ExecuteNonQueryAsync();

        //            command.CommandText = "Select @@Identity";
        //            item.Entity.Id = (int)command.ExecuteScalarAsync().Result;
        //        }

        //        foreach (var item in updated)
        //        {
        //            command.Parameters.Clear();
        //            item.CreateSql(item.Entity, command);
        //            records_affected += await command.ExecuteNonQueryAsync();
        //        }

        //        foreach (var item in deleted)
        //        {
        //            command.Parameters.Clear();
        //            item.CreateSql(item.Entity, command);
        //            records_affected += await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
        //    }
        //    finally
        //    {
        //        inserted.Clear();
        //        updated.Clear();
        //        deleted.Clear();

        //        if (connection.State == ConnectionState.Open) connection.Close();
        //    }

        //    return records_affected;
        //}
        protected virtual Base CreateModel(Base entity)
        {
            entity.Id = (int)reader["id"];
            return entity;
        }
    }
}
