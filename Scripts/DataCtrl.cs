using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;

    public GameData data;

    string filePathName;

    BinaryFormatter bf;

    public bool devMODE;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        bf = new BinaryFormatter();

        filePathName = Application.persistentDataPath + "/game.dat";

        Debug.Log(filePathName);

    }


   public void LoadData()
    {
        if(File.Exists(filePathName))
        {
            FileStream fs = new FileStream(filePathName, FileMode.Open);

            data = (GameData) bf.Deserialize(fs);
            fs.Close();

            Debug.Log("Data Loaded");
        }
    }
    public void SaveData()
    {
        FileStream fs = new FileStream(filePathName, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }
    public void SaveData(GameData data)
    {
        FileStream fs = new FileStream(filePathName, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }

    public bool isUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].unlocked;
    }

    private void OnEnable()
    {
        //LoadData();//oyun çağırıldığında güncel bilgiler oyuna kaydedilmiş olacak.
        DatabaseControl();

    }
    void DatabaseControl()
    {
        if (!File.Exists(filePathName))
        {
            #if UNITY_ANDROID

            DatabaseCopy();

            #endif
        }
        else
        {

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                string destinationFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");

                File.Delete(destinationFile);

                File.Copy(filePathName,destinationFile);
            }


            if (devMODE)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    File.Delete(filePathName);
                    DatabaseCopy();
                }
            }
            LoadData();
        }
    }

    void DatabaseCopy()
    {

        string file = Path.Combine(Application.streamingAssetsPath, "game.dat");
        WWW data = new WWW(file);

        while (!data.isDone)
        {
            
        }

         File.WriteAllBytes(filePathName, data.bytes);

        LoadData();
    }
}

