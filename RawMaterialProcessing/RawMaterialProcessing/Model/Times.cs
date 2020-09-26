using RawMaterialProcessing.Service;
using System;

namespace RawMaterialProcessing.Model
{
    class Times : IExcelToClass, IComparable<Times>
    {
        public int machineToolId { get; private set; }

        public int nomenclaturesId { get; private set; }

        public int times { get; private set; }
        public Times() { }

        public Times(int machineToolId, int nomenclaturesId, int times)
        {
            this.machineToolId = machineToolId;
            this.nomenclaturesId = nomenclaturesId;
            this.times = times;
        }

        public void SetData(string[] data)
        {
            this.machineToolId = int.Parse(data[0]);

            this.nomenclaturesId = int.Parse(data[1]);

            this.times = int.Parse(data[2]);
        }

        public int CompareTo(Times other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var nomenclaturesIdComparison = nomenclaturesId.CompareTo(other.nomenclaturesId);
            if (nomenclaturesIdComparison != 0) return nomenclaturesIdComparison;
            return times.CompareTo(other.times);
        }
    }
}
