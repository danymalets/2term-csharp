using System;
namespace HumanProject
{
    public enum SoccerPositions
    {
        Goalkeeper,
        RightFullback,
        LeftFullback,
        CenterBack,
        Sweeper,
        HoldingMidfielder,
        RightMidfielder,
        BoxToBoxMidfielder,
        Striker,
        AttackingMidfielder,
        LeftMidfielder
    }

    public class SoccerPlayer : Sportsman, ITeamPlayer
    {
        public SoccerPositions Position;

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

        string Team;

        public SoccerPlayer() : base()
        {
            Speed = 20;
            StrikePower = 500;
        }

        public SoccerPlayer(string fullName, string identifier) : base(fullName, identifier)
        {
            Speed = 20;
            StrikePower = 500;
        }

        public SoccerPlayer(string fullName, string identifier, SoccerPositions position,
            double speed, double strikePower, string team) : base(fullName, identifier)
        {
            Position = position;
            Speed = speed;
            StrikePower = strikePower;
            Team = team;
        }

        public SoccerPlayer(string fullName, string identifier, double weight, double height,
            double speed, double strikePower)
            : base(fullName, identifier, weight, height)
        {
            Speed = speed;
            StrikePower = strikePower;
        }

        public SoccerPlayer(string fullName, string identifier, double weight, double height,
            SoccerPositions position, double speed, double strikePower)
            : base(fullName, identifier, weight, height)
        {
            Position = position;
            Speed = speed;
            StrikePower = strikePower;
        }

        public override string ToString()
        {
            return string.Format("{0}, id={1}, position={2}, speed={3}, strike power={4}, team={5}",
                FullName, Identifier, Position, Speed, StrikePower, Team ?? "no team");
        }

        public override void Train(int minutes)
        {
            Random random = new Random();
            Speed += 0.001 * minutes * random.Next(1, 11);
            StrikePower += 0.01 * minutes * random.Next(1, 11);
            base.Train(minutes);
        }

        public void LeaveTeam()
        {
            Team = null;
        }

        public void EnterTeam(string team)
        {
            Team = team;
        }

        public string GetTeam()
        {
            return Team;
        }
    }
}
