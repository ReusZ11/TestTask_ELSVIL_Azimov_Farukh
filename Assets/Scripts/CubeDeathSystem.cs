using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDeathSystem : MonoBehaviour, ICubeDeath
{
    [Header("Cube settings")]
    public CubeType cubeType;
    public float destroyDelay = 0.5f;

    public static event Action<CubeType, Vector3> onCubeDied;

    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        onCubeDied?.Invoke(cubeType, transform.position);

        StartCoroutine(DeactivateAfterDelay());
    }

    public CubeType GetCubeType()
    {
        return cubeType;
    }

    private IEnumerator DeactivateAfterDelay()
    {
        Collider col = GetComponent<Collider>();
        if(col != null) col.enabled = false;

        yield return new WaitForSeconds(destroyDelay);

        if(col != null ) col.enabled = true;
        isDead= false;

        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        isDead = false;
    }

}

public interface ICubeDeath
{
    void Die();
    CubeType GetCubeType();
}

public enum CubeType
{
    Blue,
    Red,
    Any,
    None
}