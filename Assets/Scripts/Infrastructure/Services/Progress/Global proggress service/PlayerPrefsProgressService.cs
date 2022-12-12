using Game_logic;
using UnityEngine;

namespace Infrastructure
{
    public class ProgressService: IProgressService
    {
        private const string KEY = "Save";

        public void Save(Progress progress) => 
            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(progress));

        public Progress Load()
        {
            if (SaveExists())
                return LoadProgress();
            else
                return InitNewProgress();
        }

        private Progress InitNewProgress()
        {
            Progress progress = new Progress();
            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(progress));
            return null;
        }

        private Progress LoadProgress() => 
            JsonUtility.FromJson<Progress>(PlayerPrefs.GetString(KEY));

        private bool SaveExists() => 
            PlayerPrefs.HasKey(KEY);
    }
}