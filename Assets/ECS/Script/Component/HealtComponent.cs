using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HealtComponent : MonoBehaviour
{
    public Settings SettingsData;
    [HideInInspector]public int Healt=0;
    [HideInInspector] public bool Dead = false;
    [SerializeField] private Text text;
    public bool statusEnemy;
    private void Start()
    {
        StartCoroutine(Example());
    }

    private IEnumerator Example()
    {
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        DataStart();
    }
    private void DataStart()
    {
        if (Healt <= 0)
        {
            Healt = SettingsData.HealtPlayer;
        }

        text.text = $"Healt = {Healt}";
    }

    public void HealtContoll(int damage)
    {
        Healt -= damage;
        text.text = $"Healt = {Healt}";
        if (Healt<=0)
        {
            Dead = true;
        }

    }
}
