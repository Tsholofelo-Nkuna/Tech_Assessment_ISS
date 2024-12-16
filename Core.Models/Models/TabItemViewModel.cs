using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Models.Models
{
    public class TabItemViewModel
    {
        private bool _active = false;
        public string Title { get; set; }
        public string Id { get; init; }
        public bool Active { 
            get => this._active;
            set
            {
                this._active = value;
                if (this._active)
                {
                    this.ActiveClass = "active";
                }
                else
                {
                    this.ActiveClass = string.Empty;
                }
            }
        }
        public string ActiveClass { get; set; } = string.Empty;
        public TabItemViewModel(string title, string id)
        {
            Title = title; 
            Id = id;
        }
        

    }
}
