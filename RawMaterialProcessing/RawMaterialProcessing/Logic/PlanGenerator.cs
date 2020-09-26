using RawMaterialProcessing.Model;
using System.Collections.Generic;
using System.Linq;

namespace RawMaterialProcessing.Logic
{
     class PlanGenerator
    {
        //Убрать статику
        /*generating plan*/
        public List<Plan> GeneratePlan(List<Parties> parties, List<MachineTools> machines, List<Times> times)
        {
            List<MachineWorkTime> machineWorkTimes = new List<MachineWorkTime>();
            
            foreach (var machine in machines)
            {
                machineWorkTimes.Add(new MachineWorkTime(machine));
            }

            return GetPlans(parties, machines, times, machineWorkTimes);
        }
        
        private List<Plan> GetPlans(List<Parties> parties, List<MachineTools> machines, List<Times> times, List<MachineWorkTime> machineWorkTimes)
        {
            List<Plan> plan = new List<Plan>();
            
            parties.ForEach(part => plan.Add(GetPlan(times, machines, machineWorkTimes, part)));

            return plan;
        }

        private Plan GetPlan(List<Times> times, List<MachineTools> machines,
            List<MachineWorkTime> machineWorkTimes, Parties part)
        {
            var getTimes = times.FindAll(t => t.nomenclaturesId == part.nomenclaturesId);
            var machineWithMinWorkTime = GetMachineWithMinWorkTime(machines, machineWorkTimes, getTimes);
            var machineWorkTime = machineWorkTimes.Find(p => p.Machine == machineWithMinWorkTime);
            var time = machineWorkTime.WorkingTime;
            var minTime = getTimes.Find(t => t.machineToolId == machineWithMinWorkTime.id).times;

            machineWorkTime.WorkingTime += minTime;

            return new Plan(part, machineWithMinWorkTime, time, time + minTime);
        }

        /*find machine with min work time*/
        private MachineTools GetMachineWithMinWorkTime(List<MachineTools> machines,
            List<MachineWorkTime> machineWorkTimes, List<Times> workingTimes)
        {
            var checkTime = machineWorkTimes.Find(m => m.Machine.id == workingTimes.Min().machineToolId).WorkingTime;
            var machine = machines[workingTimes.Min().machineToolId];
            foreach (var takerTimes in workingTimes)
            {
                var end_time = machineWorkTimes.FindLast(p => p.Machine.id == takerTimes.machineToolId).WorkingTime;
                var workTime = workingTimes.Find(t => t.machineToolId == takerTimes.machineToolId).times;

                if (end_time + workTime < checkTime + workTime)
                {
                    checkTime = end_time + workTime;
                    machine = machines[takerTimes.machineToolId];
                }
            }

            return machine;
        }
    }
}
