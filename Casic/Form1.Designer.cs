namespace Casic
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            this.betAmountTextBox = new System.Windows.Forms.TextBox();
            this.balanceLabel = new System.Windows.Forms.Label();
            this.wheelControl = new Casic.WheelControl();
            this.betSymbolComboBox = new System.Windows.Forms.CheckedListBox();
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
            // betAmountTextBox
            // 
            this.betAmountTextBox.Location = new System.Drawing.Point(50, 50);
            this.betAmountTextBox.Name = "betAmountTextBox";
            this.betAmountTextBox.Size = new System.Drawing.Size(120, 20);
            this.betAmountTextBox.TabIndex = 2;
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
            this.wheelControl.Location = new System.Drawing.Point(200, 20);
            this.wheelControl.Name = "wheelControl";
            this.wheelControl.Size = new System.Drawing.Size(800, 800);
            this.wheelControl.TabIndex = 4;
            // 
            // betSymbolComboBox
            // 
            this.betSymbolComboBox.FormattingEnabled = true;
            this.betSymbolComboBox.Items.AddRange(new object[] {
            "1$", "2$", "5$", "10$", "20$", "Joker"});
            this.betSymbolComboBox.Location = new System.Drawing.Point(50, 80);
            this.betSymbolComboBox.Name = "betSymbolComboBox";
            this.betSymbolComboBox.Size = new System.Drawing.Size(120, 64);
            this.betSymbolComboBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1020, 840);
            this.Controls.Add(this.betSymbolComboBox);
            this.Controls.Add(this.wheelControl);
            this.Controls.Add(this.balanceLabel);
            this.Controls.Add(this.betAmountTextBox);
            this.Controls.Add(this.spinButton);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button spinButton;
        private System.Windows.Forms.TextBox betAmountTextBox;
        private System.Windows.Forms.Label balanceLabel;
        private Casic.WheelControl wheelControl;
        private System.Windows.Forms.CheckedListBox betSymbolComboBox;
    }
}
