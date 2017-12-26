using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace DailyNotes.Models
{
    public static class BaseClass
    {
        ///<summary> 
        /// 加密
        /// </summary>
        public static string EncodeCode(string str)
        {
            string key = "david.yc";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString().Trim();
        }
        /// <summary>
        /// Des 解密 GB2312 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecodeCode(string str)
        {
            string key = "david.yc";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.GetEncoding("GB2312").GetString(stream.ToArray()).Trim();
        }
        /// <summary>
        /// 把List`T转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">源数据列表</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable();

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T obj in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(obj, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        /// <summary>
        /// 把DataTable转换为List`T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">源数据表</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            var dataColumn = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

            var properties = typeof(T).GetProperties();
            string columnName;

            return dt.AsEnumerable().Select(row =>
            {
                var t = Activator.CreateInstance<T>();
                foreach (var p in properties)
                {
                    columnName = p.Name;
                    if (dataColumn.Contains(columnName))
                    {
                        if (!p.CanWrite)
                            continue;

                        object value = row[columnName];
                        Type type = p.PropertyType;

                        if (value != DBNull.Value)
                        {
                            p.SetValue(t, Convert.ChangeType(value, type), null);
                        }
                    }
                }
                return t;
            }).ToList();
        }
        /// <summary>
        /// 读取excel转为DataTable
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">指定sheet</param>
        /// <param name="isColumnName">第一行是否为列名</param>
        /// <param name="startRow">从第几行开始</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isColumnName, int startRow = 0)
        {
            IWorkbook workBook = null;
            ISheet sheet = null;
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                //低于2007版本
                if (Path.GetExtension(fileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(fs);
                }
                //2007及以上版本
                else if (Path.GetExtension(fileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(fs);
                }
            }
            //判断是否指定sheet上传
            if (sheetName != null)
            {
                //获取指定sheet
                sheet = workBook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //获取不到时取第一个sheet
                    sheet = workBook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workBook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                //sheet中第一行
                IRow firstRow = sheet.GetRow(0);
                if (firstRow == null)
                {
                    throw new Exception("首行无数据");
                }

                //遍历第一行的单元格
                for (int i = firstRow.FirstCellNum; i < firstRow.LastCellNum; i++)
                {
                    //得到列名
                    ICell cell = firstRow.GetCell(i);
                    if (cell != null)
                    {
                        //得到列名的值,若列名不是字符则不能使用StringCellValue，最好使用ToString()
                        string cellValue = cell.ToString();
                        if (cellValue != null)
                        {
                            try
                            {
                                //判断第一行是否是列名
                                if (isColumnName)
                                {
                                    //将列放入datatable中
                                    DataColumn column = new DataColumn(cellValue);
                                    dt.Columns.Add(column);
                                }
                                else
                                {
                                    //将空列放入datatable中
                                    DataColumn column = new DataColumn();
                                    dt.Columns.Add(column);
                                }
                            }
                            catch
                            {
                                throw new Exception("列名有误！");
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                }
                //遍历所有行
                for (int i = startRow; i <= sheet.LastRowNum; i++)
                {
                    //得到i行
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    //datatable新增行
                    DataRow dr = dt.NewRow();
                    //遍历i行的单元格
                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dr[j] = row.GetCell(j).ToString();
                        }
                    }
                    try
                    {
                        //将行放入datatable中
                        dt.Rows.Add(dr);
                    }
                    catch
                    {
                        throw new Exception("第" + i + "行有误！");
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="fileName">导出文件的路径</param>
        /// <param name="templetName">导出模板路径</param>
        /// <param name="dt">DataTable</param>
        /// <param name="titleName">文件标题</param>
        /// <param name="sheetName">文件sheet名称</param>
        public static void DataTableToExcel(string fileName, string templetName, DataTable dt, string titleName, string sheetName)
        {
            FileStream fs1 = new FileStream(templetName, FileMode.Open, FileAccess.Read);
            IWorkbook workBook = new HSSFWorkbook(fs1);
            ISheet sheet = workBook.GetSheet(sheetName);

            //第一行
            IRow row0 = sheet.GetRow(0);
            ICell cellTitle = row0.GetCell(0);
            cellTitle.SetCellValue(titleName);
            //第二行
            IRow row1 = sheet.GetRow(1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.GetCell(j);
                cell.SetCellValue(dt.Columns[j].ColumnName);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow rowi = sheet.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //创建单元格
                    ICell cell = rowi.CreateCell(j);
                    //给单元格赋值
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                    cell.CellStyle.BorderBottom = BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = BorderStyle.Thin;
                }
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workBook.Write(fs);
            }
        }
    }
}