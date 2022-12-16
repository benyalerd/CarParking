using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarParking.Dto
{
    public class PropertyMapHelper
    {
        public static void Map(Type type, DataRow row, PropertyInfo prop, object entity)
        {
            List<string> columnNames = GetDataNames(type, prop.Name);

            foreach (var columnName in columnNames)
            {
                if (!String.IsNullOrWhiteSpace(columnName)
                    && row.Table.Columns.Contains(columnName))
                {
                    var propertyValue = row[columnName];
                    if (propertyValue != DBNull.Value)
                    {
                        ParsePrimitive(prop, entity, row[columnName]);
                        break;
                    }
                }
            }
        }

        public static List<string> GetDataNames(Type type, string propertyName)
        {
            var property = type
                           .GetProperty(propertyName)
                           .GetCustomAttributes(false)
                           .Where(x => x.GetType().Name == "DataNamesAttribute")
                           .FirstOrDefault();

            if (property != null)
            {
                return ((DataNamesAttribute)property).ValueNames;
            }
            return new List<string>();
        }

        private static void ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, value.ToString().Trim(), null);
            }
            else if (prop.PropertyType == typeof(int)
                     || prop.PropertyType == typeof(int?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, int.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(decimal)
                     || prop.PropertyType == typeof(decimal?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, decimal.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(DateTime)
                    || prop.PropertyType == typeof(DateTime?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, DateTime.Parse(value.ToString()), null);
                }
            }

        }
    }
}
