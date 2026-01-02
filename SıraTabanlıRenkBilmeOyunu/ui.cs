using GameManagement;
using SQLManagement;
using LabelManagement;
using TextureManagement;
using ScoreManagement;
using System.Security.Cryptography.X509Certificates;
namespace UIManagement
{
    public class UI
    {
        Form form;
        Game game;
        SQL sql;
        Text label;
        Texture texture;
        Score score;
        Button guessButton;
        Button[] gameButtons;
        Button addButton;
        Button playAgainButton;


        TextBox name;
        public UI(Form form,Game game)
        {
            this.form = form;
            this.game = game;
            this.sql = this.game.sql;
            this.label = this.game.text;
            this.texture = this.game.texture;
            this.score = this.game.score;
            this.initUI();
        }
        public void initUI()
        {
            this.guessButton = new Button();
            this.gameButtons = new Button[4];
            this.addButton = new Button();
            this.playAgainButton = new Button();
            this.name = new TextBox();
        }
        public void setGameButtons(PictureBox[] emptySockets)
        {
            PictureBox lastSocket = emptySockets[emptySockets.Length - 1];


            this.guessButton.Width = 100;
            this.guessButton.Height = 50;

            this.guessButton.Left = lastSocket.Left + this.guessButton.Width;
            this.guessButton.Top = lastSocket.Top;

            this.guessButton.Text = "Tahmin Et";

            this.form.Controls.Add(this.guessButton);
        }
        public void setWinUI()
        {
            this.addButton.Width = 100;
            this.addButton.Height = 50;

            this.addButton.Left = this.form.Width / 2 - this.addButton.Width / 2 - 50;
            this.addButton.Top = this.form.Height / 2 - this.addButton.Height / 2;

            this.addButton.Text = "Puanı Kaydet";
            this.form.Controls.Add(this.addButton);


            this.playAgainButton.Width = 100;
            this.playAgainButton.Height = 50;

            this.playAgainButton.Left = this.form.Width / 2 - this.playAgainButton.Width / 2 + 50;
            this.playAgainButton.Top = this.form.Height / 2 - this.playAgainButton.Height / 2;

            this.playAgainButton.Text = "Tekrar Oyna";
            this.form.Controls.Add(this.playAgainButton);

            this.name.Width = 200;
            this.name.Height = 30;
            this.name.Left = this.addButton.Left;
            this.name.Top = this.addButton.Top - 50;
            this.form.Controls.Add(this.name);
        }
        public void setPlayAgainButton()
        {
            this.playAgainButton.Width = 100;
            this.playAgainButton.Height = 50;

            this.playAgainButton.Left = this.form.Width / 2 - this.playAgainButton.Width / 2;
            this.playAgainButton.Top = this.form.Height / 2 - this.playAgainButton.Height / 2;

            this.playAgainButton.Text = "Tekrar Oyna";
            this.form.Controls.Add(this.playAgainButton);

        }
        public void visibleButton()
        {
            if (this.playAgainButton.Visible = true) this.playAgainButton.Visible = !this.playAgainButton.Visible;
            if (this.addButton.Visible = true) this.addButton.Visible = !this.addButton.Visible;
        }
        public void setGameButtonEvents()
        {
            this.guessButton.Click += this.GuessButton_Click;
            this.addButton.Click += this.AddButton_Click;
            this.playAgainButton.Click += this.PlayAgainButton_Click;
        }
        public void GuessButton_Click(object sender,EventArgs e)
        {
            this.game.Guess();
        }
        public void AddButton_Click(object sender,EventArgs e)
        {
            this.sql.InsertData(this.name.Text, this.score.gameScore);

            this.form.Controls.Remove(this.addButton);
        }
        public void PlayAgainButton_Click(object sender,EventArgs e)
        {
            this.form.Controls.Clear();
            this.game.startGame();
        }
    }
}
