using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LD24
{
    /// <summary>
    /// Manages state for all input devices.
    /// </summary>
    public class InputState
    {
        #region Properties

        public KeyboardState KeyboardState { get; private set; }

        public KeyboardState LastKeyboardState { get; private set; }

        public MouseState MouseState { get; private set; }

        public MouseState LastMouseState { get; private set; }

        public Point MousePosition { get; private set; }

        public Point LastMousePosition { get; private set; }

        public Vector2 MousePositionAsVector { get; private set; }

        public Vector2 LastMousePositionAsVector { get; private set; }

        public bool MouseInputHandled { get; set; }

        #endregion

        #region Constructors

        public InputState()
        {
            KeyboardState = Keyboard.GetState();
            LastKeyboardState = KeyboardState;

            MouseState = Mouse.GetState();
            LastMouseState = MouseState;
        }

        #endregion

        #region Methods

        public void Update()
        {
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            MouseInputHandled = false;
            LastMouseState = MouseState;
            LastMousePosition = MousePosition;
            LastMousePositionAsVector = MousePositionAsVector;
            MouseState = Mouse.GetState();

            MousePosition = new Point(MouseState.X, MouseState.Y);

            MousePositionAsVector = new Vector2(MouseState.X, MouseState.Y);
        }

        /// <summary>
        /// Returns true as soon as the user presses the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key) &&
                LastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Returns true as soon as the user releases the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyUp(Keys key)
        {
            return KeyboardState.IsKeyUp(key) &&
                LastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Returns true if they key is in the pressed state.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Returns true as soon as the left mouse button is pressed.
        /// </summary>
        /// <returns></returns>
        public bool LeftMouseDown()
        {
            return MouseState.LeftButton == ButtonState.Pressed &&
                LastMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Returns true as soon as the left mouse button is released.
        /// </summary>
        /// <returns></returns>
        public bool LeftMouseUp()
        {
            return MouseState.LeftButton == ButtonState.Released &&
                LastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Returns true if the left mouse is in the pressed state.
        /// </summary>
        /// <returns></returns>
        public bool LeftMousePressed()
        {
            return MouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Returns true as soon as the right mouse button is pressed.
        /// </summary>
        /// <returns></returns>
        public bool RightMouseDown()
        {
            return MouseState.RightButton == ButtonState.Pressed &&
                LastMouseState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Returns true as soon as the right mouse button is released.
        /// </summary>
        /// <returns></returns>
        public bool RightMouseUp()
        {
            return MouseState.RightButton == ButtonState.Released &&
                LastMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Returns true if the right mouse is in the pressed state.
        /// </summary>
        /// <returns></returns>
        public bool RightMousePressed()
        {
            return MouseState.RightButton == ButtonState.Pressed;
        }

        public bool MouseScrolledUp()
        {
            return MouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        public bool MouseScrolledDown()
        {
            return MouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }

        #endregion
    }
}
