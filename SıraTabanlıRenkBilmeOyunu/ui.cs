namespace UIManagement
{
    public class UI
    {
        Form form;

        Button guessButton = new Button();
        Button[] gameButtons = new Button[4];
        public UI(Form form)
        {
            this.form = form;

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
    }
}
