using RawMaterialProcessing.Service;
using System;

namespace RawMaterialProcessing.Model
{
    class Parties : IExcelToClass, IComparable<Parties>
    {
        public int id { get; private set; }

        public int nomenclaturesId { get; private set; }
        public Parties() { }

        public Parties(int id, int nomenclaturesId)
        {
            this.id = id;
            this.nomenclaturesId = nomenclaturesId;
        }

        public void SetData(string[] data)
        {
            this.id = int.Parse(data[0]);
            this.nomenclaturesId = int.Parse(data[1]);
        }

       
        public int CompareTo(Parties other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = nomenclaturesId.CompareTo(other.nomenclaturesId);
            if (idComparison != 0) return idComparison;
            return id.CompareTo(other.id);
        }
    }
}
