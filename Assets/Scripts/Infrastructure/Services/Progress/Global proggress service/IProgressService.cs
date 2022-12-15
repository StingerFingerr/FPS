using Game_logic;

namespace Infrastructure
{
    public interface IProgressService
    {
        void Save(Progress progress);
        Progress Load();
    }
}