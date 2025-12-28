using TextureManagement;
using UIManagement;
using LabelManagement;
namespace GameManagement
{
    public class Game
    {
        Form form;
        Texture texture;
        Text text;
        UI ui;

        Random random = new Random();

        PictureBox[] emptySocket = new PictureBox[4];

        int trueCount;
        int falseCount;
        int guessCount;
        string[] gameColors = {"red", "green", "blue", "yellow"};


        int suffleCount = 100;
        public Game(Form form)
        {
            this.form = form;
            this.texture = new Texture(this.form);
            this.text = new Text(this.form);
            this.ui = new UI(this.form,this);


            this.guessCount = 4;
            this.texture.guessCount = this.guessCount;

            startGame();
        }
        public void startGame()
        {
            this.texture.setGameTextures();
            this.texture.setGameTextureEvents();
            this.texture.setGuessTextures();
            this.ui.setGameButtons(this.texture.emptySocket);
            this.ui.setGameButtonEvents();
            this.suffle();
        }
        public void suffle()
        {

            string store;

            for (int i=0;i<this.suffleCount;i++)
            {
                int x1 = random.Next(0, this.gameColors.Length);
                int x2 = random.Next(0, this.gameColors.Length);

                store = this.gameColors[x1];
                this.gameColors[x1] = this.gameColors[x2];
                this.gameColors[x2] = store;
            }
        }

        public void Guess()
        {
            this.trueCount = 0;
            this.falseCount = 0;
            this.emptySocket = this.texture.emptySocket;


            for (int i=0;i<this.gameColors.Length;i++)
            {
                if (this.gameColors[i] == this.emptySocket[i].Tag.ToString()) this.trueCount++;
                else this.falseCount++;

            }

            this.text.guessText(this.trueCount,this.falseCount);
            this.texture.showGuess(this.text.guessLabel);


            this.guessCount--;
            this.texture.guessCount = this.guessCount;


            if (this.trueCount == this.gameColors.Length) this.Won();

            if (this.guessCount == 0) this.Lost();
            
        }
        public void Won()
        {
            MessageBox.Show("kazandýn");
        }
        public void Wrong()
        {
            //xxxx//
        }
        public void Lost()
        {
            MessageBox.Show("kaybettin");
        }
    }
}