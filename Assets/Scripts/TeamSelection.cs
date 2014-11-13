using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamSelection : MonoBehaviour
{
	public GameObject homeTeam;

	public GameObject teamTeam;
	
    private Texture2D targetTexture;

	private Texture2D sourceTexture;

    void Update()
    {
        //originalPixelMatrix = SetOriginalPixelMatrix(texture);

        if(Input.GetKeyUp(KeyCode.P))
        {
            PaintItBlack();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            PaintItBack();
        }
    }
	
    void Destroy()
    {
        //BackToOriginalPixelMatrix(texture, originalPixelMatrix);
    }

	private void PaintBaseTexture(Texture2D source)
	{
		targetTexture.SetPixels32(sourceTexture.GetPixels32());
		targetTexture.Apply();
	}
	
	private void PaintItBack()
    {
        targetTexture.SetPixels32(sourceTexture.GetPixels32());
		targetTexture.Apply();
    }

    private void PaintItBlack()
    {
        Color32[] texturePixels = targetTexture.GetPixels32();

        for (int i = 0; i < texturePixels.Length; i++)
        {
            if (texturePixels[i].a == 255)
            {
                texturePixels[i] = Color.black;
            }
        }

		targetTexture.SetPixels32(texturePixels);
		targetTexture.Apply();
    }

    private Color32[] SetOriginalPixelMatrix(Texture2D _texture)
    {
        return _texture.GetPixels32();
    }

    public void PaintSprite(string p)
    {
        if (p.Equals("black")) Paint();
    }

    public void Paint()
    {
        //SavePixelMatrix();
    }

    /*
    	void SetPixel(int ofx, int ofy)
	{
		Color pxlClr = texture.GetPixel(touchPosX + ofx, touchPosY + ofy);

		if (mixColors)
		{
			newMixedColor = new Vector4 
			(
				(0.5f * pxlClr.r + 0.5f * color.r),
				(0.5f * pxlClr.g + 0.5f * color.g),
				(0.5f * pxlClr.b + 0.5f * color.b),
				(1.0f)
			);
		}
		
		texture.SetPixel(touchPosX + ofx, touchPosY + ofy, mixColors ? newMixedColor : color);
	}
	#endregion Methods*/
}