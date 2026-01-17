namespace RoveComm
{
    public class _Boards
    {
        public Boards.Core Core;
        public Boards.PMS PMS;
        public Boards.Nav Nav;
        public Boards.SignalStack SignalStack;
        public Boards.Arm Arm;
        public Boards.Auger Auger;
        public Boards.Autonomy Autonomy;
        public Boards.Camera1 Camera1;
        public Boards.Camera2 Camera2;
        public Boards.CameraServer CameraServer;
        public Boards.Raman Raman;
        public Boards.RoveSoSimulator RoveSoSimulator;

        internal _Boards(RoveCommService service)
        {
            Core = new(service);
            PMS = new(service);
            Nav = new(service);
            SignalStack = new(service);
            Arm = new(service);
            Auger = new(service);
            Autonomy = new(service);
            Camera1 = new(service);
            Camera2 = new(service);
            CameraServer = new(service);
            Raman = new(service);
            RoveSoSimulator = new(service);
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
        /// [LeftSpeed, RightSpeed] (-1 - 1)-> (-100% - 100%)
        /// </summary>
        /// <param name="LeftSpeed"></param>
        /// <param name="RightSpeed"></param>
        public void DriveLeftRight(float LeftSpeed, float RightSpeed)
        {
            _service.Send(3000, [LeftSpeed, RightSpeed], _ip);
        }

        /// <summary>
        /// [LF, LM, LR, RF, RM, RR] (-1 - 1)-> (-100% - 100%)
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
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void LeftGimbal(short Pan, short Tilt)
        {
            _service.Send(3003, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void RightGimbal(short Pan, short Tilt)
        {
            _service.Send(3004, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void BackGimbal(short Pan, short Tilt)
        {
            _service.Send(3005, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [R, G, B] (Brightness 0 - 255)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void LEDRGB(byte R, byte G, byte B)
        {
            _service.Send(3006, [R, G, B], _ip);
        }

        /// <summary>
        /// [Color] (RGBA)
        /// </summary>
        /// <param name="args"></param>
        public void BackImage(uint[] args)
        {
            _service.Send(3007, [args], _ip);
        }

        /// <summary>
        /// [R, G, B] (Brightness 0 - 255)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void InternalRGB(byte R, byte G, byte B)
        {
            _service.Send(3008, [R, G, B], _ip);
        }

        /// <summary>
        /// [Color] (RGBA)
        /// </summary>
        /// <param name="args"></param>
        public void InternalImage(uint[] args)
        {
            _service.Send(3009, [args], _ip);
        }

        /// <summary>
        /// [State] (DisplayState)
        /// </summary>
        /// <param name="State"></param>
        public void StateDisplay(byte State)
        {
            _service.Send(3010, [State], _ip);
        }

        /// <summary>
        /// [Brightness] (0 - 255)
        /// </summary>
        /// <param name="Brightness"></param>
        public void Brightness(byte Brightness)
        {
            _service.Send(3011, [Brightness], _ip);
        }

        /// <summary>
        /// [Mode] (0: Teleop, 1: Autonomy)
        /// </summary>
        /// <param name="Mode"></param>
        public void SetWatchdogMode(byte Mode)
        {
            _service.Send(3012, [Mode], _ip);
        }

        /// <summary>
        /// [Message] (Null terminated string)
        /// </summary>
        /// <param name="args"></param>
        public void LEDText(char[] args)
        {
            _service.Send(3013, [args], _ip);
        }

        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (-1, 1)-> (-100%, 100%)
        /// </summary>
        public void OnMotorSpeeds(RoveCommCallback<float> handler) { _service.On(3100, handler); }

        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (A)
        /// </summary>
        public void OnMotorCurrents(RoveCommCallback<float> handler) { _service.On(3101, handler); }

        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (A Battery side)
        /// </summary>
        public void OnVESCCurrents(RoveCommCallback<float> handler) { _service.On(3102, handler); }

        /// <summary>
        /// [Roll, Pitch] (deg)
        /// </summary>
        public void OnIMUData(RoveCommCallback<float> handler) { _service.On(3103, handler); }

        /// <summary>
        /// [X, Y, Z] (m/s2)
        /// </summary>
        public void OnAccelerometerData(RoveCommCallback<float> handler) { _service.On(3104, handler); }

        /// <summary>
        /// [MotorID, FaultCode]
        /// </summary>
        public void OnVESCFault(RoveCommCallback<byte> handler) { _service.On(3200, handler); }

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
        
        public void EStop()
        {
            _service.Send<byte>(4000, [], _ip);
        }

        /// <summary>
        /// Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
        /// </summary>
        
        public void Suicide()
        {
            _service.Send<byte>(4001, [], _ip);
        }

        /// <summary>
        /// Cycle all systems including network off and back on (PMS will stay on)
        /// </summary>
        
        public void Reboot()
        {
            _service.Send<byte>(4002, [], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked enable)
        /// </summary>
        /// <param name="arg1"></param>
        public void EnableBus(byte arg1)
        {
            _service.Send(4003, [arg1], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked disable)
        /// </summary>
        /// <param name="arg1"></param>
        public void DisableBus(byte arg1)
        {
            _service.Send(4004, [arg1], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmasked enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void SetBus(byte arg1)
        {
            _service.Send(4005, [arg1], _ip);
        }

        /// <summary>
        /// [PackCurrent, AuxCurrent, LowCurrent, NetworkCurrent, RadioM2Current, RadioM9Current, Cell1Voltage, Cell2Voltage, Cell3Voltage, Cell4Voltage, Cell5Voltage, Cell6Voltage] (A, A, A, A, A, A, V, V, V, V, V, V)
        /// </summary>
        public void OnCurrentAndVoltage(RoveCommCallback<float> handler) { _service.On(4100, handler); }

        /// <summary>
        /// [Motor, Core, Aux, RadioM2, RadioM9, Network] (bitmasked) [1-Enabled, 0-Disabled]
        /// </summary>
        public void OnBusStatus(RoveCommCallback<byte> handler) { _service.On(4101, handler); }

        /// <summary>
        /// Higher current draw than the battery can support. Rover will Reboot automatically
        /// </summary>
        public void OnPackOvercurrent(RoveCommCallback<byte> handler) { _service.On(4200, handler); }

        /// <summary>
        /// [C1, C2, C3, C4, C5, C6] (bitmasked undervolt). Rover will EStop automatically
        /// </summary>
        public void OnCellUndervoltage(RoveCommCallback<byte> handler) { _service.On(4201, handler); }

        /// <summary>
        /// [C1, C2, C3, C4, C5, C6] (bitmasked critical). Rover will Suicide automatically
        /// </summary>
        public void OnCellCritical(RoveCommCallback<byte> handler) { _service.On(4202, handler); }

        /// <summary>
        /// Aux system current draw too high. Rover will disable Aux bus automatically
        /// </summary>
        public void OnAuxOvercurrent(RoveCommCallback<byte> handler) { _service.On(4203, handler); }
    }

    public class Nav
    {
        private RoveCommService _service;

        internal Nav(RoveCommService service) => _service = service;

        /// <summary>
        /// [Lat, Long, Alt, horizontal_accur, vertical_accur, heading_accur, fix_type, is_differential] [degrees, degrees, meters, meters, meters, degrees, ublox_navpvt fix type (http://docs.ros.org/en/noetic/api/ublox_msgs/html/msg/NavPVT.html), boolean]]
        /// </summary>
        public void OnGPSLatLonAlt(RoveCommCallback<double> handler) { _service.On(6100, handler); }

        /// <summary>
        /// [Heading] [ 0, 360 ]
        /// </summary>
        public void OnCompassData(RoveCommCallback<float> handler) { _service.On(6102, handler); }

        /// <summary>
        /// [Number of satellites]
        /// </summary>
        public void OnSatelliteCountData(RoveCommCallback<byte> handler) { _service.On(6103, handler); }

        /// <summary>
        /// 
        /// </summary>
        public void OnGPSLockError(RoveCommCallback<byte> handler) { _service.On(6200, handler); }
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

        /// <summary>
        /// [Heading] [0, 360)
        /// </summary>
        public void OnCompassAngle(RoveCommCallback<float> handler) { _service.On(7100, handler); }

        /// <summary>
        /// (1-Watchdog timeout, 0-OK)
        /// </summary>
        public void OnWatchdogStatus(RoveCommCallback<byte> handler) { _service.On(7200, handler); }
    }

    public class Arm
    {
        private RoveCommService _service;
private static string _ip = "192.168.2.107";

        internal Arm(RoveCommService service) => _service = service;

        /// <summary>
        /// [X, J2, J3, J4, P, R] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void OpenLoop(short X, short J2, short J3, short J4, short P, short R)
        {
            _service.Send(8000, [X, J2, J3, J4, P, R], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        public void TargetAngle(float X, float J2, float J3, float J4, float P, float R)
        {
            _service.Send(8001, [X, J2, J3, J4, P, R], _ip);
        }

        /// <summary>
        /// [Gripper] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Gripper"></param>
        public void GripperOpenLoop(float Gripper)
        {
            _service.Send(8002, [Gripper], _ip);
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
        public void IKPosition(float X, float Y, float Z, float J4, float P, float R)
        {
            _service.Send(8003, [X, Y, Z, J4, P, R], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void Laser(byte Enabled)
        {
            _service.Send(8004, [Enabled], _ip);
        }

        /// <summary>
        /// [Position] (-180 - 180)
        /// </summary>
        /// <param name="Position"></param>
        public void LinearServo(byte Position)
        {
            _service.Send(8005, [Position], _ip);
        }

        /// <summary>
        /// [Position] (-180 - 180)
        /// </summary>
        /// <param name="Position"></param>
        public void Cache(byte Position)
        {
            _service.Send(8006, [Position], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.Send(8007, [Enabled], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (bitmask override enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(ushort arg1)
        {
            _service.Send(8008, [arg1], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (bitmasked override enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void ClosedLoopOverride(byte arg1)
        {
            _service.Send(8009, [arg1], _ip);
        }

        /// <summary>
        /// [X, Roll] (bitmask start calibration)
        /// </summary>
        /// <param name="arg1"></param>
        public void CalibrateEncoder(byte arg1)
        {
            _service.Send(8010, [arg1], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (bitmask override enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void SoftLimitOverride(ushort arg1)
        {
            _service.Send(8011, [arg1], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void ArmGimbal1(short Pan, short Tilt)
        {
            _service.Send(8012, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void ArmGimbal2(short Pan, short Tilt)
        {
            _service.Send(8013, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, P, R, Y, Z] (in, deg, deg, deg, deg, deg, deg, deg, in, in)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(8100, handler); }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<ushort> handler) { _service.On(8101, handler); }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (bitmask triggered)
        /// </summary>
        public void OnSoftLimit(RoveCommCallback<ushort> handler) { _service.On(8102, handler); }

        /// <summary>
        /// [X, J2, J3, J4, P, R] (Ping Time ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(8103, handler); }
    }

    public class Auger
    {
        private RoveCommService _service;
private static string _ip = "192.168.2.108";

        internal Auger(RoveCommService service) => _service = service;

        /// <summary>
        /// [Speed] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void AugerAxis(short Speed)
        {
            _service.Send(9000, [Speed], _ip);
        }

        /// <summary>
        /// [AugerAxis+, AugerAxis-] (bitmask override enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(byte arg1)
        {
            _service.Send(9001, [arg1], _ip);
        }

        /// <summary>
        /// Request calibration of the AugerAxis encoder
        /// </summary>
        
        public void CalibrateEncoder()
        {
            _service.Send<byte>(9002, [], _ip);
        }

        /// <summary>
        /// [Speed] (-1000 - 1000) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void RunAuger(short Speed)
        {
            _service.Send(9003, [Speed], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.Send(9004, [Enabled], _ip);
        }

        /// <summary>
        /// [White, 365, 405, 500] (0 - 255) -> (Off - Full Brightness)
        /// </summary>
        /// <param name="White"></param>
        /// <param name="_365"></param>
        /// <param name="_405"></param>
        /// <param name="_500"></param>
        public void LED(byte White, byte _365, byte _405, byte _500)
        {
            _service.Send(9005, [White, _365, _405, _500], _ip);
        }

        /// <summary>
        /// [AFFilters, SoilTrapdoor] (-180deg - 180deg)
        /// </summary>
        /// <param name="AFFilters"></param>
        /// <param name="SoilTrapdoor"></param>
        public void AugerServo(short AFFilters, short SoilTrapdoor)
        {
            _service.Send(9006, [AFFilters, SoilTrapdoor], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (-180deg - 180deg)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void AugerGimbal(short Pan, short Tilt)
        {
            _service.Send(9007, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [AugerAxis] (in)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(9100, handler); }

        /// <summary>
        /// [AugerSpeed] (rpm)
        /// </summary>
        public void OnAugerSpeed(RoveCommCallback<float> handler) { _service.On(9101, handler); }

        /// <summary>
        /// [AugerAxis+, AugerAxis-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<byte> handler) { _service.On(9102, handler); }

        /// <summary>
        /// [Temperature, Humidity, N, P, K, pH] (degrees C, relative humidity %, ?, ?, ?, ?)
        /// </summary>
        public void OnEnvironmental(RoveCommCallback<float> handler) { _service.On(9103, handler); }

        /// <summary>
        /// [AugerCurrent] (A)
        /// </summary>
        public void OnAugerCurrent(RoveCommCallback<float> handler) { _service.On(9104, handler); }

        /// <summary>
        /// [AugerAxis Ping Time] (ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(9105, handler); }
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
        /// <param name="Enum0"></param>
        /// <param name="Enum1"></param>
        /// <param name="Enum2"></param>
        public void SetLoggingLevels(byte Enum0, byte Enum1, byte Enum2)
        {
            _service.Send(11009, [Enum0, Enum1, Enum2], _ip);
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

        /// <summary>
        /// Enum (AUTONOMYSTATE)
        /// </summary>
        public void OnCurrentState(RoveCommCallback<byte> handler) { _service.On(11100, handler); }

        /// <summary>
        /// [Teleop, Autonomy, Reached Goal] (enum)
        /// </summary>
        public void OnStateDisplay(RoveCommCallback<byte> handler) { _service.On(11101, handler); }

        /// <summary>
        /// String version of most current error log
        /// </summary>
        public void OnCurrentLog(RoveCommCallback<char> handler) { _service.On(11102, handler); }

        /// <summary>
        /// [Thread Enum ID, FPS Value]
        /// </summary>
        public void OnThreadFPS(RoveCommCallback<uint> handler) { _service.On(11103, handler); }

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
            Reversing = 8,
            Stuck = 9,
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
            RearCam = 3,
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

        /// <summary>
        /// Number of detected cameras.
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(12100, handler); }

        /// <summary>
        /// Number of streaming cameras.
        /// </summary>
        public void OnStreamingCameras(RoveCommCallback<byte> handler) { _service.On(12101, handler); }

        /// <summary>
        /// Picture has been taken.
        /// </summary>
        public void OnPictureTaken(RoveCommCallback<byte> handler) { _service.On(12102, handler); }

        /// <summary>
        /// [cpu0, cpu1, cpu2, cpu3, mem, storage], (% usage)
        /// </summary>
        public void OnUtilization(RoveCommCallback<byte> handler) { _service.On(12103, handler); }
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

        /// <summary>
        /// Number of detected cameras.
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(13100, handler); }

        /// <summary>
        /// Number of streaming cameras.
        /// </summary>
        public void OnStreamingCameras(RoveCommCallback<byte> handler) { _service.On(13101, handler); }

        /// <summary>
        /// Picture has been taken.
        /// </summary>
        public void OnPictureTaken(RoveCommCallback<byte> handler) { _service.On(13102, handler); }

        /// <summary>
        /// [cpu0, cpu1, cpu2, cpu3, mem, storage], (% usage)
        /// </summary>
        public void OnUtilization(RoveCommCallback<byte> handler) { _service.On(13103, handler); }
    }

    public class CameraServer
    {
        private RoveCommService _service;
private static string _ip = "192.168.4.102";

        internal CameraServer(RoveCommService service) => _service = service;

        /// <summary>
        /// Take a picture with the current camera. [0] is the camera to take a picture with.
        /// </summary>
        /// <param name="akeapicturewiththecurrentcamera0"></param>
        public void TakePhoto(byte akeapicturewiththecurrentcamera0)
        {
            _service.Send(14000, [akeapicturewiththecurrentcamera0], _ip);
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

        /// <summary>
        /// Bitmask values for which cameras are able to stream. LSB is Camera 0, MSB is Camera 7.
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(14100, handler); }

        /// <summary>
        /// Which cameras the system is currently streaming on each port
        /// </summary>
        public void OnStreamingCameras(RoveCommCallback<byte> handler) { _service.On(14101, handler); }

        /// <summary>
        /// Picture has been taken.
        /// </summary>
        public void OnPictureTaken1(RoveCommCallback<byte> handler) { _service.On(14102, handler); }

        /// <summary>
        /// Camera has errored and stopped streaming. [0] is ID of camera as an integer (not bitmask).
        /// </summary>
        public void OnCameraUnavailable(RoveCommCallback<byte> handler) { _service.On(14200, handler); }
    }

    public class Raman
    {
        private RoveCommService _service;
private static string _ip = "192.168.3.105";

        internal Raman(RoveCommService service) => _service = service;

        /// <summary>
        /// [Speed] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void InstrumentsAxis(short Speed)
        {
            _service.Send(16000, [Speed], _ip);
        }

        /// <summary>
        /// [InstrumentsAxis+, InstrumentsAxis-] (bitmask override enabled)
        /// </summary>
        /// <param name="arg1"></param>
        public void LimitSwitchOverride(byte arg1)
        {
            _service.Send(16001, [arg1], _ip);
        }

        /// <summary>
        /// Request calibration of the InstrumentsAxis encoder
        /// </summary>
        
        public void CalibrateEncoder()
        {
            _service.Send<byte>(16002, [], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.Send(16003, [Enabled], _ip);
        }

        /// <summary>
        /// [0-disable, 1-enable]
        /// </summary>
        /// <param name="arg1"></param>
        public void Laser(byte arg1)
        {
            _service.Send(16004, [arg1], _ip);
        }

        /// <summary>
        /// [Integration Time] (ms)
        /// </summary>
        /// <param name="IntegrationTime"></param>
        public void RequestRamanReading(uint IntegrationTime)
        {
            _service.Send(16005, [IntegrationTime], _ip);
        }

        /// <summary>
        /// [InstrumentsAxis, TOF] (mm)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(16100, handler); }

        /// <summary>
        /// [InstrumentsAxis+, InstrumentsAxis-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<byte> handler) { _service.On(16101, handler); }

        /// <summary>
        /// Raman CCD elements 1-512
        /// </summary>
        public void OnRamanReading_Part1(RoveCommCallback<ushort> handler) { _service.On(16102, handler); }

        /// <summary>
        /// Raman CCD elements 513-1024
        /// </summary>
        public void OnRamanReading_Part2(RoveCommCallback<ushort> handler) { _service.On(16103, handler); }

        /// <summary>
        /// Raman CCD elements 1025-1536
        /// </summary>
        public void OnRamanReading_Part3(RoveCommCallback<ushort> handler) { _service.On(16104, handler); }

        /// <summary>
        /// Raman CCD elements 1537-2048
        /// </summary>
        public void OnRamanReading_Part4(RoveCommCallback<ushort> handler) { _service.On(16105, handler); }

        /// <summary>
        /// [InstrumentsAxis Ping Time] (ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(16106, handler); }
    }

    public class RoveSoSimulator
    {
        private RoveCommService _service;

        internal RoveSoSimulator(RoveCommService service) => _service = service;

        /// <summary>
        /// [Accel X, Accel Y, Accel Z, Gyro X, Gyro Y, Gyro Z, Quat X, Quat Y, Quat Z, Quat W]
        /// </summary>
        public void OnIMU(RoveCommCallback<double> handler) { _service.On(99100, handler); }
    }
}
