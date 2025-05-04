using Model;
using System.Net.Http.Json;

namespace MyService
{
    public class Apiservice : IApi
    {
        string uri;
        public HttpClient client;
        public Apiservice()
        {
            uri = "http://localhost:5213";
            client = new HttpClient();
            client.Timeout = new TimeSpan(0,10,0);
        }

        //C:\Users\student.HP-6HYJCV2\Downloads\TetrisApp (3)\TetrisApp\MyService\bin\Debug\net8.0
        public async Task<int> DeleteAdmin(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteAdmin/{id}")).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteFriendship(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteFriendship/{id}")).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteBoardComponents(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteBoardComponents/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteBrickType(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteBrickType/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteLanguage(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteLanguage/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeletePlayer(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeletePlayer/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteUser(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Values/DeleteUser/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<AdminList> GetAllAdmins()
        {
            return await client.GetFromJsonAsync<AdminList>(uri + "/api/Values/AdminSelector");
        }
        public async Task<FriendshipList> GetAllFriendships()
        {
            return await client.GetFromJsonAsync<FriendshipList>(uri + "/api/Values/FriendshipSelector");
        }
        public async Task<BoardComponentsList> GetAllBoardComponents()
        {
            return await client.GetFromJsonAsync<BoardComponentsList>(uri + "/api/Values/BoardComponentsSelector");
        }

        public async Task<BrickTypeList> GetAllBrickTypes()
        {
            return await client.GetFromJsonAsync<BrickTypeList>(uri + "/api/Values/BrickTypeSelector");
        }

        public async Task<LanguageList> GetAllLanguages()
        {
            return await client.GetFromJsonAsync<LanguageList>(uri + "/api/Values/LanguageSelector");
        }

        public async Task<PlayerList> GetAllPlayers()
        {
            return await client.GetFromJsonAsync<PlayerList>(uri + "/api/Values/PlayerSelector");
        }

        public async Task<UserList> GetAllUsers()
        {
            return await client.GetFromJsonAsync<UserList>(uri + "/api/Values/UserSelector");
        }

        public async Task<int> InsertAdmin(Admin x)
        {
            return (await client.PostAsJsonAsync<Admin>(uri + "/api/Values/InsertAdmin", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertFriendship(Friendship x)
        {
            return (await client.PostAsJsonAsync<Friendship>(uri + "/api/Values/InsertFriendship", x)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertBoardComponents(BoardComponents x)
        {
            var response = (await client.PostAsJsonAsync<BoardComponents>(uri + "/api/Values/InsertBoardComponents", x)).IsSuccessStatusCode ? 1 : 0;
            return response;            
        }

        public async Task<int> InsertBrickType(BrickType x)
        {
            return (await client.PostAsJsonAsync<BrickType>(uri + "/api/Values/InsertBrickType", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertLanguage(Language x)
        {
            return (await client.PostAsJsonAsync<Language>(uri + "/api/Values/InsertLanguage", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertPlayer(Player x)
        {
            return (await client.PostAsJsonAsync<Player>(uri + "/api/Values/InsertPlayer", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertUser(User x)
        {
            return (await client.PostAsJsonAsync<User>(uri + "/api/Values/InsertUser", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateAdmin(Admin x)
        {
            return (await client.PutAsJsonAsync<Admin>(uri + "/api/Values/UpdateAdmin", x)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateFriendship(Friendship x)
        {
            return (await client.PutAsJsonAsync<Friendship>(uri + "/api/Values/UpdateFriendship", x)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateBoardComponents(BoardComponents x)
        {
            return (await client.PutAsJsonAsync<BoardComponents>(uri + "/api/Values/UpdateBoardComponents", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateBrickType(BrickType x)
        {
            return (await client.PutAsJsonAsync<BrickType>(uri + "/api/Values/UpdateBrickType", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateLanguage(Language x)
        {
            return (await client.PutAsJsonAsync<Language>(uri + "/api/Values/UpdateLanguage", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdatePlayer(Player x)
        {
            return (await client.PutAsJsonAsync<Player>(uri + "/api/Values/UpdatePlayer", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateUser(User x)
        {
            return (await client.PutAsJsonAsync<User>(uri + "/api/Values/UpdateUser", x)).IsSuccessStatusCode ? 1 : 0;
        }
    }
}
