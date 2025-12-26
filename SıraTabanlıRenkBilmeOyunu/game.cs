using TextureManagement;
using UIManagement;
namespace GameManagement
{
    public class Game
    {
        Form form;
        Texture texture;
        UI ui;
        public Game(Form form)
        {
            this.form = form;
            this.texture = new Texture(this.form);
            this.ui = new UI(this.form);

            startGame();
        }
        public void startGame()
        {
            this.texture.setGameTextures();
            this.texture.setGameTexturesEvents();
            this.ui.setGameButtons(this.texture.emptySocket);
        }
    }
}