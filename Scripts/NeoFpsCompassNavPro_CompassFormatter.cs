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
using System;

namespace NeoSaveGames.Serialization.Formatters
{
    public class NeoFpsCompassNavPro_CompassFormatter : NeoSerializationFormatter<CompassPro>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        static void Register()
        {
            NeoSerializationFormatters.RegisterFormatter(new NeoFpsCompassNavPro_CompassFormatter());
        }

        private static readonly NeoSerializationKey k_FogSizeKey = new NeoSerializationKey("fogSize");
        private static readonly NeoSerializationKey k_FogDataKey = new NeoSerializationKey("fogData");

        protected override void WriteProperties(INeoSerializer writer, CompassPro from, NeoSerializedGameObject nsgo)
        {
            if (from.fogOfWarEnabled)
            {
                writer.WriteValue(k_FogSizeKey, from.fogOfWarTextureSize);
                writer.WriteValues(k_FogDataKey, from.fogOfWarTextureData);
            }
        }

        protected override void ReadProperties(INeoDeserializer reader, CompassPro to, NeoSerializedGameObject nsgo)
        {
            int fogSize;
            if (reader.TryReadValue(k_FogSizeKey, out fogSize, 0))
            {
                Color32[] fogData;
                if (reader.TryReadValues(k_FogDataKey, out fogData, null))
                    to.StartCoroutine(DelayedSetData(to, fogData, fogSize));
            }
        }

        static IEnumerator DelayedSetData(CompassPro to, Color32[] data, int size)
        {
            Color32[] copy = new Color32[data.Length];
            Array.Copy(data, copy, data.Length);

            yield return null;

            to.fogOfWarTextureSize = size;
            to.fogOfWarTextureData = copy;
        }
}
}