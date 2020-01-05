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
        public static List<ComponentStruct> allComponents;
        public static List<ComponentStruct> componentsWithHistory;

        public static void LoadComponents()
        {
            allComponents = Graffiti.MST.OrdersOperations.GetData.GetComponnetsConnectedToOrder(MesDataStorage.kittingData.GraffitiOrderNo.PrimaryKey_00).ToList();
            var qrCodes = allComponents.Select(c => c.QrCode).ToArray();
            componentsWithHistory = Graffiti.MST.ComponentsTools.GetDbData.GetComponentHistoryBatch(qrCodes).ToList();
        }
    }
}
