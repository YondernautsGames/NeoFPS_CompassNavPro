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
using UnityEngine.UI;
using NeoFPS.SinglePlayer;
using CompassNavigatorPro;

namespace NeoFPS
{
    [RequireComponent(typeof(CompassPro))]
	public class NeoFpsCompassNavPro_CharacterWatcher : PlayerCharacterHudBase
    {
        CompassPro m_CompassPro = null;

        protected override void Awake()
        {
            base.Awake();
            m_CompassPro = GetComponent<CompassPro>();
        }

        public override void OnPlayerCharacterChanged(ICharacter character)
        {
            bool found = false;
            if (character as Component != null)
            {
                var fpCamera = character.fpCamera as FirstPersonCamera;
                if (fpCamera != null)
                {
                    m_CompassPro.cameraMain = fpCamera.unityCamera;
                    m_CompassPro.miniMapFollow = character.transform;
                    m_CompassPro.FadeIn(1f);
                    found = true;
                }
            }

            if (!found)
            {
                m_CompassPro.cameraMain = Camera.main;
                m_CompassPro.FadeOut(1f);
            }
		}
	}
}