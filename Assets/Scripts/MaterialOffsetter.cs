using UnityEngine;

public class MaterialOffsetter : MonoBehaviour
{

    [SerializeField] bool offsetX;
    [SerializeField] bool offsetY;

    [SerializeField] float speed;
    [SerializeField] Material mat;

    float x = 0;
    float y = 0;
    void Update()
    {
        if(offsetX)
            x += Time.deltaTime * speed;
        if (offsetY)
            y += Time.deltaTime * speed;

        mat.SetTextureOffset("_MainTex", new Vector2(x,y));
    }
}
