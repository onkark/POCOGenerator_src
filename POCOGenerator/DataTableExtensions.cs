using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace POCOGenerator
{
    public static class DataTableExtensions
    {
        public static IEnumerable<T> Cast<T>(this DataTable table) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); });
        }

        public static IEnumerable<T> Cast<T>(this DataTable table, Func<T> instanceHandler)
        {
            return ToEnumerable<T>(table, instanceHandler);
        }

        public static T[] ToArray<T>(this DataTable table) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); });
        }

        public static T[] ToArray<T>(this DataTable table, Func<T> instanceHandler)
        {
            return ToEnumerable<T>(table, instanceHandler);
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); }).ToList<T>();
        }

        public static List<T> ToList<T>(this DataTable table, Func<T> instanceHandler)
        {
            return ToEnumerable<T>(table, instanceHandler).ToList<T>();
        }

        private static T[] ToEnumerable<T>(DataTable table, Func<T> instanceHandler)
        {
            if (table == null)
                return null;

            if (table.Rows.Count == 0)
                return new T[0];

            Type type = typeof(T);

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .Where(c => table.Columns.Contains(c.ColumnName)); // columns exists

            T[] instances = new T[table.Rows.Count];

            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                T instance = instanceHandler();

                foreach (var column in columns)
                {
                    if (column.IsField)
                        ((FieldInfo)column.MemberInfo).SetValue(instance, (row[column.ColumnName] is DBNull ? (object)null : row[column.ColumnName]));
                    else
                        ((PropertyInfo)column.MemberInfo).SetValue(instance, (row[column.ColumnName] is DBNull ? (object)null : row[column.ColumnName]), null);
                }

                instances[index++] = instance;
            }

            return instances;
        }
    }
}
