using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUIController : MonoBehaviour
{
    [SerializeField] private ButtonCastom Up;
    [SerializeField] private ButtonCastom Down;
    [SerializeField] private ButtonCastom Left;
    [SerializeField] private ButtonCastom Right;
    [SerializeField] private ButtonCastom Fire;
    private Vector2 _input;
    private bool _isFire;
    private static InputUIController me;

    public static Vector2 MoveInput
    {
        get
        {
            var t = me._input;
            me._input = Vector2.zero;
            return t;
        }
    }

    public static bool FireInput
    {
        get
        {
            var t = me._isFire;
            me._isFire = false;
            return t;
        }
    }

    private void Awake()
    {
        if (me == null)
            me = this;
    }
    public void OnEnable()
    {
        Up.onClick = OnUp;
        Down.onClick = OnDown;
        Left.onClick = OnLeft;
        Right.onClick = OnRight;
        Fire.onClick = OnFire;
    }

    private void OnFire()
    {
        _isFire = true;
    }

    private void OnRight()
    {
        _input = Vector2.right;
    }

    private void OnLeft()
    {
        _input = Vector2.left;
    }

    private void OnDown()
    {
        _input = Vector2.down;
    }

    private void OnUp()
    {
        _input = Vector2.up;
    }
}
