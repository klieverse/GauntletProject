﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class InputHelper
{
    protected MouseState currentMouseState, previousMouseState;
    protected KeyboardState currentKeyboardState, previousKeyboardState;
    protected Vector2 scale, offset;
    protected GamePadState currentGamePadState, previousGamePadState;
    protected GamePadCapabilities capabilities;
    public InputHelper()
    {
        scale = Vector2.One;
        offset = Vector2.Zero;
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        previousGamePadState = currentGamePadState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(PlayerIndex.One);
        capabilities = GamePad.GetCapabilities(PlayerIndex.One);
    }

    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public Vector2 MousePosition
    {
        get { return (new Vector2(currentMouseState.X, currentMouseState.Y) - offset) / scale; }
    }

    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool keyReleased(Keys k)
    {
        return currentKeyboardState.IsKeyUp(k);
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }

    public bool AnyKeyDown
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0; }
    }

    public Vector2 JoyStickLeft
    {
        get { return currentGamePadState.ThumbSticks.Left; }
    }
    public Vector2 JoyStickRight
    {
        get { return currentGamePadState.ThumbSticks.Right; } 
    }
    public bool ButtonPressed(Buttons b)
    {
        return currentGamePadState.IsButtonDown(b) && previousGamePadState.IsButtonUp(b);
    }
    public bool ControllerConnected()
    {
        return capabilities.IsConnected;
    }
    public static bool UsingController;

    public List<Keys> PressedKeys()
    {
        List<Keys> pressedKeys = new List<Keys>();
        Keys[] currentPressed = currentKeyboardState.GetPressedKeys();
        Keys[] previousPressed = previousKeyboardState.GetPressedKeys();
        foreach (Keys k in currentPressed)
        {
            foreach(Keys l in previousPressed)
            {
                //als de key in beide keyboards zit dan is het nog niet gereleased
                if(k==l)
                {
                    break;
                }
            }
            //de key zit niet in de vorige state;
            pressedKeys.Add(k);
        }
        return pressedKeys;
    }
}
