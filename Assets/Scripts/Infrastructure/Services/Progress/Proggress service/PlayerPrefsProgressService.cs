using Game_logic;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class ProgressService: IProgressService
{
    public Progress Progress { get; private set; }
    
    private const string KEY = "Save";

    private readonly DiContainer _diContainer;

    public ProgressService (DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public bool Load()
    {
        if (SaveExists())
        {
            Progress = LoadProgress();
            return true;
        }

        return false;
    }

    public void Save() => 
        PlayerPrefs.SetString(KEY,  JsonConvert.SerializeObject(Progress));

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Progress = null;
    }

    public Progress InitNewProgress()
    {
        Progress = new Progress();
        PlayerPrefs.SetString(KEY,  JsonConvert.SerializeObject(Progress));
        return null;
    }

    private Progress LoadProgress()
    {
        Progress = (Progress) JsonConvert.DeserializeObject(PlayerPrefs.GetString(KEY), typeof(Progress));
        //_diContainer.Resolve<List<IProgressReader>>().ForEach(r => r.Load(Progress));
        return Progress;
    }

    public bool SaveExists() => 
        PlayerPrefs.HasKey(KEY);
}