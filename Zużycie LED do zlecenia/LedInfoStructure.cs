using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zużycie_LED_do_zlecenia
{
    class LedInfoStructure
    {
        public LedInfoStructure(string nc12, string id)
        {
            Nc12 = nc12;
            Id = id;
        }

        public string Nc12 { get; }
        public string Id { get; }
    }
}
