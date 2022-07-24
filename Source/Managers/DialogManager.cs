using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTestGame.Managers
{
    public class DialogManager
    {
        private enum State
        {
            Typing,
            ShiftingLine,
            PageDone,
            MessageDone
        }

        private State _state;
        const float LetterTime = 0.018f;
        const float ArrowTime = 0.4f;
        const float LineShiftTime = 0.2f;
        private float _elapsedTime = 0f;
        private int _drawLetterCount = 0;
        private bool _dialogBoxIsFull;
        private int _currentStartOfMessage;
        private string _currentMessage;
        private string _currentString;
        private int _pageLineIndex;
        private int _dialogBoxTextHeight;
        private int _dialogMessageIndex;
        private float _cropY;
        private Dialog _dialog;
        private bool _running;
        private bool _topDialogBox;
        public event Action DialogEnd;
        public DialogBox DialogBox = new LinkToThePastSectionedDialogBox();

        public void Load(Dialog dialog, bool topDialogBox = false)
        {
            _dialog = dialog;
            _topDialogBox = topDialogBox;
            _running = true;
            _dialogMessageIndex = 0;
 
            StartNewMessage();
        }

        private void StartNewMessage()
        {
            _currentMessage = _dialog.Messages[_dialogMessageIndex];
            _dialogBoxIsFull = false;
            _drawLetterCount = 0;
            _currentStartOfMessage = 0;
            _elapsedTime = 0;
            _currentString = "";
            _pageLineIndex = 0;
            _state = State.Typing;
            
            int currentLineCount = Regex.Matches(_currentMessage, "\n").Count + 1;
            _dialogBoxTextHeight = Math.Min(currentLineCount, 3) * BitmapFontRenderer.Font.LineHeight;
        }

        private void NextMessage()
        {
            _dialogMessageIndex++;

            if (_dialogMessageIndex < _dialog.Messages.Length)
            {
                StartNewMessage();
            }
            else
            {
                EndDialog();
            }
        }

        private void EndDialog()
        {
            _running = false;
            _dialogMessageIndex = 0;
            SFX.MessageFinish.Play();
            DialogEnd?.Invoke();
        }

        public void Update(GameTime gameTime)
        {
            if (!_running) return;

            switch(_state)
            {
                case State.Typing:
                    _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (_elapsedTime > LetterTime)
                    {
                        _drawLetterCount++;
                        _elapsedTime = 0;

                        char currentChar = _currentMessage[_drawLetterCount - 1];

                        if (currentChar == '\n')
                        {
                            _pageLineIndex++;

                            if (_pageLineIndex != 3 && _dialogBoxIsFull)
                            {
                                _state = State.ShiftingLine;
                            }
                        }
                        else if (currentChar != ' ')
                        {
                            _currentString = _currentMessage.Substring(_currentStartOfMessage, _drawLetterCount - _currentStartOfMessage);
                            SFX.Message.Play();
                        }

                        if (_drawLetterCount == _currentMessage.Length)
                        {
                            _state = State.MessageDone;
                        }
                        else if (_pageLineIndex == 3)
                        {
                            _dialogBoxIsFull = true;
                            _state = State.PageDone;
                        }
                    }
                    break;
                case State.ShiftingLine:
                    _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (_elapsedTime < LineShiftTime)
                    {
                        _cropY = BitmapFontRenderer.Font.LineHeight * _elapsedTime / LineShiftTime;
                    }
                    else
                    {
                        int nextNewLineIndex = _currentString.IndexOf('\n');
                        _cropY = 0;
                        _currentStartOfMessage += nextNewLineIndex + 1;
                        //_currentString = _currentString.Substring(nextNewLineIndex + 1, _currentString.Length - nextNewLineIndex);
                        _currentString = _currentMessage.Substring(_currentStartOfMessage, _drawLetterCount - _currentStartOfMessage);
                        _elapsedTime = 0;
                        _state = State.Typing;
                    }
                break;
                case State.PageDone:
                    if (GotNextInput())
                    {
                        _pageLineIndex = 0;
                        _state = State.ShiftingLine;
                    }
                break;
                case State.MessageDone:
                    if (GotNextInput())
                    {
                        NextMessage();
                    }
                break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_running) return;

            DialogBox.Draw(spriteBatch, _topDialogBox, _currentString, _cropY, _dialogBoxTextHeight);
        }

        private bool GotNextInput()
        {
            return Input.JustPressed(Input.A) || Input.JustPressed(Input.B) || Input.JustPressedMouseLeft();
        }
    }
}