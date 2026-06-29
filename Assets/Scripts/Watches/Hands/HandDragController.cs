using UnityEngine;
using App.Times.Commands;

namespace App.Watches.Hands
{
    public class HandDragController : MonoBehaviour
    {
        [SerializeField] private HandClock handClock;
        [SerializeField] private RectTransform clockCenter;

        private HandSettings _activeHand;
        private bool _dragging;
        private float _lastMouseAngle;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                BeginDrag();

            if (_dragging)
                Drag();

            if (Input.GetMouseButtonUp(0))
                EndDrag();
        }

        private void BeginDrag()
        {
            _activeHand = GetClosestHandToMouse();

            if (_activeHand == null)
                return;

            _dragging = true;
            _lastMouseAngle = GetMouseAngle();
        }

        private void Drag()
        {
            var currentAngle = GetMouseAngle();
            var delta = -Mathf.DeltaAngle(_lastMouseAngle, currentAngle);

            _lastMouseAngle = currentAngle;

            handClock.TimeChanging(new RotateHandCommand(_activeHand.HandType, delta));
        }

        private void EndDrag()
        {
            _dragging = false;
            _activeHand = null;
        }

        private float GetMouseAngle()
        {
            var center = RectTransformUtility.WorldToScreenPoint(null, clockCenter.position);
            var dir = (Vector2)Input.mousePosition - center;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return (angle + 360f) % 360f;
        }

        private HandSettings GetClosestHandToMouse()
        {
            var mouseAngle = GetMouseAngle();
            var nearestDelta = float.MaxValue;

            HandSettings nearestHand = null;

            foreach (var handSetting in handClock.HandSettings)
            {
                var handAngle = (handSetting.HandTransform.localEulerAngles.z + 360f) % 360f;
                var delta = Mathf.Abs(Mathf.DeltaAngle(mouseAngle, handAngle));

                if (delta < nearestDelta)
                {
                    nearestDelta = delta;
                    nearestHand = handSetting;
                }
            }

            return nearestHand;
        }
    }
}