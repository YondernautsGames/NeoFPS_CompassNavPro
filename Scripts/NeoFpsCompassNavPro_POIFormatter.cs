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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompassNavigatorPro;

namespace NeoSaveGames.Serialization.Formatters
{
    public class NeoFpsCompassNavPro_POIFormatter : NeoSerializationFormatter<CompassProPOI>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        static void Register()
        {
            NeoSerializationFormatters.RegisterFormatter(new NeoFpsCompassNavPro_POIFormatter());
        }

        private static readonly NeoSerializationKey k_HeartbeatKey = new NeoSerializationKey("heartbeat");
        private static readonly NeoSerializationKey k_VisitedKey = new NeoSerializationKey("visited");

        protected override void WriteProperties(INeoSerializer writer, CompassProPOI from, NeoSerializedGameObject nsgo)
        {
            Debug.Log("Saving POI");

            writer.WriteValue(k_VisitedKey, from.isVisited);
            writer.WriteValue(k_HeartbeatKey, from.heartbeatIsActive);
        }

        protected override void ReadProperties(INeoDeserializer reader, CompassProPOI to, NeoSerializedGameObject nsgo)
        {
            Debug.Log("Loading POI");

            reader.TryReadValue(k_VisitedKey, out to.isVisited, to.isVisited);

            bool heartbeat;
            if (reader.TryReadValue(k_HeartbeatKey, out heartbeat, false))
            {
                if (!to.heartbeatIsActive)
                    to.StartHeartbeat();
            }
        }
    }
}