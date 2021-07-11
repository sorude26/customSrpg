using UnityEngine;
using UnityEditor;

/// <summary>
/// メッシュを生成する
/// </summary>
public class PartsMeshCreater : MonoBehaviour
{
    [Tooltip("メッシュを生成したいオブジェクト")]
    [SerializeField] MeshFilter m_meshFilter;
    [Tooltip("メッシュの名前")]
    [SerializeField] string m_meshName = "default";
    /// <summary>
    /// メッシュを生成する
    /// </summary>
   public void CreateMesh()
    {
        if (m_meshName == "")
        {
            Debug.LogError("ファイル名を入力してください");
            return;
        }
        AssetDatabase.CreateAsset(m_meshFilter.mesh, "Assets/Material/Mesh/"+ m_meshName +".asset");
        AssetDatabase.SaveAssets();
    }
}
