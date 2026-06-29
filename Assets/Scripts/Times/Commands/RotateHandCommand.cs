
using App.Watches.Hands;

namespace App.Times.Commands
{
    public class RotateHandCommand : TimeCommand
    {
        public HandType HandType { get; private set; }
        public float DeltaAngle { get; private set; }

        public RotateHandCommand(HandType handType, float deltaAngle)
        {
            HandType = handType;
            DeltaAngle = deltaAngle;
        }
    }
}
