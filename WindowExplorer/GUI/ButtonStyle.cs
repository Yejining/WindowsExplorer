using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowExplorer.GUI
{
    class ButtonStyle : Button
    {
        private static Font _normalFont = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

        private static Color _back = Color.Gray;
        private static Color _border = Color.Black;
        private static Color _activeBorder = Color.Red;
        private static Color _fore = Color.White;

        private static Padding _margin = new Padding(5, 0, 5, 0);
        private static Padding _padding = new Padding(3, 3, 3, 3);

        private static Size _minSize = new Size(100, 30);
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private Screen.Explorer explorer1;
        private bool _active;

        public ButtonStyle() : base()
        {
            base.Font = _normalFont;
            base.BackColor = _border;
            base.ForeColor = _fore;
            base.FlatAppearance.BorderColor = _back;
            base.FlatStyle = FlatStyle.Flat;
            base.Margin = _margin;
            base.Padding = _padding;
            base.MinimumSize = _minSize;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UseVisualStyleBackColor = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _activeBorder;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _border;
        }

        public void SetStateActive()
        {
            _active = true;
            base.FlatAppearance.BorderColor = _activeBorder;
        }

        public void SetStateNormal()
        {
            _active = false;
            base.FlatAppearance.BorderColor = _border;
        }

        private void InitializeComponent()
        {
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.explorer1 = new WindowExplorer.Screen.Explorer();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.elementHost1_ChildChanged);
            this.elementHost1.Child = this.explorer1;
            this.ResumeLayout(false);

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
