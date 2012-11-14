using UnityEngine;
using System.Collections;

public class WebCamMain : MonoBehaviour {
	
	
	GUITexture ShowContentTexture;
	
	WebCamDevice[]  devices ;
	WebCamTexture frontTex;
		
	string frontCamName;
	string backCamName;
	
	int Cam_width;
	int  Cam_height;	
	int  framesPerSec=30;	
	
	 Texture2D Cube_Texture;
	bool bOnOffDisplay;

	#region  Image Pixel Processing  manually
	    //private Texture2D texture;
    	private Color[] pixels;
	
	public GameObject PixelCubePrefab;
	public Transform root_PixelCube;
	
	private GameObject[] PixelObjArray;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		devices  =WebCamTexture.devices;

		Cam_width=400;		
		Cam_height = 300;

		bOnOffDisplay = true;
		
		if( devices!= null &&   devices.Length >0 )
		{
			frontTex = new WebCamTexture(devices[0].name, Cam_width, Cam_height, framesPerSec);
			ShowContentTexture = GameObject.Find("WebCamDisplayTexture").guiTexture;
			ShowContentTexture.texture  = frontTex;
			ShowContentTexture.pixelInset =new  Rect (   (float) (Cam_width/2.0*(-1)), (float)( Cam_height/2.0*(-1)), (float)Cam_width, (float)Cam_height); 
			frontTex.Play();			
		}
		else
		{
			Debug.LogError("faill");
		}
		
		pixels = frontTex.GetPixels(0,0,Cam_width,Cam_height);
		
		
		//Debug.Log(pixels[0]);
		
		// init the blocks		
		for(int i =0 ; i < Cam_width/50 ; i++)
		{
			for(int j =0 ; j < Cam_width/50 ; j++)
			{
				GameObject newObj = (GameObject)Instantiate( PixelCubePrefab, new Vector3(  i*0.5f, j*0.5f,0f),Quaternion.identity); 
				newObj.name = "Pixel_"+i+"_"+j;
				newObj.transform.parent = root_PixelCube;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		for( int i = ShowContentTexture.texture.width -1 ; i >=0; i--)
		{
			for( int j = ShowContentTexture.texture.height -1 ; j >=0; j--)	
			{
				//ShowContentTexture.guiTexture.texture
				
			}
			
		}
		
		
		for(int i =0 ; i < Cam_width/50 ; i++)
		{
			for(int j =0 ; j < Cam_width/50 ; j++)
			{
				//Debug.Log(pixels[ i + Cam_width*j]);
				 //  pixels[ i + Cam_width*j];
				//Debug.Log("pxl="+frontTex.GetPixel(i,j));
				Transform obj =  root_PixelCube.GetChild( i + Cam_width/50 *j)  ;
				
				obj.gameObject.renderer.material.color= frontTex.GetPixel(i,j);
				//obj.position = new Vector3(obj.position.x,obj.position.y,    (float)obj.gameObject.renderer.material.color.r );
				obj.position = new Vector3(obj.position.x,obj.position.y,    (float)frontTex.GetPixel(i,j).r*2 );
			}
		}		
		
		  
		
	}
	
	
	
	void OnGUI()
	{
		if( GUI.Button( new Rect(0,0,100,30), "on/close"))
		{
			bOnOffDisplay = !bOnOffDisplay;
			ShowContentTexture.enabled = bOnOffDisplay;
		}
		
		
	}
	
	
	
}
