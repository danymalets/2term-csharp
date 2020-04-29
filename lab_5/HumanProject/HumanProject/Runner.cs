using System;

namespace HumanProject
{
    public class Runner : Sportsman
    {
        // km/h
        public double Speed 
        {
            get
            {
                return _speed;
            }
            private set
            {
                if (value < 0) _speed = 0;
                else if (value > 50) _speed = 50;
                else _speed = value;
            }
        }
        double _speed;

        public Runner() : base()
        {
            Speed = 20;
        }

        public Runner(string fullName, string identifier) : base(fullName, identifier)
        {
            Speed = 20;
        }

        public Runner(string fullName, string identifier, double speed) : base(fullName, identifier)
        {
            Speed = speed;
        }

        public Runner(string fullName, string identifier,
            double weight, double height, double speed)
            : base(fullName, identifier, weight, height)
        {
            Speed = speed;
        }

        public override string ToString()
        {
            return string.Format("{0} id={1} w={2} h={3} speed={4}", FullName, Identifier, Weight, Height, Speed);
        }

        public int Run(int metres)
        {
            return (int)((double)3600 * metres / Speed);
        }

        public override void Train(int minutes)
        {
            Random random = new Random();
            Speed += 0.001 * minutes * random.Next(1, 11);
            base.Train(minutes);
        }
    }
}