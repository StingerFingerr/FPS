using System;
using Zenject;

namespace Infrastructure
{
    public interface ISceneProgressService: IInitializable, IDisposable
    {
        void InformProgressReaders();
        void SaveGame();
    }
}