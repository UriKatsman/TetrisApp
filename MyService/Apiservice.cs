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
        }


        public async Task<int> DeleteAdmin(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeleteAdmin/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteBoardComponents(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeleteBoardComponents/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteBrickType(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeleteBrickType/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteLanguage(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeleteLanguage/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeletePlayer(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeletePlayer/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteUser(int id)
        {
            return (await client.DeleteAsync(uri + $"/api/Insert/DeleteUser/{id}")).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<AdminList> GetAllAdmins()
        {
            return await client.GetFromJsonAsync<AdminList>(uri + "/api/Values/AdminSelector");
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
            return (await client.PostAsJsonAsync<Admin>(uri + "/api/Insert/InsertAdmin", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertBoardComponents(BoardComponents x)
        {
            return (await client.PostAsJsonAsync<BoardComponents>(uri + "/api/Insert/InsertBoardComponents", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertBrickType(BrickType x)
        {
            return (await client.PostAsJsonAsync<BrickType>(uri + "/api/Insert/InsertBrickType", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertLanguage(Language x)
        {
            return (await client.PostAsJsonAsync<Language>(uri + "/api/Insert/InsertLanguage", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertPlayer(Player x)
        {
            return (await client.PostAsJsonAsync<Player>(uri + "/api/Insert/InsertPlayer", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> InsertUser(User x)
        {
            return (await client.PostAsJsonAsync<User>(uri + "/api/Insert/InsertUser", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateAdmin(Admin x)
        {
            return (await client.PutAsJsonAsync<Admin>(uri + "/api/Insert/UpdateAdmin", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateBoardComponents(BoardComponents x)
        {
            return (await client.PutAsJsonAsync<BoardComponents>(uri + "/api/Insert/UpdateBoardComponents", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateBrickType(BrickType x)
        {
            return (await client.PutAsJsonAsync<BrickType>(uri + "/api/Insert/UpdateBrickType", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateLanguage(Language x)
        {
            return (await client.PutAsJsonAsync<Language>(uri + "/api/Insert/UpdateLanguage", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdatePlayer(Player x)
        {
            return (await client.PutAsJsonAsync<Player>(uri + "/api/Insert/UpdatePlayer", x)).IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> UpdateUsern(User x)
        {
            return (await client.PutAsJsonAsync<User>(uri + "/api/Insert/UpdateUser", x)).IsSuccessStatusCode ? 1 : 0;
        }
    }
}
