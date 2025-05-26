using System;
using System.IO;
using UnityEngine;

public class SaveDataWitter
{
    public string saveDataDirectoryPath = "";
    public string dataSaveFileName = "";


    public CharacterSaveData LoadCharacterSaveDataFromJson()
    {
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);

        CharacterSaveData loadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";
                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        saveDataToLoad = reader.ReadToEnd();

                    }
                }

                loadedSaveData = JsonUtility.FromJson<CharacterSaveData>(saveDataToLoad);

            }
            catch (Exception ex)
            {

                Debug.LogError(ex.Message);
            }
        }
        else
        {
            Debug.Log(" SAVE FILE DOES NOT EXIST");
        }

        return loadedSaveData;
    }

    public void WriteCharacterDataToSaveFile(CharacterSaveData characterData)
    {
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            string dataToStore = JsonUtility.ToJson(characterData, true);
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR WHILE TRYING TO  SAVE DATA, GAME COULD NOT BE SAVED" + ex);
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, dataSaveFileName));
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, dataSaveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
