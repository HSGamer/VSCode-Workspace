using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestExcel.Helper
{
    public static class ExcelHelper
    {
        public static ExcelWorksheet FillData<TEntity>(this ExcelWorksheet worksheet, IEnumerable<TEntity> list, Action<TEntity, IList<ExcelRangeBase>> action, int startRow, int startCol, int range)
        {
            int row = startRow;
            foreach (var entity in list)
            {
                List<ExcelRangeBase> cellsList = new();
                for (int col = startCol; col < startRow + range; col++)
                {
                    cellsList.Add(worksheet.Cells[row, col]);
                }
                action.Invoke(entity, cellsList);
                row++;
            }
            return worksheet;
        }
    }
}
