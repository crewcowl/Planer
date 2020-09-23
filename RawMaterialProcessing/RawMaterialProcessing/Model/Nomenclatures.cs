using RawMaterialProcessing.Service;

namespace RawMaterialProcessing.Model
{
    class Nomenclatures : IExcelToClass
    {
        public int id { get; private set; }
        public string name { get; private set; }

        public Nomenclatures() { }

        public Nomenclatures(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public void GetData(string[] data)
        {
            this.id = int.Parse(data[0]);
            this.name = data[1];
        }

    }
}
