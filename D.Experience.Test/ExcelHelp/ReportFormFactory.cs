using D.Experience.ExcelHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Experience.Test.ExcelHelp
{
    public class ReportFormFactory<T> : IExportFactory<T>
    {
        public ExportOperation<T> CreateExportOperation()
        {
            return new ReportFormExportOpration<T>();
        }
    }
}
