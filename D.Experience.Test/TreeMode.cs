using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace D.Experience.Test
{
    public class TreeMode : System.ComponentModel.INotifyPropertyChanged
    {
        private string _name;
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }
        private List<TreeMode> _children = new List<TreeMode>();
        /// <summary>
        /// 
        /// </summary>
        public List<TreeMode> Children
        {
            get { return _children; }
            set
            {
                if (value != _children)
                {
                    _children = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Children"));
                    }
                }
            }
        }
        private string _code;
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value != _code)
                {
                    _code = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Children"));
                    }
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<TreeMode> CreateModels()
        {
            ObservableCollection<TreeMode> models = new ObservableCollection<TreeMode>();
            TreeMode model0 = new TreeMode();
            model0.Name = "一级节点0";
            model0.Code = "1-0";
            TreeMode model00 = new TreeMode() { Name = "二级节点00" };
            model0.Code = "2-0";
            model0.Children.Add(model00);

            TreeMode model000 = new TreeMode() { Name = "三级节点000" };
            model000.Code = "3-0";
            model00.Children.Add(model000);
            TreeMode model001 = new TreeMode() { Name = "三级节点001" };
            model001.Code = "3-1";
            model00.Children.Add(model001);

            TreeMode model01 = new TreeMode() { Name = "二级节点01" };
            model01.Code = "2-1";
            model0.Children.Add(model01);
            TreeMode model1 = new TreeMode() { Name = "一级节点1" };
            model1.Code = "1-0";
            TreeMode model10 = new TreeMode() { Name = "二级节点10" };
            model10.Code = "1-10";
            model1.Children.Add(model10);
            TreeMode model11 = new TreeMode() { Name = "二级节点11" };
            model11.Code = "1-11";
            model1.Children.Add(model11);

            TreeMode model2 = new TreeMode() { Name = "一级节点2" };
            model2.Code = "2-0";


            models.Add(model0);
            models.Add(model1);
            models.Add(model2);
            return models;
        }
    }
}
