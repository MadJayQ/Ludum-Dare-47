using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerCloneState : BaseCloneState
{
    private MeshFilter clonedMesh;
    private static Material playerCloneMaterial;

    public const string PLAYER_CLONE_MATERIAL_ADDRESS = "CloneMaterial";

    public PlayerCloneState(Player src)
    {
        Rotation = src.Controller.Rotation;
        Origin = src.Controller.Origin;

        clonedMesh = src.MeshFilter;

        if (playerCloneMaterial == null)
        {
            playerCloneMaterial = Addressables.LoadAsset<Material>(PLAYER_CLONE_MATERIAL_ADDRESS).Result;
        }
    }

    public static IEnumerator LoadAssets()
    {
        var loadAssetRoutine = Addressables.LoadAsset<Material>(PLAYER_CLONE_MATERIAL_ADDRESS);
        yield return loadAssetRoutine;
        Debug.Assert(loadAssetRoutine.Status == AsyncOperationStatus.Succeeded, "Failed to load player clone material");
        playerCloneMaterial = loadAssetRoutine.Result;
    }

    public override GameObject CreateGameObject()
    {
        GameObject cloneObject = new GameObject("PlayerClone");
        cloneObject.transform.position = Origin;
        cloneObject.transform.rotation = Rotation;
        cloneObject.layer = LayerMask.NameToLayer("Clones");

        MeshRenderer cloneMeshRenderer = cloneObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = cloneObject.AddComponent<MeshFilter>();
        meshFilter.mesh = clonedMesh.mesh;

        Rigidbody cloneRigidBody = cloneObject.AddComponent<Rigidbody>();
        cloneRigidBody.isKinematic = true;
        cloneRigidBody.useGravity = false;

        cloneMeshRenderer.material = playerCloneMaterial;

        cloneObject.AddComponent<CapsuleCollider>();
        cloneObject.AddComponent<CloneTrigger>(); 


        return cloneObject;
    }
}