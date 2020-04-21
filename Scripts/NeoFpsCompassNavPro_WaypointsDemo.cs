/*
 * Copyright 2020 Yondernauts Games Ltd
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using CompassNavigatorPro;
using NeoSaveGames.Serialization;
using NeoSaveGames;

namespace NeoFPS.CompassNavigatorPro
{
    [RequireComponent(typeof(NeoSerializedGameObject))]
    public class NeoFpsCompassNavPro_WaypointsDemo : MonoBehaviour, INeoSerializableComponent
    {
        public Vector3[] locations = new Vector3[0];
        public CompassProPOI poiPrefab = null;

        int poiNumber = -1;
        CompassPro compass = null;

        void Start()
        {
            // Get a reference to the Compass Pro Navigator component
            compass = CompassPro.instance;

            // Add a callback when POIs are reached
            compass.OnPOIVisited += POIVisited;

            // Add the first POI
            if (poiNumber == -1)
                AddRandomPOI();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                compass.POIShowBeacon(5f, 1.1f, 1f, new Color(1, 1, 0.25f));
            }
            if (Input.GetKey(KeyCode.Z))
            {
                compass.miniMapZoomLevel -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.X))
            {
                compass.miniMapZoomLevel += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                compass.showMiniMap = !compass.showMiniMap;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    compass.POIShowBeacon(hit.point, 5f, 1.1f, 1f, new Color(0, 0.5f, 1f));
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                compass.miniMapZoomState = !compass.miniMapZoomState;
            }
        }
        
        void AddRandomPOI()
        {
            // Instantiate waypoint
            var poi = GetComponent<NeoSerializedGameObject>().InstantiatePrefab(poiPrefab);

            // Get waypoint position
            int index = ++poiNumber;
            while (index >= locations.Length)
                index -= locations.Length;
            var position = locations[index];

            // Title name and reveal text
            poi.transform.position = position;
            poi.title = "Target " + (poiNumber).ToString();
            poi.titleVisibility = TITLE_VISIBILITY.Always;
            poi.visitedText = "Target " + poiNumber + " acquired!";
        }


        void POIVisited(CompassProPOI poi)
        {
            Debug.Log(poi.title + " has been reached.");
            StartCoroutine(RemovePOI(poi));
            AddRandomPOI();
        }

        IEnumerator RemovePOI(CompassProPOI poi)
        {
            while (poi.transform.position.y < 5)
            {
                poi.transform.position += Vector3.up * Time.deltaTime;
                poi.transform.localScale *= 0.9f;
                yield return new WaitForEndOfFrame();
            }
            Destroy(poi.gameObject);
        }

        private static readonly NeoSerializationKey k_PoiIndexKey = new NeoSerializationKey("poiIndex");
        
        public void WriteProperties(INeoSerializer writer, NeoSerializedGameObject nsgo, SaveMode saveMode)
        {
            writer.WriteValue(k_PoiIndexKey, poiNumber);
        }

        public void ReadProperties(INeoDeserializer reader, NeoSerializedGameObject nsgo)
        {
            reader.TryReadValue(k_PoiIndexKey, out poiNumber, poiNumber);
        }
    }
}