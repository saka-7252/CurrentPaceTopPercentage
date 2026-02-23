using System.Windows.Forms;

namespace LiveSplit.PercentileTracker
{
    public class PercentileSettings : UserControl
    {
        public DualPercentileComponent.DisplayMode Mode { get; set; }
        public int Decimals { get; set; }

        private ComboBox cmbMode;
        private NumericUpDown nmDecimals;

        public PercentileSettings()
        {
            var tableLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(10) };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

            var label1 = new Label { Text = "Display Mode:", Anchor = AnchorStyles.Left, AutoSize = true };
            cmbMode = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbMode.Items.AddRange(new object[] { "Global (Includes Resets)", "Reach (Doesn't include Resets)", "Both (Global / Reach)" });
            cmbMode.SelectedIndexChanged += (s, e) => Mode = (DualPercentileComponent.DisplayMode)cmbMode.SelectedIndex;

            var label2 = new Label { Text = "Decimals:", Anchor = AnchorStyles.Left, AutoSize = true };
            nmDecimals = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 0, Maximum = 5, Value = 2 };
            nmDecimals.ValueChanged += (s, e) => Decimals = (int)nmDecimals.Value;

            tableLayout.Controls.Add(label1, 0, 0);
            tableLayout.Controls.Add(cmbMode, 1, 0);
            tableLayout.Controls.Add(label2, 0, 1);
            tableLayout.Controls.Add(nmDecimals, 1, 1);

            this.Controls.Add(tableLayout);
            this.Size = new System.Drawing.Size(350, 80);
        }

        public void SetSettings(DualPercentileComponent.DisplayMode mode, int decimals)
        {
            Mode = mode;
            Decimals = decimals;
            cmbMode.SelectedIndex = (int)mode;
            nmDecimals.Value = decimals;
        }
    }
}