namespace TicTacToe
{
    public partial class Form1 : Form
    {

        private bool TurnIndicator { get; set; }
        private readonly TableLayoutPanel tableLayoutPanel;
        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 3 };
            Controls.Add(tableLayoutPanel);
            for (int i = 0; i < 9; i++)
            {
                var button = new Button()
                {
                    Width = 100,
                    Height = 100,
                    Font = new Font(FontFamily.GenericSerif, 20.0f, FontStyle.Bold),
                    Cursor = Cursors.Cross,
                    TabStop = false
                };
                button.Click += new EventHandler(Button_clicked);
                button.MouseEnter += OnMouseEnterButton;
                button.MouseLeave += OnMouseLeaveButton;
                tableLayoutPanel.Controls.Add(button);
            }
        }

        private void OnMouseEnterButton(object? sender, EventArgs e)
        {
            (sender as Button).Text = TurnDecider();
        }


        private void OnMouseLeaveButton(object? sender, EventArgs e)
        {
            if ((sender as Button).Enabled) (sender as Button).Text = string.Empty;
        }

        private string TurnDecider()
        {
            return TurnIndicator ? "O" : "X";
        }

        private bool IsOver()
        {
            string c = "X";
            for (int i = 0; i < 2; i++)
            {
                //Horizontally
                if (tableLayoutPanel.Controls[0].Text == c && tableLayoutPanel.Controls[1].Text == c && tableLayoutPanel.Controls[2].Text == c) { DisableAll(); return true; }
                else if (tableLayoutPanel.Controls[3].Text == c && tableLayoutPanel.Controls[4].Text == c && tableLayoutPanel.Controls[5].Text == c) { DisableAll(); return true; }
                else if (tableLayoutPanel.Controls[6].Text == c && tableLayoutPanel.Controls[7].Text == c && tableLayoutPanel.Controls[8].Text == c) { DisableAll(); return true; }
                //Vertically
                else if (tableLayoutPanel.Controls[0].Text == c && tableLayoutPanel.Controls[3].Text == c && tableLayoutPanel.Controls[6].Text == c) { DisableAll(); return true; }
                else if (tableLayoutPanel.Controls[1].Text == c && tableLayoutPanel.Controls[4].Text == c && tableLayoutPanel.Controls[7].Text == c) { DisableAll(); return true; }
                else if (tableLayoutPanel.Controls[2].Text == c && tableLayoutPanel.Controls[5].Text == c && tableLayoutPanel.Controls[8].Text == c) { DisableAll(); return true; }
                //Diagonally
                else if (tableLayoutPanel.Controls[0].Text == c && tableLayoutPanel.Controls[4].Text == c && tableLayoutPanel.Controls[8].Text == c) { DisableAll(); return true; }
                else if (tableLayoutPanel.Controls[2].Text == c && tableLayoutPanel.Controls[4].Text == c && tableLayoutPanel.Controls[6].Text == c) { DisableAll(); return true; }
                c = "O";
            }
            return false;
        }

        private void DisableAll()
        {
            foreach (Button control in tableLayoutPanel.Controls)
            {
                control.Enabled = false;
            }
        }

        private void Button_clicked(object? sender, EventArgs e)
        {
            (sender as Button).Text = TurnDecider();
            (sender as Button).Enabled = false;
            if (IsOver())
            {
                var result = MessageBox.Show($"{TurnDecider()} won! Congratulations!\nWould you like to play again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result != DialogResult.Yes) { Application.Exit(); }
                StartNewGame();
            }
            TurnIndicator = !TurnIndicator;
        }

        private void StartNewGame()
        {
            foreach (Button control in tableLayoutPanel.Controls)
            {
                control.Enabled = true;
                control.Text = string.Empty;
                TurnIndicator = true;
            }
        }
    }
}