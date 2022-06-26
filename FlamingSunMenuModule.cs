using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace FlamingSun
{
    public class FlamingSunMenuModule : MenuModule
    {
        private Text txtSpawnSun;
        private Text txtThrowFireballs;
        private Text txtAttackOutsideWave;
        private Text txtTargetPlayer;
        private Text txtTimeIdleFireballs;
        private Text txtTimeActivateFireballs;
        private Text txtTickThrowDelay;
        private Text txtNumFireballsPerTick;
        private Text txtChancesRandomThrow;
        private Text txtRadiusDetection;
        private Button btnSpawnSun;
        private Button btnThrowFireballs;
        private Button btnAttackOutsideWave;
        private Button btnTargetPlayer;
        private Button btnTimeIdleFireballs;
        private Button btnTimeActivateFireballs;
        private Button btnTickThrowDelay;
        private Button btnNumFireballsPerTick;
        private Button btnChancesRandomThrow;
        private Button btnRadiusDetection;

        public bool allowSpawn;
        public bool sunThrowFireballs;
        public bool fireballsOutsideOfCombatWave;
        public bool sunTargetsPlayer;
        public float timerIdleWaveOfFireballs;
        public float timerActivateWaveOfFireballs;
        public float timerTickThrowDelay;
        public uint numOfFireballsPerTick;
        public uint chancesOfRandomThrow;
        public uint radiusOfDetectionCreaturesByFlamingSun;

        public FlamingSunController flamingSunController;
        public FlamingSunHook flamingSunHook;

        public override void Init(MenuData menuData, Menu menu)
        {
            base.Init(menuData, menu);


            // Grab the value from Unity
            txtSpawnSun = menu.GetCustomReference("txt_SpawnSun").GetComponent<Text>();
            txtThrowFireballs = menu.GetCustomReference("txt_ThrowFireballs").GetComponent<Text>();
            txtAttackOutsideWave = menu.GetCustomReference("txt_AttackOutsideWave").GetComponent<Text>();
            txtTargetPlayer = menu.GetCustomReference("txt_TargetPlayer").GetComponent<Text>();
            txtTimeIdleFireballs = menu.GetCustomReference("txt_TimeIdleFireballs").GetComponent<Text>();
            txtTimeActivateFireballs = menu.GetCustomReference("txt_TimeActivateFireballs").GetComponent<Text>();
            txtTickThrowDelay = menu.GetCustomReference("txt_TickThrowDelay").GetComponent<Text>();
            txtNumFireballsPerTick = menu.GetCustomReference("txt_NumFireballsPerTick").GetComponent<Text>();
            txtChancesRandomThrow = menu.GetCustomReference("txt_ChancesRandomThrow").GetComponent<Text>();
            txtRadiusDetection = menu.GetCustomReference("txt_RadiusDetection").GetComponent<Text>();
            btnSpawnSun = menu.GetCustomReference("btn_SpawnSun").GetComponent<Button>();
            btnThrowFireballs = menu.GetCustomReference("btn_ThrowFireballs").GetComponent<Button>();
            btnAttackOutsideWave = menu.GetCustomReference("btn_AttackOutsideWave").GetComponent<Button>();
            btnTargetPlayer = menu.GetCustomReference("btn_TargetPlayer").GetComponent<Button>();
            btnTimeIdleFireballs = menu.GetCustomReference("btn_TimeIdleFireballs").GetComponent<Button>();
            btnTimeActivateFireballs = menu.GetCustomReference("btn_TimeActivateFireballs").GetComponent<Button>();
            btnTickThrowDelay = menu.GetCustomReference("btn_TickThrowDelay").GetComponent<Button>();
            btnNumFireballsPerTick = menu.GetCustomReference("btn_NumFireballsPerTick").GetComponent<Button>();
            btnChancesRandomThrow = menu.GetCustomReference("btn_ChancesRandomThrow").GetComponent<Button>();
            btnRadiusDetection = menu.GetCustomReference("btn_RadiusDetection").GetComponent<Button>();

            // Add an event listener for buttons
            btnSpawnSun.onClick.AddListener(ClickSpawnSun);
            btnThrowFireballs.onClick.AddListener(ClickThrowFireballs);
            btnAttackOutsideWave.onClick.AddListener(ClickAttackOutsideWave);
            btnTargetPlayer.onClick.AddListener(ClickTargetPlayer);
            btnTimeIdleFireballs.onClick.AddListener(ClickTimeIdleFireballs);
            btnTimeActivateFireballs.onClick.AddListener(ClickTimeActivateFireballs);
            btnTickThrowDelay.onClick.AddListener(ClickTickThrowDelay);
            btnNumFireballsPerTick.onClick.AddListener(ClickNumFireballsPerTick);
            btnChancesRandomThrow.onClick.AddListener(ClickChancesRandomThrow);
            btnRadiusDetection.onClick.AddListener(ClickRadiusDetection);

            // Initialization of datas

            flamingSunController = GameManager.local.gameObject.AddComponent<FlamingSunController>();
            flamingSunController.data.FlamingSunSpawnGetSet = allowSpawn;
            flamingSunController.data.FlamingSunThrowFireballsGetSet = sunThrowFireballs;
            flamingSunController.data.FlamingSunAttackOutsideWavesGetSet = fireballsOutsideOfCombatWave;
            flamingSunController.data.FlamingSunTargetPlayerGetSet = sunTargetsPlayer;
            flamingSunController.data.FlamingSunTimeIdleFireballsGetSet = timerIdleWaveOfFireballs;
            flamingSunController.data.FlamingSunTimeActivateFireballsGetSet = timerActivateWaveOfFireballs;
            flamingSunController.data.FlamingSunTickThrowFireballsDelayGetSet = timerTickThrowDelay;
            flamingSunController.data.FlamingSunNumFireballPerTickGetSet = numOfFireballsPerTick;
            flamingSunController.data.FlamingSunChancesOfRandomThrowGetSet = chancesOfRandomThrow;
            flamingSunController.data.FlamingSunRadiusOfDetectionGetSet = radiusOfDetectionCreaturesByFlamingSun;


            flamingSunHook = menu.gameObject.AddComponent<FlamingSunHook>();
            flamingSunHook.menu = this;

            // Update all the Data for left page (text, visibility of buttons etc...)
            UpdateDataPageLeft1();
            // Update all the Data for right page (text, visibility of buttons etc...)
            UpdateDataPageRight1();
        }

        public void ClickSpawnSun()
        {
            flamingSunController.data.FlamingSunSpawnGetSet ^= true;
            UpdateDataPageLeft1();
        }
        public void ClickThrowFireballs()
        {
            flamingSunController.data.FlamingSunThrowFireballsGetSet ^= true;
            UpdateDataPageLeft1();
        }
        public void ClickAttackOutsideWave()
        {
            flamingSunController.data.FlamingSunAttackOutsideWavesGetSet ^= true;
            UpdateDataPageLeft1();
        }
        public void ClickTargetPlayer()
        {
            flamingSunController.data.FlamingSunTargetPlayerGetSet ^= true;
            Debug.Log(flamingSunController.data.FlamingSunTargetPlayerGetSet);
            UpdateDataPageLeft1();
        }
        public void ClickTimeIdleFireballs()
        {
            flamingSunController.data.FlamingSunTimeIdleFireballsButtonPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsFloat = true;
            UpdateDataPageRight1();
        }
        public void ClickTimeActivateFireballs()
        {
            flamingSunController.data.FlamingSunTimeActivateFireballsButtonPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsFloat = true;
            UpdateDataPageRight1();
        }
        public void ClickTickThrowDelay()
        {
            flamingSunController.data.FlamingSunTickThrowFireballsDelayButtonPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsFloat = true;
            UpdateDataPageRight1();
        }
        public void ClickNumFireballsPerTick()
        {
            flamingSunController.data.FlamingSunNumFireballPerTickPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsUint = true;
            UpdateDataPageRight1();
        }
        public void ClickChancesRandomThrow()
        {
            flamingSunController.data.FlamingSunChancesOfRandomThrowButtonPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsUint = true;
            UpdateDataPageRight1();
        }
        public void ClickRadiusDetection()
        {
            flamingSunController.data.FlamingSunRadiusOfDetectionButtonPressedGetSet = true;
            flamingSunController.data.ValueToAssignIsUint = true;
            UpdateDataPageRight1();
        }

        public void UpdateDataPageLeft1()
        {
            txtSpawnSun.text = flamingSunController.data.FlamingSunSpawnGetSet ? "Enabled" : "Disabled";
            txtThrowFireballs.text = flamingSunController.data.FlamingSunThrowFireballsGetSet ? "Enabled" : "Disabled";
            txtAttackOutsideWave.text = flamingSunController.data.FlamingSunAttackOutsideWavesGetSet ? "Enabled" : "Disabled";
            txtTargetPlayer.text = flamingSunController.data.FlamingSunTargetPlayerGetSet ? "Enabled" : "Disabled";
        }

        public void UpdateDataPageRight1()
        {
            // Assign
            // time idle fireballs
            // time activate fireballs
            // tick throw delay
            // number of fireballs per tick
            // chances of random throw
            // radius detections
            // value when the enter button is pressed
            if (flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet == true)
            {
                // Assign the time idle fireballs
                if (flamingSunController.data.FlamingSunTimeIdleFireballsButtonPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunTimeIdleFireballsGetSet = flamingSunController.data.ValueToAssignedFloatGetSet;
                    flamingSunController.data.FlamingSunTimeIdleFireballsButtonPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the time activate fireballs
                if (flamingSunController.data.FlamingSunTimeActivateFireballsButtonPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunTimeActivateFireballsGetSet = flamingSunController.data.ValueToAssignedFloatGetSet;
                    flamingSunController.data.FlamingSunTimeActivateFireballsButtonPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the tick throw delay
                if (flamingSunController.data.FlamingSunTickThrowFireballsDelayButtonPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunTickThrowFireballsDelayGetSet = flamingSunController.data.ValueToAssignedFloatGetSet;
                    flamingSunController.data.FlamingSunTickThrowFireballsDelayButtonPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the number of fireballs per tick
                if (flamingSunController.data.FlamingSunNumFireballPerTickPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunNumFireballPerTickGetSet = flamingSunController.data.ValueToAssignedUintGetSet;
                    flamingSunController.data.FlamingSunNumFireballPerTickPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the chances of random throw
                if (flamingSunController.data.FlamingSunChancesOfRandomThrowButtonPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunChancesOfRandomThrowGetSet = flamingSunController.data.ValueToAssignedUintGetSet;
                    flamingSunController.data.FlamingSunChancesOfRandomThrowButtonPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
                // Assign the radius detection
                if (flamingSunController.data.FlamingSunRadiusOfDetectionButtonPressedGetSet == true)
                {
                    flamingSunController.data.FlamingSunRadiusOfDetectionGetSet = flamingSunController.data.ValueToAssignedUintGetSet;
                    flamingSunController.data.FlamingSunRadiusOfDetectionButtonPressedGetSet = false;
                    flamingSunController.data.KeyboardFinishEnterButtonPressedGetSet = false;
                }
            }
            txtTimeIdleFireballs.text = flamingSunController.data.FlamingSunTimeIdleFireballsGetSet.ToString();
            txtTimeActivateFireballs.text = flamingSunController.data.FlamingSunTimeActivateFireballsGetSet.ToString();
            txtTickThrowDelay.text = flamingSunController.data.FlamingSunTickThrowFireballsDelayGetSet.ToString();
            txtNumFireballsPerTick.text = flamingSunController.data.FlamingSunNumFireballPerTickGetSet.ToString();
            txtChancesRandomThrow.text = flamingSunController.data.FlamingSunChancesOfRandomThrowGetSet.ToString();
            txtRadiusDetection.text = flamingSunController.data.FlamingSunRadiusOfDetectionGetSet.ToString();
        }
        // Refresh the menu each frame (need optimization)
        public class FlamingSunHook : MonoBehaviour
        {
            public FlamingSunMenuModule menu;

            void Update()
            {
                menu.UpdateDataPageLeft1();
                menu.UpdateDataPageRight1();
            }
        }
    }
}
