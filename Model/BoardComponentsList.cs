using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BoardComponentsList : List<BoardComponents>
    {
        public BoardComponentsList() { }
        public BoardComponentsList(IEnumerable<BoardComponents> list) : base(list) { }
        public BoardComponentsList(IEnumerable<Base> list) : base(list.Cast<BoardComponents>().ToList()) { }
    }
}
