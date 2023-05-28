using Game_logic;
using Zenject;

public interface IProgressService
{
    void InformProgressWritersForSave(DiContainer localDiContainer);
    void Save();
    bool Load();
    public Progress InitNewProgress();
    void ResetProgress();
    bool SaveExists();
    Progress Progress { get; }
}