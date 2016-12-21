using D.Experience.ExcelHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Experience.Test.ExcelHelp
{
    public class LedgerTableFactory<T> : IExportFactory<T>
    {
        public ExportOperation<T> CreateExportOperation()
        {
            return new LedgerTableExportOpration<T>();
        }
    }
}
