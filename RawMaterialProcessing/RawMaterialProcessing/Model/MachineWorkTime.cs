using System;

namespace RawMaterialProcessing.Model
{
    class MachineWorkTime : IComparable<MachineWorkTime>
    {
        public MachineTools Machine { get; private set; }
        public int WorkingTime { get; set; }

        public MachineWorkTime(MachineTools machine)
        {
            this.Machine = machine;
            WorkingTime = 0;
        }

        public int CompareTo(MachineWorkTime other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return WorkingTime.CompareTo(other.WorkingTime);
        }
    }
}
