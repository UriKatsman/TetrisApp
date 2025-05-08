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
        [ActionName("FriendshipSelector")]
        public FriendshipList SelectAllFriendships()
        {
            FriendshipDb lDB = new FriendshipDb();
            FriendshipList lList = lDB.SelectAll();
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
            PlayerList pList= lDB.SelectAll();
            return pList;
        }
        [HttpGet]
        [ActionName("UserSelector")]
        public UserList SelectAllUser()
        {
            UserDb lDB = new UserDb();
            return lDB.SelectAll();
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAdmin")]
        public int DeleteAdmin(int id)
        {
            Admin a = AdminDb.SelectById(id);
            AdminDb aDb = new AdminDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteFriendship")]
        public int DeleteFriendship(int id)
        {
            Friendship a = FriendshipDb.SelectById(id);
            FriendshipDb aDb = new FriendshipDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteBoardComponents")]
        public int DeleteBoardComponents(int id)
        {
            BoardComponents a = BoardComponentsDb.SelectById(id);
            BoardComponentsDb aDb = new BoardComponentsDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteBrickType")]
        public int DeleteBrickType(int id)
        {
            BrickType a = BrickTypeDb.SelectById(id);
            BrickTypeDb aDb = new BrickTypeDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteLanguage")]
        public int DeleteLanguage(int id)
        {
            Language a = LanguageDb.SelectById(id);
            LanguageDb aDb = new LanguageDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeletePlayer")]
        public int DeletePlayer(int id)
        {
            Player a = PlayerDb.SelectById(id);
            PlayerDb aDb = new PlayerDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteUser")]
        public int DeleteUser(int id)
        {
            User a = UserDb.SelectById(id);
            UserDb aDb = new UserDb();
            aDb.Delete(a);
            return aDb.SaveChanges().Result;
        }
        [HttpPost]
        [ActionName("InsertAdmin")]
        public int InsertAdmin([FromBody] Admin x)
        {
            AdminDb db = new AdminDb();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        [HttpPost]
        [ActionName("InsertFriendship")]
        public int InsertFriendship([FromBody] Friendship x)
        {
            FriendshipDb db = new FriendshipDb();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        
        [HttpPost]
        [ActionName("InsertBrickType")]
        public int InsertBrickType([FromBody] BrickType x)
        {
            BrickTypeDb db = new();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        [HttpPost]
        [ActionName("InsertLanguage")]
        public int InsertLanguage([FromBody] Language x)
        {
            LanguageDb db = new();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        [HttpPost]
        [ActionName("InsertPlayer")]
        public int InsertPlayer([FromBody] Player x)
        {
            PlayerDb db = new();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        [HttpPost]
        [ActionName("InsertUser")]
        public int InsertUser([FromBody] User x)
        {
            UserDb db = new();
            db.Insert(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateAdmin")]
        public int UpdateAdmin([FromBody] Admin x)
        {
            AdminDb db = new AdminDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateFriendship")]
        public int UpdateFriendship([FromBody] Friendship x)
        {
            FriendshipDb db = new FriendshipDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateBoardComponents")]
        public int UpdateBoardComponents([FromBody] BoardComponents x)
        {
            BoardComponentsDb db = new BoardComponentsDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateBrickType")]
        public int UpdateBrickType([FromBody] BrickType x)
        {
            BrickTypeDb db = new BrickTypeDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateLanguage")]
        public int UpdateLanguage([FromBody] Language x)
        {
            LanguageDb db = new LanguageDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdatePlayer")]
        public int UpdatePlayer([FromBody] Player x)
        {
            PlayerDb db = new PlayerDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }
        [HttpPut]
        [ActionName("UpdateUser")]
        public int UpdateUser([FromBody] User x)
        {
            UserDb db = new UserDb();
            db.Update(x);
            return db.SaveChanges().Result;
        }

    }
}
