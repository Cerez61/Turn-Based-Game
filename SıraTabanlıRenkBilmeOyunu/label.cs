namespace LabelManagement
{
    public class Text
    {
        Form form;

        Label label;
        public Label guessLabel = new Label();
        int guessTextWidth;
        int guessTextHeight;


        public Text(Form form)
        {
            this.form = form;

            this.guessTextWidth = 50;
            this.guessTextHeight = 50;
        }
        public void guessText(int trueCount,int falseCount)
        {
            label = new Label();

            label.Width = this.guessTextWidth;
            label.Height = this.guessTextHeight;

            label.Text = $"Doðru:{trueCount}\nYanlýþ:{falseCount}";

            this.guessLabel = label;
            this.form.Controls.Add(this.guessLabel);
        }
        public void visibleText()
        {
            foreach (var lbl in this.form.Controls.OfType<Label>())
            {
                lbl.Visible = !lbl.Visible;
            }
        }
    }
}