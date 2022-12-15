using System;
using Zenject;

namespace Infrastructure
{
    public interface ISceneProgressService: IInitializable
    {
        void InformProgressReaders();
        void SaveGame();
    }
}