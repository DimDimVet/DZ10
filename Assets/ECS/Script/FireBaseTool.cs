using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public static class FireBaseTool /*: MonoBehaviour*/
{
    public static DataSnapshot Snapshot;
    private static DatabaseReference reference;//переменная удаленой БД

    public static void SaveData(string hashKey, string json)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;//инициализируем экземпляр
        reference.Child(hashKey).SetRawJsonValueAsync(json);
    }
    public static void LoadData(string hashKey)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;//инициализируем экземпляр

        FirebaseDatabase.DefaultInstance.GetReference(hashKey).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log($"Error{task}");
            }
            else if (task.IsCompleted)
            {
                Snapshot = task.Result;
            };
        });
    }
}
