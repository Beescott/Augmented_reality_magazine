//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//
// This file has been modified by Alexandre NGUYEN
// In order to : factorize the code
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class AugmentedImageVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        public GameObject[] models_array;

        private Dictionary<string, GameObject> models;

        public void Start()
        {
            //Load all models into a dictionnary
            models = new Dictionary<string, GameObject>();
            foreach (GameObject go in models_array)
            {
                models.Add(go.name, go);
            }
        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                UnsetActiveObjects("");
                return;
            }

            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;

            if (Image.Name == "Frame")
            {
                models["FrameLowerLeft"].transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
                models["FrameLowerRight"].transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
                models["FrameUpperLeft"].transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
                models["FrameUpperRight"].transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

                UnsetActiveObjects("Frame");
            }
            else if (Image.Name == "Bed")
            {
                models["Bed"].transform.localPosition = Vector3.zero;

                UnsetActiveObjects("Bed");
            }
            else if (Image.Name == "Table")
            {
                models["Table"].transform.localPosition = Vector3.zero;

                UnsetActiveObjects("Table");
            }
            else if (Image.Name == "Couch")
            {
                models["Couch"].transform.localPosition = Vector3.zero;

                UnsetActiveObjects("Couch");
            }
            else if (Image.Name == "Sofa")
            {
                models["Sofa"].transform.localPosition = Vector3.zero;

                UnsetActiveObjects("Sofa");
            }
        }

        private void UnsetActiveObjects(string imageName)
        {
            
            // We add in a list all the objects we want to keep
            List<String> keys = new List<String>();
            if (imageName == "Frame")
            {
                keys.Add("FrameLowerLeft");
                keys.Add("FrameLowerRight");
                keys.Add("FrameUpperLeft");
                keys.Add("FrameUpperRight");
            }
            else if (imageName == "Bed")
            {
                keys.Add("Bed");
            }
            else if (imageName == "Couch")
            {
                keys.Add("Couch");
            }
            else if (imageName == "Table")
            {
                keys.Add("Table");
            }
            else if (imageName == "Sofa")
            {
                keys.Add("Sofa");
            }

            // For all objects in the dictionnary, if not in the keys
            // we want to keep, setActive at false
            foreach (KeyValuePair<string, GameObject> kvp in models)
            {
                if (!keys.Contains(kvp.Key))
                    kvp.Value.SetActive(false);
                else
                    kvp.Value.SetActive(true);
            }
        }
    }
}
