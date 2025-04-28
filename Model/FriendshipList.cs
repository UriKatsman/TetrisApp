using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FriendshipList : List<Friendship>
    {
        public FriendshipList() { }
        public FriendshipList(IEnumerable<Friendship> list) : base(list) { }
        public FriendshipList(IEnumerable<Base> list) : base(list.Cast<Friendship>().ToList()) { }
    }
}
