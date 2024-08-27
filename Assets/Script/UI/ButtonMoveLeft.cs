using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class ButtonMoveLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private float _value = 0;

        public void OnPointerDown(PointerEventData eventData)
        {
            _value -= 1f;
            Debug.Log("_value left: " + _value);
            MoveController.Instance.InputHorizontalButton = Mathf.Max(_value, -1);
        }

        
        public void OnPointerUp(PointerEventData eventData)
        {
            _value = 0;
            MoveController.Instance.InputHorizontalButton = 0;
        }
    }
}