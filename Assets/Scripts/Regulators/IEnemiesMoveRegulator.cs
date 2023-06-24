using Characters;

namespace Regulators
{
    public interface IEnemiesMoveRegulator
    {
        void UpdateEnemiesMoves(GameplayItemLinks[] enemies);
    }
}