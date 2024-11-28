using Core.Presentation.Models.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.ViewComponents.Components
{
    public partial class ModalComponent
    {
        [Parameter]
        public ModalViewModel ViewModel { get; set; } = new ModalViewModel();
        [Parameter] public RenderFragment? Body { get; set; }
        [Parameter] public RenderFragment? Footer { get; set; }

    }
}
