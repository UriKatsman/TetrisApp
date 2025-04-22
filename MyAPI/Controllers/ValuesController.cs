using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [ActionName("LanguageSelector")]
        public LanguageList SelectAllLanguages()
        {
            LanguageDb lDB = new LanguageDb();
            LanguageList lList = lDB.SelectAll();
            return lList;
        }
        [HttpGet]
        [ActionName("AdminSelector")]
        public AdminList SelectAllAdmin()
        {
            AdminDb lDB = new AdminDb();
            return lDB.SelectAll();
        }
        [HttpGet]
        [ActionName("BoardComponentsSelector")]
        public BoardComponentsList SelectAllBoardComponents()
        {
            BoardComponentsDb lDB = new BoardComponentsDb();
            return lDB.SelectAll();
        }
        [HttpGet]
        [ActionName("BrickTypeSelector")]
        public BrickTypeList SelectAllBrickType()
        {
            BrickTypeDb lDB = new BrickTypeDb();
            return lDB.SelectAll();
        }
        [HttpGet]
        [ActionName("PlayerSelector")]
        public PlayerList SelectAllPlayer()
        {
            PlayerDb lDB = new PlayerDb();
            return lDB.SelectAll();
        }
        [HttpGet]
        [ActionName("UserSelector")]
        public UserList SelectAllUser()
        {
            UserDb lDB = new UserDb();
            return lDB.SelectAll();
        }
        [HttpDelete("{id}")]
        public int DeleteAdmin(int id)
        {
            Admin a = AdminDb.SelectById(id);
            AdminDb aDb = new AdminDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteBoardComponents(int id)
        {
            BoardComponents a = BoardComponentsDb.SelectById(id);
            BoardComponentsDb aDb = new BoardComponentsDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteBrickType(int id)
        {
            BrickType a = BrickTypeDb.SelectById(id);
            BrickTypeDb aDb = new BrickTypeDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteLanguage(int id)
        {
            Language a = LanguageDb.SelectById(id);
            LanguageDb aDb = new LanguageDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeletePlayer(int id)
        {
            Player a = PlayerDb.SelectById(id);
            PlayerDb aDb = new PlayerDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpDelete("{id}")]
        public int DeleteUser(int id)
        {
            User a = UserDb.SelectById(id);
            UserDb aDb = new UserDb();
            aDb.Delete(a);
            return aDb.SaveChanges();
        }
        [HttpPost]
        public int InsertAdmin([FromBody] Admin x)
        {
            AdminDb db = new AdminDb();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPost]
        public int InsertBoardComponents([FromBody] BoardComponents x)
        {
            BoardComponentsDb db = new BoardComponentsDb();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPost]
        public int InsertBrickType([FromBody] BrickType x)
        {
            BrickTypeDb db = new();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPost]
        public int InsertLanguage([FromBody] Language x)
        {
            LanguageDb db = new();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPost]
        public int InsertPlayer([FromBody] Player x)
        {
            PlayerDb db = new();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPost]
        public int InsertUser([FromBody] User x)
        {
            UserDb db = new();
            db.Insert(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdateAdmin")]
        public int UpdateAdmin([FromBody] Admin x)
        {
            AdminDb db = new AdminDb();
            db.Update(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdateBoardComponents")]
        public int UpdateBoardComponents([FromBody] BoardComponents x)
        {
            BoardComponentsDb db = new BoardComponentsDb();
            db.Update(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdateBrickType")]
        public int UpdateBrickType([FromBody] BrickType x)
        {
            BrickTypeDb db = new BrickTypeDb();
            db.Update(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdateLanguage")]
        public int UpdateLanguage([FromBody] Language x)
        {
            LanguageDb db = new LanguageDb();
            db.Update(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdatePlayer")]
        public int UpdatePlayer([FromBody] Player x)
        {
            PlayerDb db = new PlayerDb();
            db.Update(x);
            return db.SaveChanges();
        }
        [HttpPut]
        [ActionName("UpdateUser")]
        public int UpdateUser([FromBody] User x)
        {
            UserDb db = new UserDb();
            db.Update(x);
            return db.SaveChanges();
        }        

    }
}
