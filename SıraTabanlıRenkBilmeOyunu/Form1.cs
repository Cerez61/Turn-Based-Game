using GameManagement;
namespace SıraTabanlıRenkBilmeOyunu
{
    public partial class Form1 : Form
    {
        Game game;
        public Form1()
        {
            InitializeComponent();
            game = new Game(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
