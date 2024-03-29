using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TapsasEngine;
using ZA6.Models;

namespace ZA6.Managers
{
    
    public class EventSystem : IUpdate
    {
        [Flags]
        public enum Settings
        {
            None                = 0b_0000_0000,
            Parallel            = 0b_0000_0001,
            SustainSceneChange  = 0b_0000_0010,
            Looping             = 0b_0000_0100
        }

        private List<EventManagerAndSettings> _queue = new List<EventManagerAndSettings>();
        private List<EventManagerAndSettings> _parallel = new List<EventManagerAndSettings>();

        public void Load(Event singleEvent, Settings settings = Settings.None)
        {
            Load(new Event[] { singleEvent }, settings);
        }
        
        public void Load(Event[] eventList, Settings settings = Settings.None)
        {
            if ((settings & Settings.Parallel) == Settings.Parallel)
            {
                _parallel.Add(new EventManagerAndSettings()
                {
                    EventManager = new EventManager(eventList),
                    Settings = settings
                });
            }
            else
            {
                Static.Game.StateMachine.TransitionTo("Cutscene");

                _queue.Add(new EventManagerAndSettings()
                {
                    EventManager = new EventManager(eventList),
                    Settings = settings
                });
            }
        }

        public void Clear()
        {
            foreach (var item in _queue)
                item.EventManager.Exit();

            foreach (var item in _parallel)
                item.EventManager.Exit();
            
            _queue.Clear();
            _parallel.Clear();
        }

        public void OnSceneChange()
        {
            for (int i = _queue.Count - 1; i >= 0 ; i--)
            {
                if (DoesNotSustainSceneChange(_queue[i]))
                {
                    _queue[i].EventManager.Exit();
                    _queue.RemoveAt(i);
                }
            }

            for (int i = _parallel.Count - 1; i >= 0 ; i--)
            {
                if (DoesNotSustainSceneChange(_parallel[i]))
                {
                    _parallel[i].EventManager.Exit();
                    _parallel.RemoveAt(i);
                }
            }
        }

        private bool DoesNotSustainSceneChange(EventManagerAndSettings item)
        {
            return (item.Settings & Settings.SustainSceneChange) != Settings.SustainSceneChange;
        }

        public void Update(GameTime gameTime)
        {
            if (_queue.Count != 0)
            {
                var firstEventManager = _queue[0].EventManager;

                if (!firstEventManager.IsEntered)
                {
                    firstEventManager.Enter();
                }
                else if (!firstEventManager.IsDone)
                {
                    firstEventManager.Update(gameTime);
                }
                else if ((_queue[0].Settings & Settings.Looping) != 0)
                {
                    firstEventManager.Exit();
                    firstEventManager.Enter();
                }
                else
                {
                    firstEventManager.Exit();
                    _queue.RemoveAt(0);

                    if (_queue.Count == 0)
                    {
                        Static.Game.StateMachine.TransitionTo("Default");
                    }
                }
            }
            else
            {
                for (int i = _parallel.Count - 1; i >= 0 ; i--)
                {
                    var item = _parallel[i];
                    var manager = item.EventManager;

                    if (!manager.IsEntered)
                    {
                        manager.Enter();
                    }
                    else if (!manager.IsDone)
                    {
                        manager.Update(gameTime);
                    }
                    else if ((item.Settings & Settings.Looping) != 0)
                    {
                        manager.Exit();
                        manager.Enter();
                    }
                    else
                    {
                        manager.Exit();
                        _parallel.RemoveAt(i);
                    }
                }
            }
        }

        private class EventManagerAndSettings
        {
            public EventManager EventManager;
            public Settings Settings;
        }

    }
}