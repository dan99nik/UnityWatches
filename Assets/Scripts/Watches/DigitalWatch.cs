using System;
using App.Times.Commands;
using TMPro;
using UnityEngine;

namespace App.Watches
{
    public class DigitalWatch : Watch
    {
        [SerializeField] private TMP_InputField timeText;

        private void OnEnable() => timeText.onEndEdit.AddListener(GetTimeFromText);

        private void OnDisable() => timeText.onEndEdit.RemoveListener(GetTimeFromText);

        public override void UpdateTimeOnWatch(DateTime utcTimeNow)
        {
            if (timeText.isFocused)
                return;

            timeText.text = $"{utcTimeNow:HH:mm:ss}";
        }

        private void GetTimeFromText(string time)
        {
            TimeChanging(new SetTextTimeCommand(time));
        }
    }
}