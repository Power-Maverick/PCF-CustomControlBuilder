using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maverick.PCF.Builder.Extensions
{
    public static class BindingSourceExtensions
    {
        public static DataTable DataTable(this BindingSource pBindingSource)
        {
            return (DataTable)pBindingSource.DataSource;
        }
        public static DataRow CurrentRow(this BindingSource pBindingSource)
        {
            return ((DataRowView)pBindingSource.Current).Row;
        }
    }
}
