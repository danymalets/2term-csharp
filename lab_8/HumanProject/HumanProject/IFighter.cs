using System;
namespace HumanProject
{
    public interface IFighter<T>
    {
        double GetTotalStrength();

        bool FightWith(T opponent);
    }
}
