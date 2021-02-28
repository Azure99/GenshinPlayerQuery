using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinPlayerQuery.Model
{
    class Avatar
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Element { get; set; }

        public int Fetter { get; set; }

        public int Level { get; set; }

        public int Rarity { get; set; }
    }
}
