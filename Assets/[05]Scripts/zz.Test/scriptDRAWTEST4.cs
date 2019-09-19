using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptDRAWTEST4 : Graphic
{
    [SerializeField]
    Texture m_Texture;

    private int FtipCNT = 10;

    private List<Vector2> FPointList;

    protected float FlastWidth = 0, FlastHeight = 0;
    protected float gapWidth = 0;


    public int tipCNT
    {
        get
        {
            return this.FtipCNT;
        }

        set
        {
            this.FtipCNT = value;

            if (this.FtipCNT > 0)
            {


                this.gapWidth = this.rectTransform.rect.width / this.FtipCNT;

                Debug.Log(this.rectTransform.rect.width.ToString() +  ", " 
                        + this.rectTransform.rect.height.ToString() + ", "
                        + gapWidth.ToString());
            } else
            {
                this.gapWidth = this.rectTransform.rect.width;
            }
                
        }
    }

    public override Texture mainTexture
    {
        get
        {
            return m_Texture == null ? s_WhiteTexture : m_Texture;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.tipCNT = 10;
        this.FlastWidth = this.rectTransform.rect.width;
        this.FlastHeight = this.rectTransform.rect.height;
    }

    private void Update()
    {
        if (this.FlastWidth != this.rectTransform.rect.width )
        {
            Debug.Log("Width Changed >>" 
                + this.rectTransform.rect.width.ToString() + " , " 
                + this.rectTransform.rect.left.ToString() + " , "
                + (this.rectTransform.rect.width / 2).ToString()
                );

            this.FlastWidth = this.rectTransform.rect.width;
        }

        if (this.FlastHeight != this.rectTransform.rect.height)
        {
            Debug.Log("Height Changed >>" + this.rectTransform.rect.height.ToString());
            this.FlastHeight = this.rectTransform.rect.height;
        }

    }

    /// <summary>
    /// Texture to be used.
    /// </summary>
    public Texture texture
    {
        get
        {
            return m_Texture;
        }
        set
        {
            if (m_Texture == value)
                return;

            m_Texture = value;
            SetVerticesDirty();
            SetMaterialDirty();
        }
    }

    private float drawVertBeam(VertexHelper AHelper, float AXPos, float ABeamSize,  Color32 AColor, int AidxNo)
    {
        Vector3 AVector;
        float   Abottom;

        Abottom = (this.rectTransform.rect.height / 2) - 6;

        AVector = new Vector3(AXPos, 0 - Abottom);
        AHelper.AddVert(AVector, AColor, new Vector2(0f, 0f));
        AVector = new Vector3(AXPos, ABeamSize - Abottom); 
        AHelper.AddVert(AVector, AColor, new Vector2(0f, 0f));
        AVector = new Vector3(AXPos + 1, ABeamSize - Abottom);
        AHelper.AddVert(AVector, AColor, new Vector2(0f, 0f));

        AHelper.AddVert(new Vector3(AXPos + 1, ABeamSize - Abottom), AColor, new Vector2(0f, 0f));
        AHelper.AddVert(new Vector3(AXPos + 1, 0 - Abottom), AColor, new Vector2(0f, 0f));
        AHelper.AddVert(new Vector3(AXPos, 0 - Abottom), AColor, new Vector2(0f, 0f));

        AHelper.AddTriangle(AidxNo, AidxNo + 1, AidxNo + 2);
        AHelper.AddTriangle(AidxNo + 3, AidxNo + 4, AidxNo + 5);

        return (AXPos + 1);
    }


    // Start is called before the first frame update
    protected override void OnPopulateMesh(Mesh AMesh)
    {
        float AXPos = 0f;
        Color32 color32 = Color.black;
        int AidxNo = 0;

        //this.rectTransform.rect.width;

        using (var vh = new VertexHelper())
        {
            float AccWidth;
            float ARight = (this.rectTransform.rect.width / 2);

            AidxNo = 0; AXPos = -(this.rectTransform.rect.width / 2);
            AccWidth = AXPos;

            AccWidth = 0;
            while (AXPos < ARight )
            {
                AccWidth = AccWidth + this.drawVertBeam(vh, AXPos, 15, color32, AidxNo);

                AXPos  = AXPos  + 5;
                AidxNo = AidxNo + 6; 
            }

            vh.FillMesh(AMesh);
        }

    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(68, 72, 40, 20), "Text");
    }


}
