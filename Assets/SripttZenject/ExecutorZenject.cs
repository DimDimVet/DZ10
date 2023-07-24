using System;
using UnityEngine;

//исполнительный класс записи и загрузок
public class ExecutorZenject : IData
{
    public bool LoadDataFireBase(Firebase.Database.DataSnapshot dataSnapshot, out DataPlayer dataPlayerFireBase)//метод получения из FireBase
    {
        if (FireBaseTool.Snapshot != null)
        {
            dataPlayerFireBase = new DataPlayer
            {
                healtPlayer = Int32.Parse(dataSnapshot.Child("healtPlayer").GetValue(true).ToString()),
                shootCount = Int32.Parse(dataSnapshot.Child("shootCount").GetValue(true).ToString())
            };
            return true;
        }
        else
        {
            dataPlayerFireBase = new DataPlayer();
            return false;
        }
    }
    public bool LoadDataLocalBase(string hashKey, out DataPlayer dataPlayerLocal)//метод получения из LocalBase
    {
        if (PlayerPrefs.HasKey($"{hashKey}"))
        {
            string jsonString = PlayerPrefs.GetString($"{hashKey}");
            if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
            {
                dataPlayerLocal = JsonUtility.FromJson<DataPlayer>(jsonString);
                return true;
            }
            else
            {
                dataPlayerLocal = new DataPlayer();
                return false;
            }
        }
        else
        {
            dataPlayerLocal = new DataPlayer();
            return false;
        }
    }

    public void LoadDataDefault(out DataPlayer dataPlayerDefault)//метод получения Default
    {
        dataPlayerDefault = new DataPlayer();
    }

    public string SaveData(DataPlayer dataPlayer, string hashKey)//общий метод загрузки в хранилища
    {
        string jsonString = JsonUtility.ToJson(dataPlayer);
        Debug.Log(jsonString);
        //local
        PlayerPrefs.SetString(hashKey, jsonString);
        //FireBase
        FireBaseTool.SaveData(hashKey, jsonString);

        return $"healtPlayer={dataPlayer.healtPlayer} shootCount={dataPlayer.shootCount}";
    }
}
