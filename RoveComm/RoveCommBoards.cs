namespace RoveComm
{
    public class _Boards
    {
        public Boards.Core Core;
        public Boards.PMS PMS;
        public Boards.SignalStack SignalStack;
        public Boards.Arm Arm;
        public Boards.Auger Auger;
        public Boards.Autonomy Autonomy;
        public Boards.Camera1 Camera1;
        public Boards.Camera2 Camera2;
        public Boards.CameraServer CameraServer;
        public Boards.Raman Raman;

        internal _Boards(RoveCommService service)
        {
            Core = new(service);
            PMS = new(service);
            SignalStack = new(service);
            Arm = new(service);
            Auger = new(service);
            Autonomy = new(service);
            Camera1 = new(service);
            Camera2 = new(service);
            CameraServer = new(service);
            Raman = new(service);
            Arm = new(service);
        }
    }
}

namespace RoveComm.Boards
{
    public class Core
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.110";

        internal Core(RoveCommService service) => _service = service;

        /// <summary>
        /// [LeftSpeed, RightSpeed] (-1, 1)-> (-100%, 100%)
        /// </summary>
        /// <param name="LeftSpeed"></param>
        /// <param name="RightSpeed"></param>
        public void DriveLeftRight(float LeftSpeed, float RightSpeed)
        {
            _service.Send(3000, [LeftSpeed, RightSpeed], _ip);
        }

        /// <summary>
        /// [LF, LM, LR, RF, RM, RR] (-1, 1)-> (-100%, 100%)
        /// </summary>
        /// <param name="LF"></param>
        /// <param name="LM"></param>
        /// <param name="LR"></param>
        /// <param name="RF"></param>
        /// <param name="RM"></param>
        /// <param name="RR"></param>
        public void DriveIndividual(float LF, float LM, float LR, float RF, float RM, float RR)
        {
            _service.Send(3001, [LF, LM, LR, RF, RM, RR], _ip);
        }

        /// <summary>
        /// [0-override off, 1-override on]
        /// </summary>
        /// <param name="arg1"></param>
        public void WatchdogOverride(byte arg1)
        {
            _service.Send(3002, [arg1], _ip);
        }

        /// <summary>
        /// [Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Tilt"></param>
        public void LeftDriveGimbalIncrement(short Tilt)
        {
            _service.Send(3003, [Tilt], _ip);
        }

        /// <summary>
        /// [Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Tilt"></param>
        public void RightDriveGimbalIncrement(short Tilt)
        {
            _service.Send(3004, [Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void LeftMainGimbalIncrement(short Pan, short Tilt)
        {
            _service.Send(3005, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void RightMainGimbalIncrement(short Pan, short Tilt)
        {
            _service.Send(3006, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Tilt"></param>
        public void BackDriveGimbalIncrement(short Tilt)
        {
            _service.Send(3007, [Tilt], _ip);
        }

        /// <summary>
        /// [R, G, B] (0, 255)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void LEDRGB(byte R, byte G, byte B)
        {
            _service.Send(3008, [R, G, B], _ip);
        }

        /// <summary>
        /// [Pattern] (Enum)
        /// </summary>
        /// <param name="Pattern"></param>
        public void LEDPatterns(byte Pattern)
        {
            _service.Send(3009, [Pattern], _ip);
        }

        /// <summary>
        /// [Teleop, Autonomy, Reached Goal] (enum)
        /// </summary>
        /// <param name="arg1"></param>
        public void StateDisplay(byte arg1)
        {
            _service.Send(3010, [arg1], _ip);
        }

        /// <summary>
        /// Set Brightness (0-255)
        /// </summary>
        /// <param name="arg1"></param>
        public void Brightness(byte arg1)
        {
            _service.Send(3011, [arg1], _ip);
        }

        /// <summary>
        /// 0: Teleop, 1: Autonomy
        /// </summary>
        /// <param name="arg1"></param>
        public void SetWatchdogMode(byte arg1)
        {
            _service.Send(3012, [arg1], _ip);
        }

        /// <summary>
        /// Set the message to display on the lighting panel; null terminator ends string early
        /// </summary>
        /// <param name="args"></param>
        public void LEDText(char[] args)
        {
            _service.Send(3013, [args], _ip);
        }

        public enum Motors
        {
            FRONT_LEFT = 0,
            MIDDLE_LEFT = 1,
            BACK_LEFT = 2,
            FRONT_RIGHT = 3,
            MIDDLE_RIGHT = 4,
            BACK_RIGHT = 5,
        }
        public enum DisplayState
        {
            TELEOP = 0,
            AUTONOMY = 1,
            REACHED_GOAL = 2,
        }
        public enum Patterns
        {
            MRDT = 0,
            BELGIUM = 1,
            MERICA = 2,
            DIRT = 3,
            DOTA = 4,
            MCD = 5,
            WINDOWS = 6,
        }
        public enum VESCFaultCode
        {
            NONE = 0,
            OVER_VOLTAGE = 1,
            UNDER_VOLTAGE = 2,
            DRV = 3,
            ABS_OVER_CURRENT = 4,
            OVER_TEMP_FET = 5,
            OVER_TEMP_MOTOR = 6,
            GATE_DRIVER_OVER_VOLTAGE = 7,
            GATE_DRIVER_UNDER_VOLTAGE = 8,
            MCU_UNDER_VOLTAGE = 9,
            BOOTING_FROM_WATCHDOG_RESET = 10,
            ENCODER_SPI = 11,
            ENCODER_SINCOS_BELOW_MIN_AMPLITUDE = 12,
            ENCODER_SINCOS_ABOVE_MAX_AMPLITUDE = 13,
            FLASH_CORRUPTION = 14,
            HIGH_OFFSET_CURRENT_SENSOR_1 = 15,
            HIGH_OFFSET_CURRENT_SENSOR_2 = 16,
            HIGH_OFFSET_CURRENT_SENSOR_3 = 17,
            UNBALANCED_CURRENTS = 18,
            BRK = 19,
            RESOLVER_LOT = 20,
            RESOLVER_DOS = 21,
            RESOLVER_LOS = 22,
            FLASH_CORRUPTION_APP_CFG = 23,
            FLASH_CORRUPTION_MC_CFG = 24,
            ENCODER_NO_MAGNET = 25,
            ENCODER_MAGNET_TOO_STRONG = 26,
            PHASE_FILTER = 27,
        }
    }

    public class PMS
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.102";

        internal PMS(RoveCommService service) => _service = service;

        /// <summary>
        /// Power off all systems except network (PMS will stay on)
        /// </summary>
        /// <param name="arg1"></param>
        public void EStop(byte arg1)
        {
            _service.Send(4000, [arg1], _ip);
        }

        /// <summary>
        /// Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
        /// </summary>
        /// <param name="arg1"></param>
        public void Suicide(byte arg1)
        {
            _service.Send(4001, [arg1], _ip);
        }

        /// <summary>
        /// Cycle all systems including network off and back on (PMS will stay on)
        /// </summary>
        /// <param name="arg1"></param>
        public void Reboot(byte arg1)
        {
            _service.Send(4002, [arg1], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked) [1-Enable, 0-No change]
        /// </summary>
        /// <param name="arg1"></param>
        public void EnableBus(byte arg1)
        {
            _service.Send(4003, [arg1], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked) [1-Disable, 0-No change]
        /// </summary>
        /// <param name="arg1"></param>
        public void DisableBus(byte arg1)
        {
            _service.Send(4004, [arg1], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked) [1-Enable, 0-Disable]
        /// </summary>
        /// <param name="arg1"></param>
        public void SetBus(byte arg1)
        {
            _service.Send(4005, [arg1], _ip);
        }
    }

    public class SignalStack
    {
        private RoveCommService _service;
        private static string _ip = "192.168.100.101";

        internal SignalStack(RoveCommService service) => _service = service;

        /// <summary>
        /// Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="arg1"></param>
        public void OpenLoop(short arg1)
        {
            _service.Send(7000, [arg1], _ip);
        }

        /// <summary>
        /// [Heading] [0, 360)
        /// </summary>
        /// <param name="Heading"></param>
        public void SetAngleTarget(float Heading)
        {
            _service.Send(7001, [Heading], _ip);
        }

        /// <summary>
        /// [Rover Lat, Rover Long, Basestation Lat, Basestation Long] [Lat:(-90, 90), Long:(-180, 180)] (deg)
        /// </summary>
        /// <param name="RoverLat"></param>
        /// <param name="RoverLong"></param>
        /// <param name="BasestationLat"></param>
        /// <param name="BasestationLong"></param>
        public void SetGPSTarget(double RoverLat, double RoverLong, double BasestationLat, double BasestationLong)
        {
            _service.Send(7002, [RoverLat, RoverLong, BasestationLat, BasestationLong], _ip);
        }

        /// <summary>
        /// [0-override off, 1-override on]
        /// </summary>
        /// <param name="arg1"></param>
        public void WatchdogOverride(byte arg1)
        {
            _service.Send(7003, [arg1], _ip);
        }
    }

    public class Arm
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.107";

        internal Arm(RoveCommService service) => _service = service;

        /// <summary>
        /// [X, J2, J3, J4, P, R] Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void SetIndividualSpeeds(short X, short J2, short J3, short J4, short P, short R)
        {
            _service.Send(8000, [X, J2, J3, J4, P, R], _ip);
        }

        /// <summary>
        /// [JointID, Decipercent] Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="JointID"></param>
        /// <param name="Decipercent"></param>
        public void SetJointSpeed(short JointID, short Decipercent)
        {
            _service.Send(8001, [JointID, Decipercent], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void SetIndividualTargetAngles(float X, float J2, float J3, float J4, float P, float R)
        {
            _service.Send(8002, [X, J2, J3, J4, P, R], _ip);
        }

        /// <summary>
        /// [JointID, Position] (in for id 0, deg otherwise)
        /// </summary>
        /// <param name="JointID"></param>
        /// <param name="Position"></param>
        public void SetJointTargetAngle(float JointID, float Position)
        {
            _service.Send(8003, [JointID, Position], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void IncrementIndividualTargetAngles(float X, float J2, float J3, float J4, float P, float R)
        {
            _service.Send(8004, [X, J2, J3, J4, P, R], _ip);
        }

        /// <summary>
        /// [JointID, Angle] (in for id 0, deg otherwise)
        /// </summary>
        /// <param name="JointID"></param>
        /// <param name="Angle"></param>
        public void IncrementJointTargetAngle(float JointID, float Angle)
        {
            _service.Send(8005, [JointID, Angle], _ip);
        }

        /// <summary>
        /// [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void SetIKPosition(float X, float Y, float Z, float J4, float P, float R)
        {
            _service.Send(8006, [X, Y, Z, J4, P, R], _ip);
        }

        /// <summary>
        /// [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void IncrementIKPosition(float X, float Y, float Z, float J4, float P, float R)
        {
            _service.Send(8007, [X, Y, Z, J4, P, R], _ip);
        }

        /// <summary>
        /// [J4, P, R] (deg, deg, deg)
        /// </summary>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void SetLockModePosition(float J4, float P, float R)
        {
            _service.Send(8008, [J4, P, R], _ip);
        }

        /// <summary>
        /// [J4, P, R] (deg, deg, deg)
        /// </summary>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void IncrementLockModePosition(float J4, float P, float R)
        {
            _service.Send(8009, [J4, P, R], _ip);
        }

        /// <summary>
        /// [0-disable, 1-enable]
        /// </summary>
        /// <param name="arg1"></param>
        public void Laser(byte arg1)
        {
            _service.Send(8010, [arg1], _ip);
        }

        /// <summary>
        /// [0-retract, 1-extend]
        /// </summary>
        /// <param name="arg1"></param>
        public void Solenoid(byte arg1)
        {
            _service.Send(8011, [arg1], _ip);
        }

        /// <summary>
        /// [Motor decipercent (-1000, 1000), Gripper number (0, 1)]
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void SetGripperSpeed(short arg1, short arg2)
        {
            _service.Send(8012, [arg1, arg2], _ip);
        }

        /// <summary>
        /// [0-override off, 1-override on] (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void WatchdogOverride(byte arg1)
        {
            _service.Send(8013, [arg1], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P] (0-override off, 1-override on) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(ushort arg1)
        {
            _service.Send(8014, [arg1], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (0-override off, 1-override on) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void ClosedLoopOverride(byte arg1)
        {
            _service.Send(8015, [arg1], _ip);
        }

        /// <summary>
        /// [X, Roll] (1-calibrate, 0-no action) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void CalibrateEncoder(byte arg1)
        {
            _service.Send(8016, [arg1], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (0-override off, 1-override on) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void SoftLimitOverride(ushort arg1)
        {
            _service.Send(8017, [arg1], _ip);
        }

        /// <summary>
        /// Shut off all motors (set decipercents to 0 and disable closed loop)
        /// </summary>
        /// <param name="arg1"></param>
        public void EStop(byte arg1)
        {
            _service.Send(8018, [arg1], _ip);
        }

        public enum Joints
        {
            X = 0,
            J2 = 1,
            J3 = 2,
            J4 = 3,
            PITCH = 4,
            ROLL = 5,
        }
    }

    public class Auger
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.108";

        internal Auger(RoveCommService service) => _service = service;

        /// <summary>
        /// Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="arg1"></param>
        public void AugerAxis_OpenLoop(short arg1)
        {
            _service.Send(9000, [arg1], _ip);
        }

        /// <summary>
        /// Absolute position (in)
        /// </summary>
        /// <param name="arg1"></param>
        public void AugerAxis_SetPosition(float arg1)
        {
            _service.Send(9001, [arg1], _ip);
        }

        /// <summary>
        /// (in)
        /// </summary>
        /// <param name="arg1"></param>
        public void AugerAxis_IncrementPosition(float arg1)
        {
            _service.Send(9002, [arg1], _ip);
        }

        /// <summary>
        /// [AugerAxis+, AugerAxis-] (0-override off, 1-override on) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(byte arg1)
        {
            _service.Send(9003, [arg1], _ip);
        }

        /// <summary>
        /// Request calibration of the AugerAxis encoder
        /// </summary>
        /// <param name="arg1"></param>
        public void CalibrateEncoder(byte arg1)
        {
            _service.Send(9004, [arg1], _ip);
        }

        /// <summary>
        /// Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="arg1"></param>
        public void RunAuger(short arg1)
        {
            _service.Send(9005, [arg1], _ip);
        }

        /// <summary>
        /// [0-override off, 1-override on]
        /// </summary>
        /// <param name="arg1"></param>
        public void WatchdogOverride(byte arg1)
        {
            _service.Send(9006, [arg1], _ip);
        }

        /// <summary>
        /// Request a reading of the temperature at the end of the auger
        /// </summary>
        /// <param name="arg1"></param>
        public void RequestTemperature(byte arg1)
        {
            _service.Send(9007, [arg1], _ip);
        }

        /// <summary>
        /// Request a reading of the humidity at the end of the auger
        /// </summary>
        /// <param name="arg1"></param>
        public void RequestHumidity(byte arg1)
        {
            _service.Send(9008, [arg1], _ip);
        }

        /// <summary>
        /// Ultraviolet LED on AutoFluorescence (0-off, 1-on)
        /// </summary>
        /// <param name="arg1"></param>
        public void UVLED(byte arg1)
        {
            _service.Send(9009, [arg1], _ip);
        }

        /// <summary>
        /// [Position](degrees -180-180)
        /// </summary>
        /// <param name="Position"></param>
        public void AugerMultiplexerServo(short Position)
        {
            _service.Send(9010, [Position], _ip);
        }
    }

    public class Autonomy
    {
        private RoveCommService _service;
        private static string _ip = "192.168.3.100";

        internal Autonomy(RoveCommService service) => _service = service;

        /// <summary>
        /// Start Autonomy_Software
        /// </summary>
        /// <param name="arg1"></param>
        public void StartAutonomy(byte arg1)
        {
            _service.Send(11000, [arg1], _ip);
        }

        /// <summary>
        /// Return Autonomy_Software to Idle state
        /// </summary>
        /// <param name="arg1"></param>
        public void DisableAutonomy(byte arg1)
        {
            _service.Send(11001, [arg1], _ip);
        }

        /// <summary>
        /// [Lat, Lon, AUTONOMYWAYPOINTTYPES]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="AUTONOMYWAYPOINTTYPES"></param>
        public void AddPositionLeg(double Lat, double Lon, double AUTONOMYWAYPOINTTYPES)
        {
            _service.Send(11002, [Lat, Lon, AUTONOMYWAYPOINTTYPES], _ip);
        }

        /// <summary>
        /// [Lat, Lon, AUTONOMYWAYPOINTTYPES, MarkerRadius (meters)]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="AUTONOMYWAYPOINTTYPES"></param>
        /// <param name="MarkerRadius"></param>
        public void AddMarkerLeg(double Lat, double Lon, double AUTONOMYWAYPOINTTYPES, double MarkerRadius)
        {
            _service.Send(11003, [Lat, Lon, AUTONOMYWAYPOINTTYPES, MarkerRadius], _ip);
        }

        /// <summary>
        /// [Lat, Lon, AUTONOMYWAYPOINTTYPES, ObjectRadius (meters)]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="AUTONOMYWAYPOINTTYPES"></param>
        /// <param name="ObjectRadius"></param>
        public void AddObjectLeg(double Lat, double Lon, double AUTONOMYWAYPOINTTYPES, double ObjectRadius)
        {
            _service.Send(11004, [Lat, Lon, AUTONOMYWAYPOINTTYPES, ObjectRadius], _ip);
        }

        /// <summary>
        /// Clear queued positions, markers, and objects waypoints.
        /// </summary>
        /// <param name="arg1"></param>
        public void ClearWaypoints(byte arg1)
        {
            _service.Send(11005, [arg1], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will scale the max power effort of Autonomy.
        /// </summary>
        /// <param name="arg1"></param>
        public void SetMaxSpeed(float arg1)
        {
            _service.Send(11006, [arg1], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will filter points from the traversability map. Higher values will result in more conservative pathing.
        /// </summary>
        /// <param name="arg1"></param>
        public void SetMinTravScore(float arg1)
        {
            _service.Send(11007, [arg1], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will bias the pathing algorithm towards shorter paths (lower values) or safer paths (higher values).
        /// </summary>
        /// <param name="arg1"></param>
        public void SetBetaBias(float arg1)
        {
            _service.Send(11008, [arg1], _ip);
        }

        /// <summary>
        /// [Enum (AUTONOMYLOG), Enum (AUTONOMYLOG), Enum (AUTONOMYLOG)] {Console, File, RoveComm}
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public void SetLoggingLevels(byte arg1, byte arg2, byte arg3)
        {
            _service.Send(11009, [arg1, arg2, arg3], _ip);
        }

        /// <summary>
        /// [Lat, Lon, ObstacleRadius (meters)]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="ObstacleRadius"></param>
        public void AddObstacle(double Lat, double Lon, double ObstacleRadius)
        {
            _service.Send(11010, [Lat, Lon, ObstacleRadius], _ip);
        }

        /// <summary>
        /// Clear queued permanent obstacles.
        /// </summary>
        /// <param name="arg1"></param>
        public void ClearObstacles(byte arg1)
        {
            _service.Send(11011, [arg1], _ip);
        }

        public enum AUTONOMYSTATE
        {
            Idle = 0,
            Navigating = 1,
            SearchPattern = 2,
            ApproachingMarker = 3,
            ApproachingObject = 4,
            VerifyingGPS = 5,
            VerifyingMarker = 6,
            VerifyingObject = 7,
            Avoidance = 8,
            Reversing = 9,
            Stuck = 10,
        }
        public enum AUTONOMYLOG
        {
            TraceL3 = 0,
            TraceL2 = 1,
            TraceL1 = 2,
            Debug = 3,
            Info = 4,
            Notice = 5,
            Warning = 6,
            Error = 7,
            Critical = 8,
        }
        public enum AUTONOMYTHREADS
        {
            NotSet = 0,
            MainProcess = 1,
            MainCam = 2,
            GroundCam = 3,
            TagDetector = 4,
            ObjectDetector = 5,
            StateMachine = 6,
            RoveCommUDP = 7,
            RoveCommTCP = 8,
        }
        public enum AUTONOMYWAYPOINTTYPES
        {
            ContinuousNavigate = -99,
            RockPick = -4,
            WaterBottle = -3,
            Mallet = -2,
            Any = -1,
            Tag0 = 0,
            Tag1 = 1,
            Tag2 = 2,
            Tag3 = 3,
        }
    }

    public class Camera1
    {
        private RoveCommService _service;
        private static string _ip = "192.168.4.100";

        internal Camera1(RoveCommService service) => _service = service;

        /// <summary>
        /// Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void TakePicture(byte arg1, byte arg2)
        {
            _service.Send(12000, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void ToggleStream(byte arg1, byte arg2)
        {
            _service.Send(12001, [arg1, arg2], _ip);
        }

        /// <summary>
        /// 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/ffmpeg_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $ip: output ip, $port: output port, $brightness, $contrast.
        /// </summary>
        /// <param name="args"></param>
        public void SetFFMPEGArguments(char[] args)
        {
            _service.Send(12002, [args], _ip);
        }

        /// <summary>
        /// 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/picture_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $output: output file without extension, $brightness, $contrast.
        /// </summary>
        /// <param name="args"></param>
        public void SetPictureArguments(char[] args)
        {
            _service.Send(12003, [args], _ip);
        }

        /// <summary>
        /// Brightness for each camera (-1.0, 1.0)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void SetBrightness(float arg1, float arg2, float arg3, float arg4)
        {
            _service.Send(12004, [arg1, arg2, arg3, arg4], _ip);
        }

        /// <summary>
        /// Contrast for each camera (0, 2)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void SetContrast(float arg1, float arg2, float arg3, float arg4)
        {
            _service.Send(12005, [arg1, arg2, arg3, arg4], _ip);
        }
    }

    public class Camera2
    {
        private RoveCommService _service;
        private static string _ip = "192.168.4.101";

        internal Camera2(RoveCommService service) => _service = service;

        /// <summary>
        /// Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void TakePicture(byte arg1, byte arg2)
        {
            _service.Send(13000, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void ToggleStream(byte arg1, byte arg2)
        {
            _service.Send(13001, [arg1, arg2], _ip);
        }

        /// <summary>
        /// 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/ffmpeg_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $ip: output ip, $port: output port, $brightness, $contrast.
        /// </summary>
        /// <param name="args"></param>
        public void SetFFMPEGArguments(char[] args)
        {
            _service.Send(13002, [args], _ip);
        }

        /// <summary>
        /// 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/picture_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $output: output file without extension, $brightness, $contrast.
        /// </summary>
        /// <param name="args"></param>
        public void SetPictureArguments(char[] args)
        {
            _service.Send(13003, [args], _ip);
        }

        /// <summary>
        /// Brightness for each camera (-1.0, 1.0)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void SetBrightness(float arg1, float arg2, float arg3, float arg4)
        {
            _service.Send(13004, [arg1, arg2, arg3, arg4], _ip);
        }

        /// <summary>
        /// Contrast for each camera (0, 2)
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        public void SetContrast(float arg1, float arg2, float arg3, float arg4)
        {
            _service.Send(13005, [arg1, arg2, arg3, arg4], _ip);
        }
    }

    public class CameraServer
    {
        private RoveCommService _service;
        private static string _ip = "192.168.4.102";

        internal CameraServer(RoveCommService service) => _service = service;

        /// <summary>
        /// Take a picture with the current camera. [0] is the camera to take a picture with.
        /// </summary>
        /// <param name="arg1"></param>
        public void TakePhoto(byte arg1)
        {
            _service.Send(14000, [arg1], _ip);
        }

        /// <summary>
        /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is the action (0 = Shutdown, 1 = Startup, 2 = Restart).
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void ToggleStream(byte arg1, byte arg2)
        {
            _service.Send(14001, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Adjust brightness level (0-255). [0] is the camera ID, [1] is the brightness level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void AdjustBrightness(byte arg1, byte arg2)
        {
            _service.Send(14002, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Adjust contrast level (0-255). [0] is the camera ID, [1] is the contrast level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void AdjustContrast(byte arg1, byte arg2)
        {
            _service.Send(14003, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Adjust saturation level (0-255). [0] is the camera ID, [1] is the saturation level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void AdjustSaturation(byte arg1, byte arg2)
        {
            _service.Send(14004, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Adjust hue level (0-255). [0] is the camera ID, [1] is the hue level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void AdjustHue(byte arg1, byte arg2)
        {
            _service.Send(14005, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Set white balance temperature. [0] is the camera ID, [1] is the white balance level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void SetWhiteBalance(byte arg1, byte arg2)
        {
            _service.Send(14008, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Adjust backlight contrast level (0-255). [0] is the camera ID, [1] is the backlight contrast level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void AdjustBacklightContrast(byte arg1, byte arg2)
        {
            _service.Send(14009, [arg1, arg2], _ip);
        }

        /// <summary>
        /// Set exposure level. [0] is the camera ID, [1] is the exposure level.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void SetExposure(int arg1, int arg2)
        {
            _service.Send(14010, [arg1, arg2], _ip);
        }
    }

    public class Raman
    {
        private RoveCommService _service;
        private static string _ip = "192.168.3.105";

        internal Raman(RoveCommService service) => _service = service;

        /// <summary>
        /// Motor decipercent [-1000, 1000]
        /// </summary>
        /// <param name="arg1"></param>
        public void InstrumentsAxis_OpenLoop(short arg1)
        {
            _service.Send(16000, [arg1], _ip);
        }

        /// <summary>
        /// Absolute position (in)
        /// </summary>
        /// <param name="arg1"></param>
        public void InstrumentsAxis_SetPosition(float arg1)
        {
            _service.Send(16001, [arg1], _ip);
        }

        /// <summary>
        /// (in)
        /// </summary>
        /// <param name="arg1"></param>
        public void InstrumentsAxis_IncrementPosition(float arg1)
        {
            _service.Send(16002, [arg1], _ip);
        }

        /// <summary>
        /// [InstrumentsAxis+, InstrumentsAxis-] (0-override off, 1-override on) (bitmasked)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(byte arg1)
        {
            _service.Send(16003, [arg1], _ip);
        }

        /// <summary>
        /// Request calibration of the InstrumentsAxis encoder
        /// </summary>
        /// <param name="arg1"></param>
        public void CalibrateEncoder(byte arg1)
        {
            _service.Send(16004, [arg1], _ip);
        }

        /// <summary>
        /// [0-override off, 1-override on]
        /// </summary>
        /// <param name="arg1"></param>
        public void WatchdogOverride(byte arg1)
        {
            _service.Send(16005, [arg1], _ip);
        }

        /// <summary>
        /// [0-disable, 1-enable]
        /// </summary>
        /// <param name="arg1"></param>
        public void Laser(byte arg1)
        {
            _service.Send(16006, [arg1], _ip);
        }

        /// <summary>
        /// Start a Raman reading, with the provided integration time (milliseconds)
        /// </summary>
        /// <param name="arg1"></param>
        public void RequestRamanReading(uint arg1)
        {
            _service.Send(16007, [arg1], _ip);
        }

        /// <summary>
        /// [Pan, Tilt](degrees -180-180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void RamanGimbalIncrement(short Pan, short Tilt)
        {
            _service.Send(16008, [Pan, Tilt], _ip);
        }
    }
}
