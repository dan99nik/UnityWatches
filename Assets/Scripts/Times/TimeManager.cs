using System;
using System.Globalization;
using System.Threading.Tasks;
using App.Times.Commands;
using App.Watches.Hands;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Times
{
    public class TimeManager
    {
        private DateTime _utcTimeNow;

        public Action<DateTime> OnTimeChanged;

        public async Task GetRequest(string uri)
        {
            using UnityWebRequest uwr = UnityWebRequest.Get(uri);

            var operation = uwr.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error While Sending: {uwr.error}");
                return;
            }

            var yandexData = JsonUtility.FromJson<YandexTimeResponse>(uwr.downloadHandler.text);
            _utcTimeNow = DateTimeOffset.FromUnixTimeMilliseconds(yandexData.time).UtcDateTime;
            TimeChanged();

            CalculateTime();
        }

        private async Task CalculateTime()
        {
            while (true)
            {
                _utcTimeNow = _utcTimeNow.AddSeconds(Time.deltaTime);
                TimeChanged();

                await Task.Yield();
            }
        }

        public void SetTimeFromCommand(TimeCommand timeCommand)
        {
            switch (timeCommand)
            {
                case RotateHandCommand command:
                    SetTimeFromHand(command.HandType, command.DeltaAngle);
                    break;

                case SetTextTimeCommand command:
                    SetTimeFromText(command.Text);
                    break;
            }
        }

        private void SetTimeFromHand(HandType handType, float angle)
        {
            switch (handType)
            {
                case HandType.SecondHand:
                    _utcTimeNow = _utcTimeNow.AddSeconds(angle / 6f);
                    break;

                case HandType.MinuteHand:
                    _utcTimeNow = _utcTimeNow.AddMinutes(angle / 6f);
                    break;

                case HandType.HourHand:
                    _utcTimeNow = _utcTimeNow.AddHours(angle / 30f);
                    break;
            }

            TimeChanged();
        }

        private void SetTimeFromText(string time)
        {
            var currentUtcTime = _utcTimeNow;
            var timeOfDay = TimeSpan.ParseExact(time, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);

            _utcTimeNow = currentUtcTime.Date + timeOfDay;

            TimeChanged();
        }
        
        private void TimeChanged() => OnTimeChanged?.Invoke(_utcTimeNow);
    }
}