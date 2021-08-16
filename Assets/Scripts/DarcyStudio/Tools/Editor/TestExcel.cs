/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 10 August 2021
 * Time: 18:46:56
 * Description:
 * 
 ***/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DarcyStudio.GameComponent.Tools;
using ExcelDataReader;
// using ExcelLibrary.SpreadSheet;
// #if UNITY_EDITOR
// using OfficeOpenXml;
// #endif
using UnityEngine;


namespace DarcyStudio.Tools
{
    public static class TestExcel
    {

        // public static void SaveToExcel<T> (this List<T> current, string excel_name = "", string sheet_name = "",
        //     string                                      path = "") where T : ExcelData, new ()
        // {
        //     ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //
        //     /// excel_name为空时采用T类型名作为excel文件名，sheet_name同理
        //     excel_name = string.IsNullOrEmpty (excel_name) ? typeof (T).ToString () : excel_name;
        //     sheet_name = string.IsNullOrEmpty (sheet_name) ? typeof (T).ToString () : sheet_name;
        //
        //     /// 获取文件信息 PathConst.ExcelFilePath(excel_name) 为文件地址
        //
        //     if (!File.Exists (path))
        //     {
        //         File.Create (path);
        //     }
        //     
        //     var fileInfo = new FileInfo (path);
        //
        //     using (var package = new ExcelPackage (fileInfo))
        //     {
        //         if (package.Workbook.Worksheets[sheet_name] != null)
        //         {
        //             package.Workbook.Worksheets.Delete (sheet_name);
        //         }
        //
        //         var workSheet = package.Workbook.Worksheets.Add (sheet_name);
        //
        //         var fields = typeof (T).GetFields ();
        //
        //         /// 字段名
        //         for (int titleId = 0; titleId < fields.Length; titleId++)
        //         {
        //             var attribs = fields[titleId].GetCustomAttributes (typeof (DescriptionAttribute), false);
        //             workSheet.Cells[1, titleId + 1].Value = attribs.Length > 0
        //                 ? ((DescriptionAttribute) attribs[0]).Description
        //                 : fields[titleId].Name;
        //         }
        //
        //         /// 内容
        //         for (int i = 0; i < current.Count; i++)
        //         {
        //             for (int j = 0; j < fields.Length; j++)
        //             {
        //                 workSheet.Cells[i + 2, j + 1].Value = fields[j].GetValue (current[i]);
        //             }
        //         }
        //
        //         package.Save ();
        //         Log.Error (string.Format ("{0}_{1}:写入成功!", excel_name, sheet_name));
        //     }
        // }

        public static List<T> ReadFromExcel<T> (this List<T> current, string excel_name = "", string sheet_name = "",
            string                                           path = "")
            where T : ExcelData, new ()
        {
            excel_name = string.IsNullOrEmpty (excel_name) ? typeof (T).ToString () : excel_name;
            sheet_name = string.IsNullOrEmpty (sheet_name) ? typeof (T).ToString () : sheet_name;

            if (!File.Exists (path))
            {
                // current.SaveToExcel<T> ();
                return current;
            }

            using (var fs = File.Open (path, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader (fs))
                {
                    var table = excelReader.AsDataSet ().Tables[sheet_name];
                    if (table == null)
                        return current;
                    var fields      = typeof (T).GetFields ();
                    int rowCount    = table.Rows.Count;
                    int columnCount = table.Columns.Count;
                    /// 第一行为变量名称
                    var variableNameList = new List<string> ();
                    for (int i = 0; i < columnCount; i++)
                        variableNameList.Add (table.Rows[0][i].ToString ());
                    for (int i = 1; i < rowCount; i++)
                    {
                        var item = new T ();
                        var row  = table.Rows[i];
                        for (int j = 0; j < fields.Length; j++)
                        {
                            var field = fields[j];
                            var index = variableNameList.IndexOf (field.Name);
                            if (index < 0)
                                Log.Error (string.Format ("Excel表格{0}中，无法找到{1}字段", typeof (T).ToString (),
                                    field.Name));
                            else
                                field.SetValue (item, Convert.ChangeType (row[j], field.FieldType));
                        }

                        current.Add (item);
                    }
                }

                Log.Error (string.Format ("{0}_{1}:读出成功!", excel_name, sheet_name));
            }

            return current;
        }


        // public static void SaveFile<T> (this List<T> current, string sheetName, string filePath)
        //     where T : ExcelData, new ()
        // {
        //     if (!File.Exists (filePath))
        //     {
        //         File.Create (filePath);
        //     }
        //
        //     var file      = filePath;
        //     var workbook  = new Workbook ();
        //     var workSheet = new Worksheet (sheetName);
        //     workbook.Worksheets.Add (workSheet);
        //
        //
        //     var fields = typeof (T).GetFields ();
        //
        //     /// 字段名
        //     for (int titleId = 0; titleId < fields.Length; titleId++)
        //     {
        //         var attribs = fields[titleId].GetCustomAttributes (typeof (DescriptionAttribute), false);
        //         workSheet.Cells[1, titleId + 1] = new Cell (attribs.Length > 0
        //             ? ((DescriptionAttribute) attribs[0]).Description
        //             : fields[titleId].Name);
        //         // workSheet.Cells[1, titleId + 1].Value = attribs.Length > 0
        //         // ? ((DescriptionAttribute) attribs[0]).Description
        //         // : fields[titleId].Name;
        //     }
        //
        //     /// 内容
        //     for (int i = 0; i < current.Count; i++)
        //     {
        //         for (int j = 0; j < fields.Length; j++)
        //         {
        //             workSheet.Cells[i + 2, j + 1] = new Cell (fields[j].GetValue (current[i]));
        //         }
        //     }

        // worksheet.Cells[0, 1]             = new Cell ((short) 1);
        // worksheet.Cells[2, 0]             = new Cell (9999999);
        // worksheet.Cells[3, 3]             = new Cell ((decimal) 3.45);
        // worksheet.Cells[2, 2]             = new Cell ("Text string");
        // worksheet.Cells[2, 4]             = new Cell ("Second string");
        // worksheet.Cells[4, 0]             = new Cell (32764.5,      "#,##0.00");
        // worksheet.Cells[5, 1]             = new Cell (DateTime.Now, @"YYYY-MM-DD");
        // worksheet.Cells.ColumnWidth[0, 1] = 3000;
        //     workbook.Save (file);
        // }


        public static List<TestData> TestRead ()
        {
            var result = new List<TestData> ();
            return result.ReadFromExcel ("", "1",
                Path.Combine (Application.streamingAssetsPath, "test.xlsm"));
        }


        public static void TestWrite ()
        {
            var source = new List<TestData> ();
            for (var i = 0; i < 10; i++)
            {
                source.Add (new TestData (i, "name_" + i, DateTime.Now.ToString (CultureInfo.InvariantCulture)));
            }

            var path = Path.Combine (Application.streamingAssetsPath, "test2.xlsm");
            // source.SaveFile ("1", path);
        }


    }
}