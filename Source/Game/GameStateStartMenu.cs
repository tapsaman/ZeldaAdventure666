using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ZA6.Managers;
using ZA6.Manangers;

namespace ZA6.Models
{
    public class GameStateStartMenu : RenderState
    {
        private ZeldaAdventure666 _game;
        private GameMenu _menu;
        private SceneManager _sceneManager;
        private float _elapsedTime;
        private const float _WAIT_TIME = 2f;
        private const float _FADE_TIME = 5f;
        private const float _FADE_TITLE_TIME = 3f;
        private const float _SHOW_MENU_TIME = 10f;
        private const float _START_WAIT_TIME = 1.4f;
        private float _overlayMultiplier;
        private float _titleMultiplier;
        private bool _pressedStart;
        private Texture2D _overlay;
        private Song _song;

        public GameStateStartMenu(ZeldaAdventure666 game)
        {
            CanReEnter = true;
            _game = game;
            _overlay = Utility.CreateColorTexture(Static.NativeWidth * 2, Static.NativeHeight * 2, Color.Black);
            _song = Static.Content.Load<Song>("oot_title_theme");
        }

        public override void Enter(StateArgs _)
        {
            Music.Stop();
            Music.Play(_song);
            _elapsedTime = 0;
            _overlayMultiplier = 1f;
            _titleMultiplier = 0f;
            _pressedStart = false;
            _sceneManager = new SceneManager()
            {
                World = Static.World
            };
            _sceneManager.Init("B1");
            _sceneManager.CurrentScene.UpdateCamera(new Vector2(450, 150));
            _sceneManager.CurrentScene.Paused = false;
            _sceneManager.Player.StateMachine.TransitionTo("Stopped");
            _menu = null;
        }

        public override void Update(GameTime gameTime)
        {
            Music.Update(gameTime);
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _sceneManager.Update(gameTime);

            if (_menu == null)
            {   
                if (_elapsedTime < _WAIT_TIME)
                {
                    return;
                }

                if (Input.P1.IsAnyKeyPressed() || Input.P1.JustReleasedMouseLeft())
                {
                    _elapsedTime = _SHOW_MENU_TIME - 0.2f;
                    _overlayMultiplier = 0f;
                    _titleMultiplier = 1f;
                }

                if (_elapsedTime < _WAIT_TIME + _FADE_TIME)
                {
                    _overlayMultiplier = (1 - (_elapsedTime - _WAIT_TIME) / _FADE_TIME);
                }
                else if (_elapsedTime < _WAIT_TIME + _FADE_TIME + _FADE_TITLE_TIME)
                {
                    _titleMultiplier = (_elapsedTime - _WAIT_TIME - _FADE_TIME) / _FADE_TITLE_TIME;
                }
                else if (_elapsedTime > _SHOW_MENU_TIME)
                {
                    _overlayMultiplier = 0f;
                    _titleMultiplier = 1f;
                    _menu = new GameMenu(PressedStartGame, true);
                    UI.Add(_menu);
                }
            }
            if (!_pressedStart)
            {
                UI.Update(gameTime);
            }
            else if (_elapsedTime > _START_WAIT_TIME)
            {
                stateMachine.TransitionTo("StartOver");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Static.Renderer.StartMenuStart();

            _sceneManager.Draw(Static.SpriteBatch);
            spriteBatch.Draw(_overlay, Vector2.Zero, Color.Black * _overlayMultiplier);
            
            Static.Renderer.StartMenuStartUI();
            
            if (true || _menu != null)
            {
                UI.Draw(Static.SpriteBatch);

                if (UI.CurrentMenu == _menu)
                {
                    DrawTitle(spriteBatch);
                }
            }

            Static.Renderer.StartMenuEnd();
        }

        private void DrawTitle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Img.GameTitle,
                new Vector2(Static.NativeWidth / 2 - Img.GameTitle.Width / 2, 15),
                Color.White * _titleMultiplier
            );

            Shaders.Wavy.Parameters["yOffset"].SetValue(_elapsedTime / 4);
            Static.Renderer.ChangeToEffect(Shaders.Wavy);

            spriteBatch.Draw(
                Img.GameNumberTitle,
                new Vector2(Static.NativeWidth / 2 - Img.GameNumberTitle.Width / 2, 15 + Img.GameTitle.Height),
                Color.White * _titleMultiplier
            );

            Static.Renderer.ChangeToDefault();
        }

        private void PressedStartGame(object sender, EventArgs e)
        {
            //_menu.Enabled = false;
            _pressedStart = true;
            _elapsedTime = 0f;
            Music.FadeOut(_START_WAIT_TIME);
        }

        public override void Exit()
        {
            //UI.SetToRemove(_menu);
            UI.SetToClear();
            _menu = null;
        }
    } 
}