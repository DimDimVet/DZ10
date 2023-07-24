using UnityEngine;

public class ModeComponent : MonoBehaviour, IModeComponent
{
    [SerializeField] private Renderer render;
    public float ModeDelay = 1f;
    private float modeTime = float.MinValue;
    public void Mode()
    {
        if (Time.time < modeTime + ModeDelay)
        {
            return;
        }
        else
        {
            modeTime = Time.time;
        }

        StMash();

    }

    private void StMash()
    {
        if (render.material.GetFloat("_Alfa") != 0)
        {
            render.material.SetFloat("_Alfa", 0);
        }
        else
        {
            render.material.SetFloat("_Alfa", 1);
        }
    }

}
