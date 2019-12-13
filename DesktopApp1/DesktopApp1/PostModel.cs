using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp1
{
    public class PostModel
    {
        public string action { get; set; }

        public List<string> id { get; set; }
        public List<string> count { get; set; }
        public List<string> name { get; set; }
        public string sellType { get; set; }

        public List<string> sell { get; set; }
        public List<string> cost { get; set; }
        public string Time { get; set; }
        public List<string> sum { get; set; }
    }
}
