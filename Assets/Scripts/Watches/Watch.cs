using System;
using App.Times.Commands;
using UnityEngine;

namespace App.Watches
{
    public abstract class Watch : MonoBehaviour
    {
        public event Action<TimeCommand> TimeChanged;

        public virtual void UpdateTimeOnWatch(DateTime utcTimeNow) { }

        public void TimeChanging(TimeCommand timeCommand) => TimeChanged?.Invoke(timeCommand);
    }
}