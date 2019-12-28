using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graffiti.MST.ComponentsTools;

namespace Zużycie_LED_do_zlecenia
{
    public class GraffitiComponents
    {
        public static IEnumerable<ComponentStruct> allComponents;

        public static void LoadComponents()
        {
            var locations = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsInLocations("EL2.");
            var componentsList = locations.SelectMany(x => x.Value).Where(x => x.StartsWith("4010460") || x.StartsWith("4010560")).ToList();
            allComponents = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(componentsList);
        }
    }
}
