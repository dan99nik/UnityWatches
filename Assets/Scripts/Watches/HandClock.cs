using System;
using App.Watches.Hands;
using UnityEngine;

namespace App.Watches
{
    public class HandClock : Watch
    {
        [SerializeField] private HandSettings[] handSettings;

        public HandSettings[] HandSettings => handSettings;

        public override void UpdateTimeOnWatch(DateTime utcTimeNow)
        {
            foreach (var handSetting in handSettings)
            {
                switch (handSetting.HandType)
                {
                    case HandType.HourHand:
                        handSetting.HandTransform.localRotation = Quaternion.Euler(0, 0, -HandConverter.GetHourAngle(utcTimeNow));
                        break;
                    case HandType.MinuteHand:
                        handSetting.HandTransform.localRotation = Quaternion.Euler(0, 0, -HandConverter.GetMinuteAngle(utcTimeNow));
                        break;
                    case HandType.SecondHand:
                        handSetting.HandTransform.localRotation = Quaternion.Euler(0, 0, -HandConverter.GetSecondAngle(utcTimeNow));
                        break;
                }
            }
        }
    }
}
