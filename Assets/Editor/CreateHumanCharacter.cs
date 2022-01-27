using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

   public class CreateHumanCharacter : AssetPostprocessor
    {

        public void OnPreprocessModel()
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.animationType = ModelImporterAnimationType.Human;
        }


    }

   
