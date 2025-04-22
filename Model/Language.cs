using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Language:Base
    {
        public string LanguageName { get; set; }

        public override string ToString()
        {
            return this.LanguageName;
        }
    }
}
