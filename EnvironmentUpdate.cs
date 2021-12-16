using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class EnvironmentUpdate : MonoBehaviour {
	public BackgroundEnvironmentInfo BackgoundInfo = new BackgroundEnvironmentInfo();

	public CharacterEnvironmentInfo CharacterInfo = new CharacterEnvironmentInfo();

	private List<GameObject> _rootObjects;

	private Texture2D _defaultMatcap = null;

	// Use this for initialization
	void Start () {
		_defaultMatcap =  new Texture2D (1, 1);
		_defaultMatcap.SetPixel (0, 0, Color.white);
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		//GetSceneRoot ();
		BackgoundInfo.CheckDirty();
		CharacterInfo.CheckDirty();

		if ( BackgoundInfo.IsDirty == true || CharacterInfo.IsDirty == true) 
			GetSceneRoot();

		UpdateMaterial ();
		#endif
	}

	void OnEnable(){
		GetSceneRoot ();
		BackgoundInfo.CheckDirty();
		CharacterInfo.CheckDirty();
		UpdateMaterial ();
	}

	void GetSceneRoot(){
		_rootObjects = new List<GameObject>();
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (obj.transform.parent == null)
			{
				_rootObjects.Add(obj);
			}
		}
	}

	void UpdateMaterial(){
		if (BackgoundInfo.IsDirty == true) {

            Shader.SetGlobalColor("_FogColor", BackgoundInfo.FogColor);
            Shader.SetGlobalVector("_FogInfo", BackgoundInfo.FogInfo);
            Shader.SetGlobalVector("_LightMapInfo", BackgoundInfo.LightmapInfo);

            foreach (GameObject root in _rootObjects)
            {
                Renderer[] renderers = root.GetComponentsInChildren<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    foreach (Material m in renderer.sharedMaterials)
                    {
                        if (m == null)
                        {
                            Debug.LogWarning("renderer sharedMaterial is null => " + renderer.ToString());
                            continue;
                        }

                        if (m.shader.name == "SCShader/SCLightmap")
                        {
                            m.SetColor("_Color", BackgoundInfo.Color);
                        }
                    }
                }
            }

            BackgoundInfo.IsDirty = false;
		}

		if (CharacterInfo.IsDirty == true) {
			if (CharacterInfo.MatCap != null) {
				Shader.SetGlobalTexture ("_MatCap", CharacterInfo.MatCap);
			} else {
				Shader.SetGlobalTexture ("_MatCap", _defaultMatcap);
			}

			Shader.SetGlobalVector("_AmbientInfo", CharacterInfo.AmbientInfo);

			CharacterInfo.IsDirty = false;
		}
	}
}

[System.Serializable]
public class BackgroundEnvironmentInfo
{
	public Color FogColor = new Color( 0.2f, 0.3f, 0.7f, 1.0f);
	public float FogHeight = 0.0f;
	public float FogDensity = 1.0f;
	
	public float LightMapPower = 1.0f;
	public float LightMapIntensity = 1.0f;
	public Color Color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	private Color _fogColor = new Color( 0.2f, 0.3f, 0.7f, 1.0f);
	private Color _color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	
	private Vector4 _fogInfo = new Vector4 (0.0f, 1.0f, 0.0f, 0.0f);
	public Vector4 FogInfo{
		get{
			return _fogInfo;
		}
	}

	private Vector4 _lightmapInfo = new Vector4 (1.0f, 1.0f, 0.0f, 0.0f);
	public Vector4 LightmapInfo{
		get{
			return _lightmapInfo;
		}
	}

	private bool _isDirty = true;
	public bool IsDirty{
		get{
			return _isDirty;
		}
		set{
			if ( value != _isDirty){
				_isDirty = value;
			}
		}
	}

	public void CheckDirty(){
		if (_fogColor != FogColor) _isDirty = true;
		if (_fogInfo.x != FogHeight) _isDirty = true;
		if (_fogInfo.y != FogDensity) _isDirty = true;
		if (_lightmapInfo.x != LightMapPower) _isDirty = true;
		if (_lightmapInfo.y != LightMapIntensity) _isDirty = true;
		if (_color != Color) _isDirty = true;

		if (_isDirty) {
			_fogColor = FogColor;
			
			_fogInfo.x = FogHeight;
			_fogInfo.y = FogDensity;
			
			_lightmapInfo.x = LightMapPower;
			_lightmapInfo.y = LightMapIntensity;
			
			_color = Color;
		}
	}
}

[System.Serializable]
public class CharacterEnvironmentInfo
{
	public Texture2D MatCap;
	public float AmbientUpperIntensity = 1.0f;
	public float AmbientLowerIntensity = 1.0f;

	private Texture2D _matCap = null;
	private Vector4 _ambientInfo = new Vector4 (1.0f, 1.0f, 0.0f, 0.0f);
	public Vector4 AmbientInfo{
		get{
			return _ambientInfo;
		}
	}

	private bool _isDirty = true;
	public bool IsDirty{
		get{
			return _isDirty;
		}
		set{
			if ( value != _isDirty){
				_isDirty = value;
			}
		}
	}
	
	public void CheckDirty(){
		if (_matCap != MatCap) _isDirty = true;
		if (_ambientInfo.x != AmbientUpperIntensity) _isDirty = true;
		if (_ambientInfo.y != AmbientLowerIntensity) _isDirty = true;
		
		if (_isDirty) {
			_matCap = MatCap;
			_ambientInfo.x = AmbientUpperIntensity;
			_ambientInfo.y = AmbientLowerIntensity;
		}
	}
}
