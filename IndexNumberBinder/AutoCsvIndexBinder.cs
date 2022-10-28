using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexNumberBinder
{
    public class AutoCsvIndexBinder
    {
        private readonly int startIndex;

        public AutoCsvIndexBinder(int startIndex)
        {
            this.startIndex = startIndex;
        }
        public void Bind(Type model)
        {
            int i = this.startIndex;
            model.GetType().GetProperties().ToList().ForEach(x =>
            {
                Console.WriteLine($"md.{x.Name} = {getTypeString(x.PropertyType, i)}");
                i++;
            });

        }

        public string getTypeString(Type type, int idx)
        {
            //valueData[{ i}]
            string result;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int32:
                    result = $"Convert.ToInt32(valueData[{idx}]);";
                    break;
                case TypeCode.DateTime:
                    result = $"Convert.ToDateTime(valueData[{idx}]);";
                    break;
                case TypeCode.Double:
                    result = $"Convert.ToDouble(valueData[{idx}]);";
                    break;
                default:
                    result = $"valueData[{idx}];";
                    break;
            }

            return result;
        }
    }


}
