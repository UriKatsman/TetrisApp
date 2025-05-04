using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace MyService
{
    public interface IApi
    {
        public Task<AdminList> GetAllAdmins();
        public Task<int> DeleteAdmin(int id);
        public Task<int> UpdateAdmin(Admin x);
        public Task<int> InsertAdmin(Admin x);

        public Task<BoardComponentsList> GetAllBoardComponents();
        public Task<int> DeleteBoardComponents(int id);
        public Task<int> UpdateBoardComponents(BoardComponents x);
        public Task<int> InsertBoardComponents(BoardComponents x);

        public Task<FriendshipList> GetAllFriendships();
        public Task<int> DeleteFriendship(int id);
        public Task<int> UpdateFriendship(Friendship x);
        public Task<int> InsertFriendship(Friendship x);

        public Task<BrickTypeList> GetAllBrickTypes();
        public Task<int> DeleteBrickType(int id);
        public Task<int> UpdateBrickType(BrickType x);
        public Task<int> InsertBrickType(BrickType x);

        public Task<LanguageList> GetAllLanguages();
        public Task<int> DeleteLanguage(int id);
        public Task<int> UpdateLanguage(Language x);
        public Task<int> InsertLanguage(Language x);

        public Task<PlayerList> GetAllPlayers();
        public Task<int> DeletePlayer(int id);
        public Task<int> UpdatePlayer(Player x);
        public Task<int> InsertPlayer(Player x);

        public Task<UserList> GetAllUsers();
        public Task<int> DeleteUser(int id);
        public Task<int> UpdateUser(User x);
        public Task<int> InsertUser(User x);
    }
}
