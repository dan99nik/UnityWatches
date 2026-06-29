using System;

namespace App.Watches.Hands
{
    public static class HandConverter
    {
        public static float GetSecondAngle(DateTime time) => time.Second * 6f;

        public static float GetMinuteAngle(DateTime time) => (time.Minute + time.Second / 60f) * 6f;

        public static float GetHourAngle(DateTime time) => ((time.Hour % 12) + time.Minute / 60f) * 30f;
    }
}