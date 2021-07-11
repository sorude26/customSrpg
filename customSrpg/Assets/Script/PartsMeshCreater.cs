using UnityEngine;
using UnityEditor;

/// <summary>
/// メッシュを生成する
/// </summary>
public class PartsMeshCreater : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] string meshName = "default";
   public void Create()
    {
        if (meshName == "")
        {
            Debug.LogError("ファイル名を入力してください");
            return;
        }
        AssetDatabase.CreateAsset(meshFilter.mesh, "Assets/Material/Mesh/"+ meshName +".asset");
        AssetDatabase.SaveAssets();
    }
}
