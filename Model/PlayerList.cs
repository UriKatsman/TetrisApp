﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PlayerList : List<Player>
    {
        public PlayerList() { }
        public PlayerList(IEnumerable<Player> list) : base(list) { }
        public PlayerList(IEnumerable<Base> list) : base(list.Cast<Player>().ToList()) { }
    }
}
