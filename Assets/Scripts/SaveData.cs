using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    private string currentStage;

    public SaveData(string _currentStage)
    {
        currentStage = _currentStage;
    }

    public string CurrentStage()
    {
        return currentStage;
    }
}
