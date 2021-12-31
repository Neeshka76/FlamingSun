using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FlamingSun
{
    public class FlamingSunData
    {
        // Send if the Flaming Sun have to spawn
        public bool FlamingSunSpawnGetSet { get; set; }
        // Send if the Flaming Sun throw fireballs
        public bool FlamingSunThrowFireballsGetSet { get; set; }
        // Send if the Flaming Sun attack outside of waves
        public bool FlamingSunAttackOutsideWavesGetSet { get; set; }
        // Send if the Flaming Sun target the player
        public bool FlamingSunTargetPlayerGetSet { get; set; }
        // Send value of the Flaming Sun time idle for fireballs
        public float FlamingSunTimeIdleFireballsGetSet { get; set; }
        // Send value of the Flaming Sun time activate for fireballs
        public float FlamingSunTimeActivateFireballsGetSet { get; set; }
        // Send value of the Flaming Sun tick throw fireballs delay
        public float FlamingSunTickThrowFireballsDelayGetSet { get; set; }
        // Send value of the Flaming Sun number of fireballs per tick
        public uint FlamingSunNumFireballPerTickGetSet { get; set; }
        // Send value of the Flaming Sun chances of random throw
        public uint FlamingSunChancesOfRandomThrowGetSet { get; set; }
        // Send value of the Flaming Sun Radius of detection
        public uint FlamingSunRadiusOfDetectionGetSet { get; set; }

        // Set if Keyboard has pressed enter button finish
        public bool KeyboardFinishEnterButtonPressedGetSet { get; set; }
        // Set if the value to assign is a int
        public bool ValueToAssignIsInt { get; set; }
        // Set if the value to assign is a float
        public bool ValueToAssignIsFloat { get; set; }
        // Set if the value to assign is uint
        public bool ValueToAssignIsUint { get; set; }
        // Value to pass from the Keyboard in Uint
        public uint ValueToAssignedUintGetSet { get; set; }
        // Value to pass from the Keyboard in Int
        public int ValueToAssignedIntGetSet { get; set; }
        // Value to pass from the Keyboard in float
        public float ValueToAssignedFloatGetSet { get; set; }

        // Set if Player has pressed the button Time Idle Fireballs
        public bool FlamingSunTimeIdleFireballsButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button Time Activate Fireballs
        public bool FlamingSunTimeActivateFireballsButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button Tick throw fireballs delay
        public bool FlamingSunTickThrowFireballsDelayButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button number of fireballs per tick
        public bool FlamingSunNumFireballPerTickPressedGetSet { get; set; }
        // Set if Player has pressed the button chances of random throw
        public bool FlamingSunChancesOfRandomThrowButtonPressedGetSet { get; set; }
        // Set if Player has pressed the button radius of detection
        public bool FlamingSunRadiusOfDetectionButtonPressedGetSet { get; set; }
    }
    public class FlamingSunController : MonoBehaviour
    {
        public FlamingSunData data = new FlamingSunData();
    }
}
