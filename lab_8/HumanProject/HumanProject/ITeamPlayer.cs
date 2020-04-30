using System;
namespace HumanProject
{
    public interface ITeamPlayer
    {
        string FullName { get; }

        string Identifier { get; }

        void LeaveTeam();

        void EnterTeam(string team);

        string GetTeam();
    }
}
