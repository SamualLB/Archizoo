using System;
using Microsoft.Xna.Framework;
using Stateless;

namespace Archizoo.Entities
{
    public abstract class Animal : Entity
    {
        enum State
        {
            Ambient,
            Sleeping,
            Dead
        }

        private readonly StateMachine<State, char> _machine;

        protected Animal(Color fg, int glyph) : base(fg, glyph)
        {
            _machine = new StateMachine<State, char>(State.Ambient);
            
            _machine.Configure(State.Ambient).Permit('s', State.Sleeping);
            _machine.Configure(State.Ambient).Permit('d', State.Dead);
            _machine.Configure(State.Sleeping).Permit('a', State.Ambient);
            _machine.Configure(State.Sleeping).Permit('d', State.Dead);

            _machine.Configure(State.Dead).OnEntry(OnDead);
            
            _machine.OnTransitioned(t => Console.WriteLine($"OnTransitioned: {t.Source} -> {t.Trigger}({string.Join(", ", t.Parameters)}))"));
        }

        private void OnDead()
        {
            Animation.Frames[0].Cells[0].Foreground = Color.Red;
        }

        public void Sleep()
        {
            _machine.Fire('s');
        }

        public void Ambient()
        {
            _machine.Fire('a');
        }

        public void Die()
        {
            _machine.Fire('d');
        }
    }
}