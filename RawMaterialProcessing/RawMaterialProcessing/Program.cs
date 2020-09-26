using RawMaterialProcessing.Logic;
using RawMaterialProcessing.Model;
using RawMaterialProcessing.Service;
using System.Collections.Generic;
using System.IO;

namespace RawMaterialProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @".\..\..\InputData\machine_tools.xlsx";
            string path2 = @".\..\..\InputData\nomenclatures.xlsx";
            string path3 = @".\..\..\InputData\parties.xlsx";
            string path4 = @".\..\..\InputData\times.xlsx";

            List<MachineTools> machines = new Serializer<MachineTools>().GetObjectFromXlsString(path1);
            List<Nomenclatures> nomeclatures = new Serializer<Nomenclatures>().GetObjectFromXlsString(path2);
            List<Parties> parties = new Serializer<Parties>().GetObjectFromXlsString(path3);
            List<Times> times = new Serializer<Times>().GetObjectFromXlsString(path4);

            List<Plan> plan = new PlanGenerator().GeneratePlan(parties, machines, times);

            string path = @"plan.xlsx";
            Excel excel = new Excel();
            excel.WritePlan(plan, nomeclatures, path);
            excel.close(Path.GetFullPath(path));

        }
    }
}

