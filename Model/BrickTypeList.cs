using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BrickTypeList : List<BrickType>
    {
        public BrickTypeList() { }
        public BrickTypeList(IEnumerable<BrickType> list) : base(list) { }
        public BrickTypeList(IEnumerable<Base> list) : base(list.Cast<BrickType>().ToList()) { }
    }
}
