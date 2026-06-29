namespace App.Times.Commands
{
    public class SetTextTimeCommand : TimeCommand
    {
        public string Text { get; private set; }

        public SetTextTimeCommand(string text)
        {
            Text = text;
        }
    }
}