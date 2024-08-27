using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script
{
    public class ButtonMoveJump : Button
    {
        protected override void Awake()
        {
            base.Awake();
            if (this)
            {
                this.onClick.AddListener(OnButtonClick);
            }
        }

        void OnButtonClick()
        {
            MoveController.Instance.IsJumpButton = true;
            Debug.Log("true");
        }
    }
}