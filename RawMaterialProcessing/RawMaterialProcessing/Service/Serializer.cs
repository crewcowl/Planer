using System.Collections.Generic;
using System.IO;

namespace RawMaterialProcessing.Service
{
    static class Serializer<T> where T: IExcelToClass, new()
    {

        public static List<T> GetObjectFromXlsString(string path) {
            List<T> objList = new List<T>();
            if (System.IO.File.Exists(path))
            {
                Excel excel = new Excel();
                if (excel.OpenWorksheet(Path.GetFullPath(path)))
                {
                    List<string> data = new List<string>();
                    data = excel.Read();

                    foreach (var dt in data)
                    {
                        T classObj2 = new T();
                        classObj2.GetData(dt.Split(';'));
                        objList.Add(classObj2);
                    }
                }
                excel.close(Path.GetFullPath(path));
            }
            return objList;
        }

    }
}
