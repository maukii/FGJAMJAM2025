using UnityEngine;

public class BubbleDissolve : MonoBehaviour
{
	ParticleSystem ps;
	MeshRenderer mr;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
		mr = GetComponent<MeshRenderer>();
	}

	void Update()
	{
		float pos = ps.time / ps.main.duration;
		MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
		propertyBlock.SetFloat("_Dissolve", pos);
		mr.SetPropertyBlock(propertyBlock);
	}
}
