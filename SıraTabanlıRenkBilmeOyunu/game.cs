using TextureManagement;
using UIManagement;
using LabelManagement;
using SQLManagement;
using ScoreManagement;
namespace GameManagement
{
    public class Game
    {
        Form form;
        public Texture texture;
        public Text text;
        public UI ui;
        public SQL sql;
        public Score score;

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
            this.sql = new SQL(this.form);
            this.score = new Score(this.form);
            this.ui = new UI(this.form,this);

            startGame();
        }
        public void startGame()
        {
            this.guessCount = this.texture.guessLength;
            this.texture.guessCount = this.guessCount;

            this.sql.initSQL();
            this.texture.initTexture();
            this.ui.initUI();
            this.score.initScore();

            this.texture.setGameTextures();
            this.texture.setGameTextureEvents();
            this.texture.setGuessTextures();
            this.ui.setGameButtons(this.texture.emptySocket);
            this.ui.setGameButtonEvents();
            this.suffle();
            this.sql.ShowData();


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
            //bütün boþ kutular dolu mu diye kontrol ediyor
            for(int i=0;i<this.texture.emptySocket.Length;i++)
            {
                if (this.texture.emptySocket[i].Tag == "empty") return;
            }



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
            this.texture.deleteSockets();

            this.guessCount--;
            this.texture.guessCount = this.guessCount;


            if (this.trueCount == this.gameColors.Length) { 
                this.Won();
                return;
            }

            if (this.guessCount == 0) this.Lost();
            
        }
        public void Won()
        {

            MessageBox.Show("kazandýn");
            this.score.CalculateScore(this.guessCount, this.texture.guessLength);
            this.ui.setWinUI();
            
        }
        public void Wrong()
        {
            //xxxx//
        }
        public void Lost()
        {
            MessageBox.Show("kaybettin");
            this.ui.setPlayAgainButton();
        }
    }
}