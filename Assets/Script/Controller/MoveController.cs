using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    private static MoveController _instance;
//    [SerializeField] private ButtonMoveLeft leftBtn;
//    [SerializeField] private ButtonMoveRight rightBtn;
//    [SerializeField] private ButtonMoveJump jumpBtn;

    private float _inputHorizontalButton;
    private float _inputHorizontalKey;
    private float _inputHorizontalTotal;
    
    private bool _isJumpKey = false;
    private bool _isJumpButton = false;
    private bool _isJumpTotal = false;
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static MoveController Instance => _instance;

    public float InputHorizontalButton
    {
        get => _inputHorizontalButton;
        set => _inputHorizontalButton = value;
    }

    public float InputHorizontalTotal => _inputHorizontalTotal;

    public bool IsJumpTotal
    {
        get => _isJumpTotal;
        set => _isJumpTotal = value;
    }

    public bool IsJumpKey
    {
        get => _isJumpKey;
        set => _isJumpKey = value;
    }
    
    public bool IsJumpButton
    {
        get => _isJumpButton;
        set => _isJumpButton = value;
    }

    private void Update()
    {
        _inputHorizontalKey = Input.GetAxis("Horizontal");

        _inputHorizontalTotal = _inputHorizontalButton + _inputHorizontalKey;
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            _isJumpKey = true;
        else _isJumpKey = false;

        _isJumpTotal = _isJumpKey || _isJumpButton;
    }
    
}
