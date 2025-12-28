using GameManagement;
namespace UIManagement
{
    public class UI
    {
        Form form;
        Game game;

        Button guessButton = new Button();
        Button[] gameButtons = new Button[4];
        public UI(Form form,Game game)
        {
            this.form = form;
            this.game = game;

        }
        public void setGameButtons(PictureBox[] emptySockets)
        {
            PictureBox lastSocket = emptySockets[emptySockets.Length - 1];


            this.guessButton.Width = 100;
            this.guessButton.Height = 50;

            this.guessButton.Left = lastSocket.Left + this.guessButton.Width;
            this.guessButton.Top = lastSocket.Top;

            this.guessButton.Text = "Guess";

            this.form.Controls.Add(this.guessButton);
        }
        public void setGameButtonEvents()
        {
            this.guessButton.Click += this.GuessButton_Click;
        }
        public void GuessButton_Click(object sender,EventArgs e)
        {
            this.game.Guess();
        }
    }
}
