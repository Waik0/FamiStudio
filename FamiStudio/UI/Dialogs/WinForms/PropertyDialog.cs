﻿using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace FamiStudio
{
    public partial class PropertyDialog : Form
    {
        public PropertyPage Properties => propertyPage;

        public PropertyDialog(Point pt, int width, bool leftAlign = false)
        {
            width = (int)(width * Direct2DTheme.DialogScaling);
            
            if (pt.X >= 0 && pt.Y >= 0)
            {
                if (leftAlign)
                    pt.X -= width;

                StartPosition = FormStartPosition.Manual;
                Location = pt;
            }

            InitializeComponent();

            // TODO: Resize Yes/No button depending on DPI.
            string suffix = ""; // Direct2DTheme.DialogScaling >= 2.0f ? "@2x" : "";

            buttonYes.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"FamiStudio.Resources.Yes{suffix}.png"));
            buttonNo.Image  = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"FamiStudio.Resources.No{suffix}.png"));

            Width = width;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;
                p.ExStyle |= 0x2000000; // WS_EX_COMPOSITED
                return p;
            }
        }

        private void PropertyDialog_Shown(object sender, EventArgs e)
        {
            Height = propertyPage.Height + buttonNo.Height + 5;
        }
        
        private void propertyPage_PropertyWantsClose(int idx)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void PropertyDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
