using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Extensions
{
    public static class DataGridViewButtonExtensions
    {
        public static bool IsValidDataGridViewButton(this DataGridView pDataGridView, string pCellName, string pPostFix = "Column")
        {
            bool Result = false;

            if (pDataGridView.CurrentCell.Value.ToString() == pCellName.Replace(pPostFix, ""))
            {
                if (((DataGridViewDisableButtonCell)pDataGridView.Rows[pDataGridView.CurrentRow.Index].Cells[pCellName]).Enabled)
                {
                    Result = true;
                }
            }

            return Result;

        }
        /// <summary>
        /// Determine if we are dealing with a button column
        /// </summary>
        /// <param name="pDataGridView"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsHeaderButtonCell(this DataGridView pDataGridView, DataGridViewCellEventArgs e)
        {
            return pDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && !(e.RowIndex == -1);
        }
        public static void AdjustButtons(this DataGridView pDataGridView, string pCellName = "Details")
        {
            for (int i = 0; i < pDataGridView.RowCount; i++)
            {
                pDataGridView.Rows[i].Cells[pCellName].Value = pCellName;
                ((DataGridViewDisableButtonCell)(pDataGridView.Rows[i].Cells[pCellName])).Enabled = false;
            }

            ((DataGridViewDisableButtonCell)(pDataGridView.Rows[pDataGridView.CurrentRow.Index]
                .Cells[pCellName])).Enabled = true;

        }
        public static void CreateUnboundButtonColumn(this DataGridView pDataGridView, string pColumnName, string pColumnText, string pHeaderText, int pWith = 60)
        {

            DataGridViewDisableButtonColumn Column = new DataGridViewDisableButtonColumn
            {
                HeaderText = pHeaderText,
                Name = pColumnName,
                Text = pColumnText,
                Width = pWith,
                UseColumnTextForButtonValue = true
            };

            pDataGridView.Columns.Insert(pDataGridView.ColumnCount, Column);

        }
    }
}
