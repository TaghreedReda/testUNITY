using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrlHintMessage : MonoBehaviour
{

    public Font txtFont;
    public List<MeshFilter> Meshes;

    private void Awake()
    {
        foreach (MeshFilter AMesh in Meshes)
            AMesh.GetComponent<MeshRenderer>().material.mainTexture = this.txtFont.material.mainTexture;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator DisplayText()
    {
        CreateFont("");

        yield return null;
    }

    private void CreateFont(string AMsg)
    {
        this.txtFont.RequestCharactersInTexture(AMsg);

        for (int i = 0; i < AMsg.Length; i++ )
        {
            CharacterInfo AChar;

            this.txtFont.GetCharacterInfo(AMsg[i], out AChar);

            Vector2[] uvs = new Vector2[4];
            uvs[0] = AChar.uvBottomLeft;
            uvs[1] = AChar.uvTopRight;
            uvs[2] = AChar.uvBottomRight;
            uvs[3] = AChar.uvTopLeft;

            Meshes[i].mesh.uv = uvs;

            Vector3 AScale = Meshes[i].transform.localScale;
            AScale.x = AChar.glyphWidth * 0.02f;

            Meshes[i].transform.localScale = AScale;


        }
    }
}
