namespace Casic
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button spinButton;
        private System.Windows.Forms.Label balanceLabel;
        private Casic.WheelControl wheelControl;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.spinButton = new System.Windows.Forms.Button();
            this.balanceLabel = new System.Windows.Forms.Label();
            this.wheelControl = new Casic.WheelControl();
            this.SuspendLayout();
            // 
            // spinButton
            // 
            this.spinButton.Location = new System.Drawing.Point(50, 150);
            this.spinButton.Name = "spinButton";
            this.spinButton.Size = new System.Drawing.Size(120, 30);
            this.spinButton.TabIndex = 1;
            this.spinButton.Text = "Spin";
            this.spinButton.UseVisualStyleBackColor = true;
            this.spinButton.Click += new System.EventHandler(this.spinButton_Click);
            // 
            // balanceLabel
            // 
            this.balanceLabel.AutoSize = true;
            this.balanceLabel.Location = new System.Drawing.Point(50, 20);
            this.balanceLabel.Name = "balanceLabel";
            this.balanceLabel.Size = new System.Drawing.Size(46, 13);
            this.balanceLabel.TabIndex = 3;
            this.balanceLabel.Text = "Balance";
            // 
            // wheelControl
            // 
            this.wheelControl.Location = new System.Drawing.Point(600, 20);
            this.wheelControl.Name = "wheelControl";
            this.wheelControl.Size = new System.Drawing.Size(800, 800);
            this.wheelControl.TabIndex = 4;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1440, 860);
            this.Controls.Add(this.wheelControl);
            this.Controls.Add(this.balanceLabel);
            this.Controls.Add(this.spinButton);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
