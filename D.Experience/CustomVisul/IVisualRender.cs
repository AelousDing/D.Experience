using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace D.Experience.CustomVisul
{
    public interface IVisualRender
    {
        void OnRender(Brush brush, Pen pen);
    }
}
