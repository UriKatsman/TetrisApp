using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User:Base
    {
        public Language language {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
        public override string ToString()
        {
            return $"Username: {UserName}, Password: {Password}, language: {language}";
        }
    }
}
