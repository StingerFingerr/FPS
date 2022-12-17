using System;
using System.Collections.Generic;
using Game_logic;
using UnityEngine;

namespace Infrastructure
{
    public class SceneProgressService : ISceneProgressService, IDisposable
    {
        private IProgressService _progressService;
        private List<IProgressReader> _progressReaders;
        private List<IProgressWriter> _progressWriters;

        private Progress _progress;
        
        public SceneProgressService ( 
            IProgressService progressService, 
            List<IProgressReader> progressReaders,
            List<IProgressWriter> progressWriters)
        {
            _progressService = progressService;
            _progressReaders = progressReaders;
            _progressWriters = progressWriters;
        }
        
        public void InformProgressReaders()
        {
            if (LoadProgressIfRequired())            
                _progressReaders.ForEach(r => r.Load(_progress));
        }

        public void SaveGame()
        {
            if (LoadProgressIfRequired())
            {
                _progressWriters.ForEach(w => w.Save(_progress));
                _progressService.Save(_progress);
            }
            
        }

        private bool LoadProgressIfRequired()
        {
            _progress ??= _progressService.Load();
            return _progress is not null;
        }

        public void Initialize() => 
            InformProgressReaders();

        public void Dispose()
        {
            //SaveGame();
        }
    }
}