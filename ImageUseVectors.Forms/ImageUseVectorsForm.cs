﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zyl.VectorTraits;

namespace ImageUseVectors.Forms {
    public partial class ImageUseVectorsForm : Form {
        public ImageUseVectorsForm() {
            InitializeComponent();
            TraitsOutput.OutputEnvironment(Console.Out);
        }
    }
}
