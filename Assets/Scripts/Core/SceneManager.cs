using System;
using App.Times.Commands;
using App.Watches;
using UnityEngine;

namespace App.Core
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private Watch[] watches;

        private void OnEnable()
        {
            AppManager.Instance.TimeManager.OnTimeChanged += UpdateTime;

            for (var i = 0; i < watches.Length; i++)
                watches[i].TimeChanged += TimeChanging;
        }

        private void OnDisable()
        {
            AppManager.Instance.TimeManager.OnTimeChanged -= UpdateTime;

            for (var i = 0; i < watches.Length; i++)
                watches[i].TimeChanged -= TimeChanging;
        }

        private void UpdateTime(DateTime utcTimeNow)
        {
            for (var i = 0; i < watches.Length; i++)
                watches[i].UpdateTimeOnWatch(utcTimeNow);
        }

        private void TimeChanging(TimeCommand timeCommand)
        {
            AppManager.Instance.TimeManager.SetTimeFromCommand(timeCommand);
        }
    }
}