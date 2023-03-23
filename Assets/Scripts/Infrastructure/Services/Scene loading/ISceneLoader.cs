using System;

namespace Scene_service
{
    public interface ISceneLoader
    {
       void LoadSceneAsync( string name, Action onLoaded = null);
    }
}