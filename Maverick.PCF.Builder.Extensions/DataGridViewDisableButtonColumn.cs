using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Extensions
{
    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            CellTemplate = new DataGridViewDisableButtonCell();
        }
    }


    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool mEnabledValue;
        public bool Enabled
        {
            get
            {
                return mEnabledValue;
            }
            set
            {
                mEnabledValue = value;
            }
        }

        private bool mShowDisabledText;
        public bool ShowDisabledText
        {
            get
            {
                return mShowDisabledText;
            }
            set
            {
                mShowDisabledText = value;
            }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewDisableButtonCell Cell = (DataGridViewDisableButtonCell)base.Clone();
            Cell.Enabled = this.Enabled;
            return Cell;
        }

        // By default, enable the button cell.
        public DataGridViewDisableButtonCell()
        {
            this.mEnabledValue = true;
            this.mShowDisabledText = false;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            // The button cell is disabled, so paint the border, background, and disabled button for the cell.
            if (!this.mEnabledValue)
            {
                // Draw the background of the cell, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                // Calculate the area in which to draw the button.
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                if (ShowDisabledText)
                {
                    // Draw the disabled button.                
                    ButtonRenderer.DrawButton(graphics, buttonArea, System.Windows.Forms.VisualStyles.PushButtonState.Disabled);

                    // Draw the disabled button text. 
                    if (this.FormattedValue is string)
                    {
                        TextRenderer.DrawText(graphics, Convert.ToString(this.FormattedValue), this.DataGridView.Font, buttonArea, SystemColors.GrayText);
                    }
                }

            }
            else
            {
                // The button cell is enabled, so let the base class 
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            }
        }
    }
}
