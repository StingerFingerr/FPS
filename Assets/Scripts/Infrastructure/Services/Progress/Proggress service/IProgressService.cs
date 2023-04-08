using Game_logic;

public interface IProgressService
{
    void Save();
    bool Load();
    public Progress InitNewProgress();
    void ResetProgress();
    bool SaveExists();
    Progress Progress { get; }
}