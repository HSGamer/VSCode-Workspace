using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace TestExcel.Helper
{
    public static class ExcelHelper
    {
        public static ExcelWorksheet FillDataToCells<TEntity>(this ExcelWorksheet worksheet, IEnumerable<TEntity> list, Action<TEntity, IList<ExcelRangeBase>> action, int startRow, int startCol, int range)
        {
            return FillDataToRange(worksheet, list, (entity, range) => {
                List<ExcelRangeBase> cellsList = new();
                int row = range.Start.Row;
                int startCol = range.Start.Column;
                int endCol = range.End.Column;
                for (int col = startCol; col <= endCol; col++)
                    cellsList.Add(worksheet.Cells[row, col]);
                action.Invoke(entity, cellsList);
            }, startRow, startCol, range);
        }

        public static ExcelWorksheet FillDataToRange<TEntity>(this ExcelWorksheet worksheet, IEnumerable<TEntity> list, Action<TEntity, ExcelRangeBase> action, int startRow, int startCol, int range)
        {
            int row = startRow;
            foreach (var entity in list)
            {
                action.Invoke(entity, worksheet.Cells[row, startCol, row, startCol + (range - 1)]);
                row++;
            }
            return worksheet;
        }
    }
}
