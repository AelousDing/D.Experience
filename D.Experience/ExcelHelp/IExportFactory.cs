using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Experience.ExcelHelp
{
    public interface IExportFactory<T>
    {
        ExportOperation<T> CreateExportOperation();
    }
}
