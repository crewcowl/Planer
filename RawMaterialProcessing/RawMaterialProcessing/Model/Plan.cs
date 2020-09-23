using System.Collections.Generic;

namespace RawMaterialProcessing.Model
{
    class Plan
    {
        public Parties parties {get; private set;}
        public MachineTools machineTools { get; private set; }

        public int startTime { get; private set; }
        public int endTime { get; private set; }

        public Plan(Parties parties, MachineTools machineTools, int startTime, int endTime)
        {
            this.parties = parties;
            this.machineTools = machineTools;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public string PlanToExcelString(List<Nomenclatures> nomenclatureses)
        {
            string excelString = nomenclatureses.Find(n => n.id == this.parties.nomenclaturesId).name + ";" +
                                 this.machineTools.name + ";" + this.startTime + ";" + this.endTime;
            return excelString;
        }

    }
}
