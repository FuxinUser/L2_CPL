using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CPL1HMI.comm
{
    public class ExportFile
    {
        private enum Mode
        {
            xls,
            xlsx
        }

        public static void WriteToExcel_xls(DataTable dt, string fileName, string filePath)
        {
            WriteToExcel(dt, fileName, filePath, Mode.xls);
        }

        public static void WriteToExcel_xls(Func<IWorkbook, object, IWorkbook> func, object obj, string samplePath, string folderPath, string fileName)
        {
            WriteToExcel(func, obj, samplePath, folderPath, fileName, Mode.xls);
        }

        public static void WriteToExcel_xlsx(DataTable dt, string fileName, string filePath)
        {
            WriteToExcel(dt, fileName, filePath, Mode.xlsx);
        }

        public static void WriteToExcel_xlsx(Func<IWorkbook, object, IWorkbook> func, object obj, string samplePath, string folderPath, string fileName)
        {
            WriteToExcel(func, obj, samplePath, folderPath, fileName, Mode.xlsx);
        }

        public static bool IsGetFolderPath(out string folderPath)
        {
            folderPath = string.Empty;

            var dialog = new FolderBrowserDialog() { Description = "請選擇路徑" };

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            if (string.IsNullOrEmpty(dialog.SelectedPath))
                return false;

            folderPath = dialog.SelectedPath;
            return true;
        }

        public static ICellStyle CreateCellStyle(IWorkbook wb, short colorIdx)
        {
            var cellStyle = wb.CreateCellStyle();

            cellStyle.FillForegroundColor = colorIdx;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            return cellStyle;
        }

        private static void WriteToExcel(DataTable dt, string fileName, string filePath, Mode mode)
        {
            FileStream file = null;

            try
            {
                var wb = mode == Mode.xls ? new HSSFWorkbook() : new XSSFWorkbook() as IWorkbook;   //  建立活頁簿
                var ws = wb.CreateSheet("Sheet");                                                   //  建立工作表

                //  建置標頭
                ws.CreateRow(0);
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    ws.GetRow(0).CreateCell(i).SetCellValue($"{dt.Columns[i]}");
                }

                //  建置內容
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rowIdx = i + 1;

                    ws.CreateRow(rowIdx);
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.GetRow(rowIdx).CreateCell(j).SetCellValue($"{dt.Rows[i][$"{dt.Columns[j]}"]}");
                    }
                }

                //  完整路徑
                var fullPath = $@"{filePath}\{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.{mode}";

                //  產生檔案
                file = new FileStream($@"{fullPath}", FileMode.Create);

                //  寫入數據
                wb.Write(file);
            }
            catch
            {
                throw;
            }
            finally
            {
                //  若資訊流不為空則關閉
                if (file != null)
                    file.Close();
            }
        }

        private static void WriteToExcel(Func<IWorkbook, object, IWorkbook> func, object obj, string samplePath, string folderPath, string fileName, Mode mode)
        {
            FileStream openFile = null;
            FileStream saveFile = null;

            try
            {
                using (openFile = new FileStream($@"{samplePath}", FileMode.Open, FileAccess.Read))
                {
                    var wb = mode == Mode.xls ? new HSSFWorkbook(openFile) : new XSSFWorkbook(openFile) as IWorkbook;
                    var nWb = func.Invoke(wb, obj);

                    //  產生檔案
                    saveFile = new FileStream($@"{folderPath}\{fileName}_{DateTime.Now:yyyyMMddHHmmss}.{mode}", FileMode.Create);

                    //  寫入數據
                    nWb.Write(saveFile);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //  若資訊流不為空則關閉
                if (openFile != null)
                    openFile.Close();
                if (saveFile != null)
                    saveFile.Close();
            }
        }
    }
}
