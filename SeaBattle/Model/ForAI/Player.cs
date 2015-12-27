using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle.Model.ForAI
{
    public abstract class Player
    {
        protected readonly Dictionary<Point, ShootResult> PastShots;
        private bool _canShoot;

        protected Player(string name)
        {
            Name = name;
            PastShots = new Dictionary<Point, ShootResult>();
        }

        public string Name { get; set; }
        public virtual void Shoot()
        {
            _canShoot = true;
            var handler = MyTurn;
            if (handler != null)
                handler(this, new EventArgs());
        }
        public virtual void Reset()
        {
            PastShots.Clear();
            _canShoot = false;
        }
        protected void ShotTargetChosen(int x, int y)
        {
            if (!_canShoot)
                return;

            _canShoot = false;

            var shooting = Shooting;
            if (shooting == null)
                return;

            var eventArgs = new ShootingEventArgs(x, y);
            shooting(this, eventArgs);
            AddShotResult(x, y, eventArgs.Result);

            var shot = Shot;
            if (shot != null)
                shot(this, eventArgs);

        }

        protected virtual void AddShotResult(int x, int y, ShootResult result)
        {
            PastShots[new Point(x, y)] = result;
        }
        public event EventHandler<ShootingEventArgs> Shooting;
        public event EventHandler<ShootingEventArgs> Shot;
        public event EventHandler MyTurn;
    }
    public class ShootingEventArgs : EventArgs
    {
        private readonly int _x;
        private readonly int _y;

        public ShootingEventArgs(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public ShootResult Result { get; set; }
    }
}
