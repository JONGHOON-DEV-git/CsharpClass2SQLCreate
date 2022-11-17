using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexNumberBinder
{
    public class AutoCsvIndexBinder
    {
        private readonly int startIdx;

        public AutoCsvIndexBinder(int startIdx)
        {
            this.startIdx = startIdx;
        }

        public void Bind(object model)
        {
            int i = this.startIdx;
            model.GetType().GetProperties().ToList().ForEach(x =>
            {
                Console.WriteLine($"md.{x.Name} = {getTypeString(x.PropertyType, i)}");
                i++;
            });
        }


        public void GetTableColumns(string tableName, object model)
        {
            string createTableText = $@"CREATE TABLE {tableName}({Environment.NewLine}";
            model.GetType().GetProperties().ToList().ForEach(x =>
            {
                string addon = $"[{x.Name}] {getTypeSqlColumn(x.PropertyType)},";
                createTableText += addon + Environment.NewLine;
            });
            createTableText += $"{Environment.NewLine})";

            Console.WriteLine(createTableText);
        }

        private string getTypeSqlColumn(Type propertyType)
        {
            //NullableType Check
            if (propertyType.GenericTypeArguments.Length > 0)
            {
                propertyType = propertyType.GenericTypeArguments.First();
            }

            string result;
            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.Int32:
                    result = $"INT";
                    break;
                case TypeCode.DateTime:
                    result = $"DATETIME";
                    break;
                case TypeCode.Double:
                    result = $"DECIMAL(13,5)";
                    break;
                default:
                    result = $"VARCHAR(100)";
                    break;
            }
            return result;
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
                    result = $"valueData[{idx}].EmptyNullToZero();";
                    break;
                default:
                    result = $"valueData[{idx}];";
                    break;
            }

            return result;
        }
    }


}
