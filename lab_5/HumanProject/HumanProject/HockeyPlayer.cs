using System;

namespace HumanProject
{
    public enum HockeyPositions
    {
        Goalkeeper,
        LeftDefenseman,
        RightDefenseman,
        LeftWing,
        Center,
        RightWing
    }

    public class HockeyPlayer : Sportsman
    {
        public HockeyPositions Position;

        public double Speed
        {
            get
            {
                return _speed;
            }
            private set
            {
                if (value > 40) _speed = 40;
                else if (value < 0) _speed = 0;
                else _speed = value;
            }
        }
        double _speed;

        public double StrikePower
        {
            get
            {
                return _strikePower;
            }
            private set
            {
                if (value > 1e9) _strikePower = 1e9;
                else if (value < 0) _strikePower = 0;
                else _strikePower = value;
            }
        }
        double _strikePower;

        public HockeyPlayer() : base()
        {
            Speed = 20;
            StrikePower = 500;
        }

        public HockeyPlayer(string fullName, string identifier) : base(fullName, identifier)
        {
            Speed = 20;
            StrikePower = 500;
        }

        public HockeyPlayer(string fullName, string identifier, HockeyPositions position,
            double speed, double strikePower) : base(fullName, identifier)
        {
            Position = position;
            Speed = speed;
            StrikePower = strikePower;
        }

        public HockeyPlayer(string fullName, string identifier, double weight, double height,
            double speed, double strikePower)
            : base(fullName, identifier, weight, height)
        {
            Speed = speed;
            StrikePower = strikePower;
        }

        public HockeyPlayer(string fullName, string identifier, double weight, double height,
            HockeyPositions position, double speed, double strikePower)
            : base(fullName, identifier, weight, height)
        {
            Position = position;
            Speed = speed;
            StrikePower = strikePower;
        }

        public override string ToString()
        {
            return string.Format("{0} id={1} w={2} h={3} pos={4} speed={5} s_p={6}",
                FullName, Identifier, Weight, Height, Position, Speed, StrikePower);
        }

        public override void Train(int minutes)
        {
            Random random = new Random();
            Speed += 0.001 * minutes * random.Next(1, 11);
            StrikePower += 0.01 * minutes * random.Next(1, 11);
            base.Train(minutes);
        }
    }
}