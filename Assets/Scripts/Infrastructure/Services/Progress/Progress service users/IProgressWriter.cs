using Game_logic;

namespace Infrastructure
{
    public interface IProgressWriter
    {
        void Save(Progress progress);
    }
}