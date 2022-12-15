using Game_logic;
using UnityEngine;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class ProgressService: IProgressService
    {
        private const string KEY = "Save";

        public Progress Load()
        {
            if (SaveExists())
                return LoadProgress();
            else
                return InitNewProgress();
        }

        public void Save(Progress progress) => 
            PlayerPrefs.SetString(KEY,  JsonConvert.SerializeObject(progress));

        private Progress InitNewProgress()
        {
            PlayerPrefs.SetString(KEY,  JsonConvert.SerializeObject(new Progress()));
            return null;
        }

        private Progress LoadProgress() => 
           (Progress) JsonConvert.DeserializeObject(PlayerPrefs.GetString(KEY), typeof(Progress));

        private bool SaveExists() => 
            PlayerPrefs.HasKey(KEY);
    }
}