public interface IData
{
    //void ZenjectData();
    //void ZenjectData1();
    //void ZenjectData2(int x,out string dataE);
    //
    string SaveData(DataPlayer dataPlayer, string hashKey);
    bool LoadDataFireBase(Firebase.Database.DataSnapshot dataSnapshot, out DataPlayer dataPlayerFireBase);
    bool LoadDataLocalBase(string hashKey, out DataPlayer dataPlayerLocal);
    void LoadDataDefault(out DataPlayer dataPlayerDefault);
}
