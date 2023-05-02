using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Data gameData;
    private static string dataFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");

    public DataManager(int bestScore = 0)
    {
        gameData = new Data();
        gameData.bestScore = bestScore;
    }

   
    public void SetBestScore(int bestScore)
    {
        if (gameData == null)
        {
            gameData = new Data();
        }

        gameData.bestScore = bestScore;
    }

    // The method to return the loaded game data when needed
    public Data GetGameData()
    {
        if (gameData == null)
        {
            gameData = new Data();

            gameData.bestScore = 0;
        }

        return gameData;
    }

    public void Save()
    {
        // This creates a new StreamWriter to write to a specific file path
        using (StreamWriter writer = new StreamWriter(dataFilePath))
        {
            // This will convert our Data object into a string of JSON
            string dataToWrite = JsonUtility.ToJson(gameData);

            // This is where we actually write to the file
            writer.Write(dataToWrite);
        }
    }

    public void Load()
    {
        if (!System.IO.File.Exists(dataFilePath))
        {
            return;
        }
            // This creates a StreamReader, which allows us to read the data from the specified file path
            using (StreamReader reader = new StreamReader(dataFilePath))
        {
            // We read in the file as a string
            string dataToLoad = reader.ReadToEnd();

            // Here we convert the JSON formatted string into an actual Object in memory
            gameData = JsonUtility.FromJson<Data>(dataToLoad);
        }
    }

    [System.Serializable]
    public class Data
    {
        // The actual data we want to save goes here, for this example we'll only use an integer to represent best score
        public int bestScore = 0;
    }
}
