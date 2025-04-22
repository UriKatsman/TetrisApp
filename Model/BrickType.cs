using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BrickType:Base
    {
        public string BrickTypeName { get; set; }

        public override string ToString()
        {
            return this.BrickTypeName;
        }
    }
}
