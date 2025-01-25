using UnityEngine;

public class BubbleDissolve : MonoBehaviour
{
	ParticleSystem ps;
	MeshRenderer mr;
	float fresnel;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		mr = GetComponent<MeshRenderer>();

		fresnel = mr.materials[0].GetFloat("_FresnelStrength");
	}

	void Update()
	{
		float pos = ps.time / ps.main.duration;
		MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
		propertyBlock.SetFloat("_Dissolve", pos);
		propertyBlock.SetFloat("_FresnelStrength", Mathf.Lerp(fresnel, 0.0f, pos));
		mr.SetPropertyBlock(propertyBlock);
	}
}
