using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Photon.Pun;


public class NewCharacterCreator : EditorWindow
{

    private GameObject newPlayerPrefab;
    private string localPath;
    private string windowsPath;
    private string newModelsPath = "Assets/3D_Models/";
    private string texturesPath = "Assets/3D_Models/textures/";

    private Vector3 setColliderPosition = new Vector3(0, 1, 0);
    private PhotonView photonView;

    [MenuItem ("Character Creator / Create new character" )]

    public static void ShowCreatorWindow()
    {
        GetWindow(typeof(NewCharacterCreator));
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.Label("Create new character", EditorStyles.boldLabel);
        GUILayout.Space(20);
        newPlayerPrefab = EditorGUILayout.ObjectField("Enter model", newPlayerPrefab, typeof(GameObject), false) as GameObject;
        GUILayout.Space(10);
        if (GUILayout.Button("Load model from file explorer"))
        {
            OpenWindowsExplorer();
        }
        GUILayout.Space(10);
        if (GUILayout.Button ("Load textures from file explorer"))
        {
            LoadTextures();
        }
        GUILayout.Space(30);
        if (GUILayout.Button ("CREATE PREFAB"))
        {
            PrefabCreator();
        }
    }

    private void OpenWindowsExplorer()
    {
        windowsPath = EditorUtility.OpenFilePanel("Select fbx model", "", "fbx");
        GetModelFronExplorer();
    }

    private void GetModelFronExplorer()
    {
        if (windowsPath != null)
        {
            string fileName = Path.GetFileName(windowsPath);
            File.Copy(windowsPath, Path.Combine(newModelsPath + fileName), true);
            AssetDatabase.Refresh();
            newPlayerPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newModelsPath + fileName, typeof(GameObject));

        }
    }

    private void LoadTextures()
    {
        windowsPath = EditorUtility.OpenFolderPanel("Select textures file", "", "");
        if (windowsPath != null)
        {
            string folderName = Path.GetDirectoryName(windowsPath + @"\");
            Debug.Log(folderName);
         
              Directory.CreateDirectory(folderName);
              string[] files = Directory.GetFiles(folderName);
              foreach (var newFiles in files)
                {
                    var filename = Path.GetFileName(newFiles);
                    var destFile = Path.Combine(texturesPath, filename);
                    File.Copy(newFiles, destFile, true);
                }

                AssetDatabase.Refresh();
        }
    }

    private void PrefabCreator()
    {
        localPath = "Assets/Resources/" + newPlayerPrefab.name + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        Instantiate(newPlayerPrefab);
        GameObject newPrefab = GameObject.Find(newPlayerPrefab.name + "(Clone)");
        newPrefab.name = newPlayerPrefab.name;
        
        PrefabUtility.SaveAsPrefabAsset(newPrefab, localPath);
        CreateLobbyModel(newPlayerPrefab.name);
        FillNewPrefab(newPlayerPrefab.name);

    }
    private void CreateLobbyModel(string newModelName)
    {
        GameObject modelinHierarchy = GameObject.Find(newModelName);

        GameObject characterSelector = GameObject.FindObjectOfType<CharacterSelector>().gameObject;
        modelinHierarchy.transform.parent = characterSelector.transform;
        modelinHierarchy.transform.localScale = new Vector3(4, 4, 4);
        modelinHierarchy.transform.position = new Vector3(2, -2, 0);
        modelinHierarchy.transform.Rotate(0, 180, 0);

        EnableAnimationController(modelinHierarchy);

        CharacterSelector characterSelectorScript = characterSelector.GetComponent<CharacterSelector>();
        characterSelectorScript.AddNewCharacterToList(modelinHierarchy);

        modelinHierarchy.SetActive(false);

    }

    private void FillNewPrefab(string newPrefabName)
    {
        GameObject newPrefab = Resources.Load(newPrefabName) as GameObject;
        newPrefab.AddComponent<CharacterController>();

        CharacterController characterController;
        characterController = newPrefab.GetComponent<CharacterController>();
        characterController.center = setColliderPosition;

        newPrefab.AddComponent<AnimatorController>();

        EnableAnimationController(newPrefab);

        photonView = newPrefab.AddComponent<PhotonView>();
        newPrefab.AddComponent<PhotonTransformView>();
        newPrefab.AddComponent<PhotonAnimatorView>();
        SynchroniceAnimations(newPrefab);
        
        photonView.FindObservables(true);
        photonView.GetInstanceID();

        newPrefab.AddComponent<ChatManager>();
    }

    private void EnableAnimationController(GameObject modelAnimator )
    {
        Animator modelAnim = modelAnimator.GetComponent<Animator>();
        modelAnim.runtimeAnimatorController = Resources.Load("BaseAnim/BaseNamimation") as RuntimeAnimatorController;
    }

    private void SynchroniceAnimations (GameObject objetSync)
    {
        PhotonAnimatorView photonAnimatorView = objetSync.GetComponent<PhotonAnimatorView>();
        photonAnimatorView.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("Speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);
        photonAnimatorView.SetParameterSynchronized("Direction", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);
    }

}
