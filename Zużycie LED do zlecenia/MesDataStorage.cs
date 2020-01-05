using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MST.MES.ModelInfo;
using static MST.MES.OrderStructureByOrderNo;

namespace Zużycie_LED_do_zlecenia
{
    public static class MesDataStorage
    {
        public static Kitting kittingData;
        public static SMT smtData;
        //public static ModelSpecification modelSpec;
        public static VisualInspection viData;
        public static List<BoxingInfo> boxingData;
        public static void LoadMesData(string orderNo)
        {
            LoadKittingData(orderNo);
            LoadSmtData(orderNo);
            LoadViData(orderNo);
            LoadBoxingData(orderNo);
        }

        public static async Task LoadMesDataAsync(string orderNo)
        {
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() => LoadKittingData(orderNo)));
            taskList.Add(Task.Run(() => LoadSmtData(orderNo)));
            taskList.Add(Task.Run(() => LoadViData(orderNo)));
            taskList.Add(Task.Run(() => LoadBoxingData(orderNo)));
            await Task.WhenAll(taskList);
            //LoadModelInfo(kittingData.modelId);
        }

        private static void LoadKittingData(string orderNo)
        {
            kittingData = MST.MES.SqlDataReaderMethods.Kitting.GetOneOrderByDataReader(orderNo);
        }
        private static void LoadSmtData(string orderNo)
        {
            smtData = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder(orderNo);
        }
        private static void LoadModelInfo(string modelId)
        {
            //modelSpec = MST.MES.SqlDataReaderMethods.MesModels.mesModel(modelId);
        }

        private static void LoadViData(string orderNo)
        {
            viData = MST.MES.SqlDataReaderMethods.VisualInspection.GetViForOneOrder(orderNo);
        }
        private static void LoadBoxingData(string orderNo)
        {
            boxingData = MST.MES.SqlDataReaderMethods.Boxing.GetBoxingForOneOrder(orderNo);
        }
    }
}
