using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject mainTexture;
	public Camera cam;
	SpriteRenderer uiTex;
	Texture2D tex;
	int mWidth;
	int mHeight;
	int brushSize;
	Vector3 centerScreenPos;


	// Use this for initialization
	void Start () {
		// get mainTexture and width, Heigth of the texture
		if(mainTexture!=null)
		{
			uiTex=mainTexture.GetComponent<SpriteRenderer>();
		}
		tex = (Texture2D)uiTex.sprite.texture;

		mWidth=tex.width;
		mHeight=tex.height;

		Debug.Log(mHeight);
		Debug.Log(mWidth);

		brushSize=100;
		// cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		// if MouseDown then run CheckPoint with the mousePosition
		// if(Input.GetMouseButtonDown(0))
		{
			Debug.Log(CheckPoint(Input.mousePosition));
		}
	}
	bool CheckPoint(Vector3 pScreenPos)
	{	
		// get worldPosition from the mousePosition
		Vector3 worldPos=cam.ScreenToWorldPoint(pScreenPos);

		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position); // 目的获取z，在Start方法  
		Vector3 mousePos = pScreenPos;  
		mousePos.z = screenPos.z; // 这个很关键  
		worldPos = Camera.main.ScreenToWorldPoint(mousePos);  

		// get localPosition from the worldPosition
		Vector3 localPos=uiTex.transform.InverseTransformPoint(worldPos);
		// Test Position
		localPos.x=100*worldPos.x;
		localPos.y=100*worldPos.y;

		Debug.Log(pScreenPos);
		Debug.Log(worldPos);
		Debug.Log(localPos);
		// localPos=pScreenPos;
		// localPos.x-=mWidth/2;
		// localPos.y-=mHeight/2;

		Color col;
		// if localPosition is in the Texture
		if (localPos.x>-mWidth/2 && localPos.x<mWidth/2 && localPos.y>-mHeight/2 && localPos.y<mHeight/2)
		{
			// loop every pixel in the brush area
			for (int i=(int)localPos.x-brushSize;i<(int)localPos.x+brushSize;i++)
			{
				for(int j=(int)localPos.y-brushSize;j<(int)localPos.y+brushSize;j++)
				{
					// if the point is out of the brush circle area, continue
					if(Mathf.Pow(i-localPos.x,2)+Mathf.Pow(j-localPos.y,2)>Mathf.Pow(brushSize,2)) continue;
					// change the alpha channel of the pixel to make it transparent
					col=tex.GetPixel(i+(int)mWidth/2,j+(int)mHeight/2);
					col.a=0.0f;
					tex.SetPixel(i+(int)mWidth/2,j+(int)mHeight/2,col);
				}
			}
			// apply the change
			tex.Apply();
			return true;
		}
		return false;
	}
}
