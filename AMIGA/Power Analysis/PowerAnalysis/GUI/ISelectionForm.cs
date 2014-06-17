﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.GUI {
    public interface ISelectionForm {

        string Name { get; set; }

        string Description { get; }

        void Activate();

        bool IsVisible();
    }
}
