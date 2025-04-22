using Model;
using MyService;
using System.IO.Pipes;
using System.Numerics;
using System.Reflection.Metadata;
using ViewModel;


namespace Test
{
    public class Program
    {
        public static void Main()
        {           
            start();
        }
        public static async void start()
        {
            Apiservice apiservice = new();
            
            AdminList Admins = await apiservice.GetAllAdmins();
            Console.WriteLine(Admins.Count);
            int id = Admins.Last().Id;
            await apiservice.DeleteAdmin(id);
            Console.WriteLine(Admins.Count);

            Admin a1 = new Admin() { language = LanguageDb.SelectById(1), UserName = "mebunan", Password = "shambanana", ProfilePicture = "", AmountBanned = 5 };
            await apiservice.InsertAdmin(a1);
            Admin myAdmin = Admins.First();
            myAdmin.UserName = "LO";
            await apiservice.UpdateAdmin(myAdmin);


            //*/


        }
    }
}
