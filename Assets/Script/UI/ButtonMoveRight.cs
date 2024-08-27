using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class ButtonMoveRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private float _value;
        public void OnPointerDown(PointerEventData eventData)
        {
            _value += 1f;
            Debug.Log("_value right: " + _value);
            MoveController.Instance.InputHorizontalButton = Mathf.Min(_value, 1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _value = 0;
            MoveController.Instance.InputHorizontalButton = 0; ;
        }
    }
}