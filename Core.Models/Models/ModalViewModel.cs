using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Models.Models
{
    public class ModalViewModel
    {
        public string DisplayStyle { get; set; } = "none";
        public string ShowhowClass { get; set; } = "show";
        private bool _show = false;
        public string Title { get; set; } = string.Empty;
        public bool Show { 
            get 
            { 
                return this._show;
            } 
            set {
               _show = value;
                if (this._show)
                {
                    this.DisplayStyle = "inline-block";
                    this.ShowhowClass = "show";
                }
                else
                {
                    this.DisplayStyle = "none";
                    this.ShowhowClass = "";
                }
            } 
        }
    }
}
