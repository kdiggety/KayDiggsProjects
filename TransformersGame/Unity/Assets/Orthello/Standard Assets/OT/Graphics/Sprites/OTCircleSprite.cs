using UnityEngine;
using System.Collections;
/// <summary>
/// A circular sprite that consists of a number of divisions and rings.
/// </summary>
public class OTCircleSprite : OTSprite {	
	
	public int _divisions = 32;
	int divisions_ = 32;
	public int divisions
	{
		get
		{
			return _divisions;
		}
		set
		{
			_divisions = value;
			divisions_ = value;
			meshDirty = true;
			Update();
		}
	}
	
	public int _rings = 1;
	int rings_ = 1;
	public int rings
	{
		get
		{
			return _rings;
		}
		set
		{
			_rings = value;
			rings_ = value;
			meshDirty = true;
			Update();
		}
	}
	
	public float _fillFactor = 1;
	float fillFactor_ = 1;
	public float fillFactor
	{
		get
		{
			return _fillFactor;
		}
		set
		{
			_fillFactor = value;
			fillFactor_ = value;
			meshDirty = true;
			Update();
		}
	}
	
	public Color[] _colors;
	public Color[] colors_;
	public Color[] colors
	{
		get
		{
			return _colors;
		}
		set
		{
			_colors = value;
			colors_ = value;
			SetColors();
		}
	}

	void SetColors()
	{
	}
	
	bool ColorsChanged()
	{
		return false;
	}
		
	protected override void CheckSettings ()
	{
		base.CheckSettings ();
					
		if (_divisions!=divisions_ || _rings!=rings_ || _fillFactor != fillFactor_)
		{			
			meshDirty = true;
			divisions_ = _divisions;
			rings_ = _rings;
			fillFactor_ = _fillFactor;
		}
		
		if (ColorsChanged())
			SetColors();
		
	}
	
	protected override string GetTypeName ()
	{
        return "CircleSprite";
	}	
	
	// Use this for initialization
	new protected void Awake () {
		divisions_ = _divisions;
		rings_ = _rings;
		fillFactor_ = _fillFactor;
		colors_ = _colors;
		base.Awake();	
	}

	void AdjustUV()
	{
	}	
	
	protected override void HandleUV ()
	{
		base.HandleUV();
		AdjustUV();
	}
	
	protected override Mesh GetMesh ()
	{
		Mesh mesh = base.GetMesh();
				
		if (divisions>0 && rings>0 && fillFactor>0)
		{
			AdjustUV();			
			return mesh;
		}
		else
		{
			mesh.triangles = new int[] { };
			mesh.uv = new Vector2[] { };
			mesh.colors = new Color[] { };
			mesh.vertices = new Vector3[] { };
			return mesh;
		}		
	}
	
	
					
}
