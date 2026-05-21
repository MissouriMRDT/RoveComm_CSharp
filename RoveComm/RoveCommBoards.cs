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
        }
    }
}

namespace RoveComm.Boards
{
    public class Core
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.110";

        internal Core(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[3100] = new float[6];
            _service.UDP._telemetryFloat[3101] = new float[6];
            _service.UDP._telemetryFloat[3102] = new float[6];
            _service.UDP._telemetryFloat[3103] = new float[2];
            _service.UDP._telemetryFloat[3104] = new float[3];
            _service.UDP._telemetryFloat[3105] = new float[3];
            _service.UDP._telemetryUInt8[3200] = new byte[2];
        }
        /// <summary>
        /// [LeftSpeed, RightSpeed] (-1 - 1) -> (-100% - 100%)
        /// </summary>
        /// <param name="LeftSpeed"></param>
        /// <param name="RightSpeed"></param>
        public void DriveLeftRight(float LeftSpeed, float RightSpeed)
        {
            _service.SendBG(3000, [LeftSpeed, RightSpeed], _ip);
        }

        /// <summary>
        /// [LF, LM, LR, RF, RM, RR] (-1 - 1) -> (-100% - 100%)
        /// </summary>
        /// <param name="LF"></param>
        /// <param name="LM"></param>
        /// <param name="LR"></param>
        /// <param name="RF"></param>
        /// <param name="RM"></param>
        /// <param name="RR"></param>
        public void DriveIndividual(float LF, float LM, float LR, float RF, float RM, float RR)
        {
            _service.SendBG(3001, [LF, LM, LR, RF, RM, RR], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.SendBG(3002, [Enabled], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void LeftGimbal(short Pan, short Tilt)
        {
            _service.SendBG(3003, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void RightGimbal(short Pan, short Tilt)
        {
            _service.SendBG(3004, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void BackGimbal(short Pan, short Tilt)
        {
            _service.SendBG(3005, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [R, G, B] (brightness 0 - 255)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void LEDRGB(byte R, byte G, byte B)
        {
            _service.SendBG(3006, [R, G, B], _ip);
        }

        /// <summary>
        /// [Color] (RGBA)
        /// </summary>
        /// <param name="Data"></param>

        public void BackImage(uint[] Data)
        {
            _service.SendBG(3007, Data, _ip);
        }

        /// <summary>
        /// [R, G, B] (brightness 0 - 255)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public void InternalRGB(byte R, byte G, byte B)
        {
            _service.SendBG(3008, [R, G, B], _ip);
        }

        /// <summary>
        /// [Color] (RGBA)
        /// </summary>
        /// <param name="Data"></param>

        public void InternalImage(uint[] Data)
        {
            _service.SendBG(3009, Data, _ip);
        }

        /// <summary>
        /// [State] (DisplayState)
        /// </summary>
        /// <param name="State"></param>
        public void StateDisplay(byte State)
        {
            _service.SendBG(3010, [State], _ip);
        }

        /// <summary>
        /// [Brightness] (0 - 255)
        /// </summary>
        /// <param name="Brightness"></param>
        public void Brightness(byte Brightness)
        {
            _service.SendBG(3011, [Brightness], _ip);
        }

        /// <summary>
        /// [Mode] (0: Teleop 1: Autonomy)
        /// </summary>
        /// <param name="Mode"></param>
        public void SetWatchdogMode(byte Mode)
        {
            _service.SendBG(3012, [Mode], _ip);
        }

        /// <summary>
        /// [Message] (null terminated string)
        /// </summary>
        /// <param name="Data"></param>

        public void LEDText(char[] Data)
        {
            _service.SendBG(3013, Data, _ip);
        }

        public float[] MotorSpeeds { get => _service.UDP._telemetryFloat[3100]; }
        public float MotorSpeeds_FL { get => _service.UDP._telemetryFloat[3100][0]; }
        public float MotorSpeeds_ML { get => _service.UDP._telemetryFloat[3100][1]; }
        public float MotorSpeeds_BL { get => _service.UDP._telemetryFloat[3100][2]; }
        public float MotorSpeeds_FR { get => _service.UDP._telemetryFloat[3100][3]; }
        public float MotorSpeeds_MR { get => _service.UDP._telemetryFloat[3100][4]; }
        public float MotorSpeeds_BR { get => _service.UDP._telemetryFloat[3100][5]; }
        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (-1 - 1) -> (-100% - 100%)
        /// </summary>
        public void OnMotorSpeeds(RoveCommCallback<float> handler) { _service.On(3100, handler); }

        public float[] MotorCurrents { get => _service.UDP._telemetryFloat[3101]; }
        public float MotorCurrents_FL { get => _service.UDP._telemetryFloat[3101][0]; }
        public float MotorCurrents_ML { get => _service.UDP._telemetryFloat[3101][1]; }
        public float MotorCurrents_BL { get => _service.UDP._telemetryFloat[3101][2]; }
        public float MotorCurrents_FR { get => _service.UDP._telemetryFloat[3101][3]; }
        public float MotorCurrents_MR { get => _service.UDP._telemetryFloat[3101][4]; }
        public float MotorCurrents_BR { get => _service.UDP._telemetryFloat[3101][5]; }
        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (A)
        /// </summary>
        public void OnMotorCurrents(RoveCommCallback<float> handler) { _service.On(3101, handler); }

        public float[] VESCCurrents { get => _service.UDP._telemetryFloat[3102]; }
        public float VESCCurrents_FL { get => _service.UDP._telemetryFloat[3102][0]; }
        public float VESCCurrents_ML { get => _service.UDP._telemetryFloat[3102][1]; }
        public float VESCCurrents_BL { get => _service.UDP._telemetryFloat[3102][2]; }
        public float VESCCurrents_FR { get => _service.UDP._telemetryFloat[3102][3]; }
        public float VESCCurrents_MR { get => _service.UDP._telemetryFloat[3102][4]; }
        public float VESCCurrents_BR { get => _service.UDP._telemetryFloat[3102][5]; }
        /// <summary>
        /// [FL, ML, BL, FR, MR, BR] (A battery side)
        /// </summary>
        public void OnVESCCurrents(RoveCommCallback<float> handler) { _service.On(3102, handler); }

        public float[] IMUData { get => _service.UDP._telemetryFloat[3103]; }
        public float IMUData_Roll { get => _service.UDP._telemetryFloat[3103][0]; }
        public float IMUData_Pitch { get => _service.UDP._telemetryFloat[3103][1]; }
        /// <summary>
        /// [Roll, Pitch] (deg)
        /// </summary>
        public void OnIMUData(RoveCommCallback<float> handler) { _service.On(3103, handler); }

        public float[] AccelerometerData { get => _service.UDP._telemetryFloat[3104]; }
        public float AccelerometerData_X { get => _service.UDP._telemetryFloat[3104][0]; }
        public float AccelerometerData_Y { get => _service.UDP._telemetryFloat[3104][1]; }
        public float AccelerometerData_Z { get => _service.UDP._telemetryFloat[3104][2]; }
        /// <summary>
        /// [X, Y, Z] (m/s2)
        /// </summary>
        public void OnAccelerometerData(RoveCommCallback<float> handler) { _service.On(3104, handler); }

        public float[] Thermal { get => _service.UDP._telemetryFloat[3105]; }
        public float Thermal_FanSpeed { get => _service.UDP._telemetryFloat[3105][0]; }
        public float Thermal_BoardTemperature { get => _service.UDP._telemetryFloat[3105][1]; }
        public float Thermal_OtherTemperature { get => _service.UDP._telemetryFloat[3105][2]; }
        /// <summary>
        /// [FanSpeed, BoardTemperature, OtherTemperature] (rpm, C, C)
        /// </summary>
        public void OnThermal(RoveCommCallback<float> handler) { _service.On(3105, handler); }

        public byte[] VESCFault { get => _service.UDP._telemetryUInt8[3200]; }
        public byte VESCFault_MotorID { get => _service.UDP._telemetryUInt8[3200][0]; }
        public byte VESCFault_FaultCode { get => _service.UDP._telemetryUInt8[3200][1]; }
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

        internal PMS(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[4100] = new float[12];
            _service.UDP._telemetryUInt8[4101] = new byte[1];
            _service.UDP._telemetryUInt8[4201] = new byte[1];
            _service.UDP._telemetryUInt8[4202] = new byte[1];
        }
        /// <summary>
        /// Power off all systems except network (PMS will stay on)
        /// </summary>
        public void EStop()
        {
            _service.SendBG<byte>(4000, [], _ip);
        }

        /// <summary>
        /// Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
        /// </summary>
        public void Suicide()
        {
            _service.SendBG<byte>(4001, [], _ip);
        }

        /// <summary>
        /// Cycle all systems including network off and back on (PMS will stay on)
        /// </summary>
        public void Reboot()
        {
            _service.SendBG<byte>(4002, [], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmask enable)
        /// </summary>
        /// <param name="Data0"></param>
        public void EnableBus(byte Data0)
        {
            _service.SendBG(4003, [Data0], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmask disable)
        /// </summary>
        /// <param name="Data0"></param>
        public void DisableBus(byte Data0)
        {
            _service.SendBG(4004, [Data0], _ip);
        }

        /// <summary>
        /// [Motor, Core, Aux] (bitmask enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void SetBus(byte Data0)
        {
            _service.SendBG(4005, [Data0], _ip);
        }

        public float[] CurrentAndVoltage { get => _service.UDP._telemetryFloat[4100]; }
        public float CurrentAndVoltage_PackCurrent { get => _service.UDP._telemetryFloat[4100][0]; }
        public float CurrentAndVoltage_AuxCurrent { get => _service.UDP._telemetryFloat[4100][1]; }
        public float CurrentAndVoltage_LowCurrent { get => _service.UDP._telemetryFloat[4100][2]; }
        public float CurrentAndVoltage_NetworkCurrent { get => _service.UDP._telemetryFloat[4100][3]; }
        public float CurrentAndVoltage_RadioM2Current { get => _service.UDP._telemetryFloat[4100][4]; }
        public float CurrentAndVoltage_RadioM9Current { get => _service.UDP._telemetryFloat[4100][5]; }
        public float CurrentAndVoltage_Cell1Voltage { get => _service.UDP._telemetryFloat[4100][6]; }
        public float CurrentAndVoltage_Cell2Voltage { get => _service.UDP._telemetryFloat[4100][7]; }
        public float CurrentAndVoltage_Cell3Voltage { get => _service.UDP._telemetryFloat[4100][8]; }
        public float CurrentAndVoltage_Cell4Voltage { get => _service.UDP._telemetryFloat[4100][9]; }
        public float CurrentAndVoltage_Cell5Voltage { get => _service.UDP._telemetryFloat[4100][10]; }
        public float CurrentAndVoltage_Cell6Voltage { get => _service.UDP._telemetryFloat[4100][11]; }
        /// <summary>
        /// [PackCurrent, AuxCurrent, LowCurrent, NetworkCurrent, RadioM2Current, RadioM9Current, Cell1Voltage, Cell2Voltage, Cell3Voltage, Cell4Voltage, Cell5Voltage, Cell6Voltage] (A, A, A, A, A, A, V, V, V, V, V, V)
        /// </summary>
        public void OnCurrentAndVoltage(RoveCommCallback<float> handler) { _service.On(4100, handler); }

        public byte BusStatus { get => _service.UDP._telemetryUInt8[4101][0]; }
        /// <summary>
        /// [Motor, Core, Aux, RadioM2, RadioM9, Network] (bitmask enabled)
        /// </summary>
        public void OnBusStatus(RoveCommCallback<byte> handler) { _service.On(4101, handler); }

        /// <summary>
        /// Higher current draw than the battery can support. Rover will Reboot automatically
        /// </summary>
        public void OnPackOvercurrent(RoveCommCallback<byte> handler) { _service.On(4200, handler); }

        public byte CellUndervoltage { get => _service.UDP._telemetryUInt8[4201][0]; }
        /// <summary>
        /// [C1, C2, C3, C4, C5, C6] (bitmask undervolt) Rover will EStop automatically
        /// </summary>
        public void OnCellUndervoltage(RoveCommCallback<byte> handler) { _service.On(4201, handler); }

        public byte CellCritical { get => _service.UDP._telemetryUInt8[4202][0]; }
        /// <summary>
        /// [C1, C2, C3, C4, C5, C6] (bitmask critical) Rover will Suicide automatically
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

        internal Nav(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryDouble[6100] = new double[8];
            _service.UDP._telemetryFloat[6102] = new float[1];
            _service.UDP._telemetryUInt8[6103] = new byte[1];
        }
        public double[] GPSLatLonAlt { get => _service.UDP._telemetryDouble[6100]; }
        public double GPSLatLonAlt_Lat { get => _service.UDP._telemetryDouble[6100][0]; }
        public double GPSLatLonAlt_Lon { get => _service.UDP._telemetryDouble[6100][1]; }
        public double GPSLatLonAlt_Alt { get => _service.UDP._telemetryDouble[6100][2]; }
        public double GPSLatLonAlt_HorizontalAccuracy { get => _service.UDP._telemetryDouble[6100][3]; }
        public double GPSLatLonAlt_VerticalAccuracy { get => _service.UDP._telemetryDouble[6100][4]; }
        public double GPSLatLonAlt_HeadingAccuracy { get => _service.UDP._telemetryDouble[6100][5]; }
        public double GPSLatLonAlt_FixType { get => _service.UDP._telemetryDouble[6100][6]; }
        public double GPSLatLonAlt_IsDifferential { get => _service.UDP._telemetryDouble[6100][7]; }
        /// <summary>
        /// [Lat, Lon, Alt, HorizontalAccuracy, VerticalAccuracy, HeadingAccuracy, FixType, IsDifferential] (deg, deg, m, m, m, deg, ublox_navpvt fix type http://docs.ros.org/en/noetic/api/ublox_msgs/html/msg/NavPVT.html, bool)
        /// </summary>
        public void OnGPSLatLonAlt(RoveCommCallback<double> handler) { _service.On(6100, handler); }

        public float CompassData { get => _service.UDP._telemetryFloat[6102][0]; }
        /// <summary>
        /// [Heading] (0 - 360)
        /// </summary>
        public void OnCompassData(RoveCommCallback<float> handler) { _service.On(6102, handler); }

        public byte SatelliteCountData { get => _service.UDP._telemetryUInt8[6103][0]; }
        /// <summary>
        /// [Satellites]
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

        internal SignalStack(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[7100] = new float[1];
        }
        /// <summary>
        /// [Speed] (-1000 - 1000) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void OpenLoop(short Speed)
        {
            _service.SendBG(7000, [Speed], _ip);
        }

        /// <summary>
        /// [Heading] (0 - 360)
        /// </summary>
        /// <param name="Heading"></param>
        public void SetAngleTarget(float Heading)
        {
            _service.SendBG(7001, [Heading], _ip);
        }

        /// <summary>
        /// [Rover Lat, Rover Lon, Basestation Lat, Basestation Lon] (-90 - 90, -180 - 180, -90 - 90, -180 - 180)
        /// </summary>
        /// <param name="RoverLat"></param>
        /// <param name="RoverLon"></param>
        /// <param name="BasestationLat"></param>
        /// <param name="BasestationLon"></param>
        public void SetGPSTarget(double RoverLat, double RoverLon, double BasestationLat, double BasestationLon)
        {
            _service.SendBG(7002, [RoverLat, RoverLon, BasestationLat, BasestationLon], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.SendBG(7003, [Enabled], _ip);
        }

        public float CompassAngle { get => _service.UDP._telemetryFloat[7100][0]; }
        /// <summary>
        /// [Heading] (0 - 360)
        /// </summary>
        public void OnCompassAngle(RoveCommCallback<float> handler) { _service.On(7100, handler); }
    }

    public class Arm
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.107";

        internal Arm(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[8100] = new float[9];
            _service.UDP._telemetryUInt16[8101] = new ushort[1];
            _service.UDP._telemetryUInt16[8102] = new ushort[1];
            _service.UDP._telemetryUInt16[8103] = new ushort[7];
            _service.UDP._telemetryFloat[8104] = new float[6];
        }
        /// <summary>
        /// [X, J2, J3, J4, J5, J6] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        public void OpenLoop(short X, short J2, short J3, short J4, short J5, short J6)
        {
            _service.SendBG(8000, [X, J2, J3, J4, J5, J6], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        public void TargetAngle(float X, float J2, float J3, float J4, float J5, float J6)
        {
            _service.SendBG(8001, [X, J2, J3, J4, J5, J6], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        public void TargetAngleIncrement(float X, float J2, float J3, float J4, float J5, float J6)
        {
            _service.SendBG(8002, [X, J2, J3, J4, J5, J6], _ip);
        }

        /// <summary>
        /// [Gripper] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Gripper"></param>
        public void GripperOpenLoop(short Gripper)
        {
            _service.SendBG(8003, [Gripper], _ip);
        }

        /// <summary>
        /// [X, Y, Z, J4, J5, J6] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        public void IKPosition(float X, float Y, float Z, float J4, float J5, float J6)
        {
            _service.SendBG(8004, [X, Y, Z, J4, J5, J6], _ip);
        }

        /// <summary>
        /// [X, Y, Z, J4, J5, J6] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        public void IKWristIncrement(float X, float Y, float Z, float J4, float J5, float J6)
        {
            _service.SendBG(8005, [X, Y, Z, J4, J5, J6], _ip);
        }

        /// <summary>
        /// [TX, TY, TZ, RX, RY, RZ] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="TX"></param>
        /// <param name="TY"></param>
        /// <param name="TZ"></param>
        /// <param name="RX"></param>
        /// <param name="RY"></param>
        /// <param name="RZ"></param>
        public void IKWorldIncrement(float TX, float TY, float TZ, float RX, float RY, float RZ)
        {
            _service.SendBG(8006, [TX, TY, TZ, RX, RY, RZ], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void Laser(byte Enabled)
        {
            _service.SendBG(8007, [Enabled], _ip);
        }

        /// <summary>
        /// [Position] (0 - 180)
        /// </summary>
        /// <param name="Position"></param>
        public void LinearServo(byte Position)
        {
            _service.SendBG(8008, [Position], _ip);
        }

        /// <summary>
        /// [Position] (0 - 180)
        /// </summary>
        /// <param name="Position"></param>
        public void Cache(byte Position)
        {
            _service.SendBG(8009, [Position], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.SendBG(8010, [Enabled], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask override enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void LimitSwitchOverride(ushort Data0)
        {
            _service.SendBG(8011, [Data0], _ip);
        }

        /// <summary>
        /// [X, J2, J3, J4, J5, J6] (bitmask override enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void ClosedLoopOverride(byte Data0)
        {
            _service.SendBG(8012, [Data0], _ip);
        }

        /// <summary>
        /// [X, Roll] (bitmask start calibration)
        /// </summary>
        /// <param name="Data0"></param>
        public void CalibrateEncoder(byte Data0)
        {
            _service.SendBG(8013, [Data0], _ip);
        }

        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask override enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void SoftLimitOverride(ushort Data0)
        {
            _service.SendBG(8014, [Data0], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void ArmGimbal1(short Pan, short Tilt)
        {
            _service.SendBG(8015, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void ArmGimbal2(short Pan, short Tilt)
        {
            _service.SendBG(8016, [Pan, Tilt], _ip);
        }

        /// <summary>
        /// [TX, TY, TZ, RX, RY, RZ] (in, in, in, deg, deg, deg)
        /// </summary>
        /// <param name="TX"></param>
        /// <param name="TY"></param>
        /// <param name="TZ"></param>
        /// <param name="RX"></param>
        /// <param name="RY"></param>
        /// <param name="RZ"></param>
        public void IKToolIncrement(float TX, float TY, float TZ, float RX, float RY, float RZ)
        {
            _service.SendBG(8017, [TX, TY, TZ, RX, RY, RZ], _ip);
        }

        public float[] Position { get => _service.UDP._telemetryFloat[8100]; }
        public float Position_X { get => _service.UDP._telemetryFloat[8100][0]; }
        public float Position_J2 { get => _service.UDP._telemetryFloat[8100][1]; }
        public float Position_J3 { get => _service.UDP._telemetryFloat[8100][2]; }
        public float Position_J4 { get => _service.UDP._telemetryFloat[8100][3]; }
        public float Position_J5 { get => _service.UDP._telemetryFloat[8100][4]; }
        public float Position_J6 { get => _service.UDP._telemetryFloat[8100][5]; }
        public float Position_GX { get => _service.UDP._telemetryFloat[8100][6]; }
        public float Position_GY { get => _service.UDP._telemetryFloat[8100][7]; }
        public float Position_GZ { get => _service.UDP._telemetryFloat[8100][8]; }
        /// <summary>
        /// [X, J2, J3, J4, J5, J6, GX, GY, GZ] (in, deg, deg, deg, deg, deg, in, in, in)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(8100, handler); }

        public ushort LimitSwitch { get => _service.UDP._telemetryUInt16[8101][0]; }
        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<ushort> handler) { _service.On(8101, handler); }

        public ushort SoftLimit { get => _service.UDP._telemetryUInt16[8102][0]; }
        /// <summary>
        /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask triggered)
        /// </summary>
        public void OnSoftLimit(RoveCommCallback<ushort> handler) { _service.On(8102, handler); }

        public ushort[] SMOCOPing { get => _service.UDP._telemetryUInt16[8103]; }
        public ushort SMOCOPing_X { get => _service.UDP._telemetryUInt16[8103][0]; }
        public ushort SMOCOPing_J2 { get => _service.UDP._telemetryUInt16[8103][1]; }
        public ushort SMOCOPing_J3 { get => _service.UDP._telemetryUInt16[8103][2]; }
        public ushort SMOCOPing_J4 { get => _service.UDP._telemetryUInt16[8103][3]; }
        public ushort SMOCOPing_J5 { get => _service.UDP._telemetryUInt16[8103][4]; }
        public ushort SMOCOPing_J6 { get => _service.UDP._telemetryUInt16[8103][5]; }
        public ushort SMOCOPing_G { get => _service.UDP._telemetryUInt16[8103][6]; }
        /// <summary>
        /// [X, J2, J3, J4, J5, J6, G] (ping time ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(8103, handler); }

        public float[] Target { get => _service.UDP._telemetryFloat[8104]; }
        public float Target_X { get => _service.UDP._telemetryFloat[8104][0]; }
        public float Target_J2 { get => _service.UDP._telemetryFloat[8104][1]; }
        public float Target_J3 { get => _service.UDP._telemetryFloat[8104][2]; }
        public float Target_J4 { get => _service.UDP._telemetryFloat[8104][3]; }
        public float Target_J5 { get => _service.UDP._telemetryFloat[8104][4]; }
        public float Target_J6 { get => _service.UDP._telemetryFloat[8104][5]; }
        /// <summary>
        /// [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
        /// </summary>
        public void OnTarget(RoveCommCallback<float> handler) { _service.On(8104, handler); }
    }

    public class Auger
    {
        private RoveCommService _service;
        private static string _ip = "192.168.2.108";

        internal Auger(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[9100] = new float[1];
            _service.UDP._telemetryFloat[9101] = new float[1];
            _service.UDP._telemetryUInt8[9102] = new byte[1];
            _service.UDP._telemetryFloat[9103] = new float[2];
            _service.UDP._telemetryFloat[9104] = new float[1];
            _service.UDP._telemetryUInt16[9105] = new ushort[1];
            _service.UDP._telemetryInt32[9106] = new int[1];
        }
        /// <summary>
        /// [Speed] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void AugerAxis(short Speed)
        {
            _service.SendBG(9000, [Speed], _ip);
        }

        /// <summary>
        /// [AugerAxis+, AugerAxis-] (bitmask override enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void LimitSwitchOverride(byte Data0)
        {
            _service.SendBG(9001, [Data0], _ip);
        }

        /// <summary>
        /// Request calibration of the AugerAxis encoder
        /// </summary>
        public void CalibrateEncoder()
        {
            _service.SendBG<byte>(9002, [], _ip);
        }

        /// <summary>
        /// [Speed] (-1000 - 1000) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void RunAuger(short Speed)
        {
            _service.SendBG(9003, [Speed], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.SendBG(9004, [Enabled], _ip);
        }

        /// <summary>
        /// [White, 365, 405, 500] (brightness 0 - 255)
        /// </summary>
        /// <param name="White"></param>
        /// <param name="_365"></param>
        /// <param name="_405"></param>
        /// <param name="_500"></param>
        public void LED(byte White, byte _365, byte _405, byte _500)
        {
            _service.SendBG(9005, [White, _365, _405, _500], _ip);
        }

        /// <summary>
        /// [AFFilters, SoilTrapdoor] (0 - 180)
        /// </summary>
        /// <param name="AFFilters"></param>
        /// <param name="SoilTrapdoor"></param>
        public void AugerServo(short AFFilters, short SoilTrapdoor)
        {
            _service.SendBG(9006, [AFFilters, SoilTrapdoor], _ip);
        }

        /// <summary>
        /// [Pan, Tilt] (0 - 180)
        /// </summary>
        /// <param name="Pan"></param>
        /// <param name="Tilt"></param>
        public void AugerGimbal(short Pan, short Tilt)
        {
            _service.SendBG(9007, [Pan, Tilt], _ip);
        }

        public float Position { get => _service.UDP._telemetryFloat[9100][0]; }
        /// <summary>
        /// [AugerAxis] (in)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(9100, handler); }

        public float AugerSpeed { get => _service.UDP._telemetryFloat[9101][0]; }
        /// <summary>
        /// [AugerSpeed] (rpm)
        /// </summary>
        public void OnAugerSpeed(RoveCommCallback<float> handler) { _service.On(9101, handler); }

        public byte LimitSwitch { get => _service.UDP._telemetryUInt8[9102][0]; }
        /// <summary>
        /// [AugerAxis+, AugerAxis-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<byte> handler) { _service.On(9102, handler); }

        public float[] Environmental { get => _service.UDP._telemetryFloat[9103]; }
        public float Environmental_Temperature { get => _service.UDP._telemetryFloat[9103][0]; }
        public float Environmental_Humidity { get => _service.UDP._telemetryFloat[9103][1]; }
        /// <summary>
        /// [Temperature, Humidity] (C, relative %)
        /// </summary>
        public void OnEnvironmental(RoveCommCallback<float> handler) { _service.On(9103, handler); }

        public float AugerCurrent { get => _service.UDP._telemetryFloat[9104][0]; }
        /// <summary>
        /// [AugerCurrent] (A)
        /// </summary>
        public void OnAugerCurrent(RoveCommCallback<float> handler) { _service.On(9104, handler); }

        public ushort SMOCOPing { get => _service.UDP._telemetryUInt16[9105][0]; }
        /// <summary>
        /// [AugerAxis] (ping time ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(9105, handler); }

        public int LEDStatus { get => _service.UDP._telemetryInt32[9106][0]; }
        /// <summary>
        /// [LEDTimer] (ms)
        /// </summary>
        public void OnLEDStatus(RoveCommCallback<int> handler) { _service.On(9106, handler); }
    }

    public class Autonomy
    {
        private RoveCommService _service;
        private static string _ip = "192.168.3.100";

        internal Autonomy(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryUInt8[11100] = new byte[1];
            _service.UDP._telemetryUInt8[11101] = new byte[1];
            _service.UDP._telemetryUInt32[11103] = new uint[2];
            _service.UDP._telemetryDouble[11104] = new double[1000];
            _service.UDP._telemetryDouble[11105] = new double[1];
        }
        /// <summary>
        /// Start Autonomy_Software
        /// </summary>
        /// <param name="Data0"></param>
        public void StartAutonomy(byte Data0)
        {
            _service.SendBG(11000, [Data0], _ip);
        }

        /// <summary>
        /// Return Autonomy_Software to Idle state
        /// </summary>
        /// <param name="Data0"></param>
        public void DisableAutonomy(byte Data0)
        {
            _service.SendBG(11001, [Data0], _ip);
        }

        /// <summary>
        /// [Lat, Lon, AUTONOMYWAYPOINTTYPES]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="AUTONOMYWAYPOINTTYPES"></param>
        public void AddPositionLeg(double Lat, double Lon, double AUTONOMYWAYPOINTTYPES)
        {
            _service.SendBG(11002, [Lat, Lon, AUTONOMYWAYPOINTTYPES], _ip);
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
            _service.SendBG(11003, [Lat, Lon, AUTONOMYWAYPOINTTYPES, MarkerRadius], _ip);
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
            _service.SendBG(11004, [Lat, Lon, AUTONOMYWAYPOINTTYPES, ObjectRadius], _ip);
        }

        /// <summary>
        /// Clear queued positions, markers, and objects waypoints.
        /// </summary>
        /// <param name="Data0"></param>
        public void ClearWaypoints(byte Data0)
        {
            _service.SendBG(11005, [Data0], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will scale the max power effort of Autonomy.
        /// </summary>
        /// <param name="Data0"></param>
        public void SetMaxSpeed(float Data0)
        {
            _service.SendBG(11006, [Data0], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will filter points from the traversability map. Higher values will result in more conservative pathing.
        /// </summary>
        /// <param name="Data0"></param>
        public void SetMinTravScore(float Data0)
        {
            _service.SendBG(11007, [Data0], _ip);
        }

        /// <summary>
        /// A multiplier from 0.0 to 1.0 that will bias the pathing algorithm towards shorter paths (lower values) or safer paths (higher values).
        /// </summary>
        /// <param name="Data0"></param>
        public void SetBetaBias(float Data0)
        {
            _service.SendBG(11008, [Data0], _ip);
        }

        /// <summary>
        /// [Enum (AUTONOMYLOG), Enum (AUTONOMYLOG), Enum (AUTONOMYLOG)] {Console, File, RoveComm}
        /// </summary>
        /// <param name="Enum0"></param>
        /// <param name="Enum1"></param>
        /// <param name="Enum2"></param>
        public void SetLoggingLevels(byte Enum0, byte Enum1, byte Enum2)
        {
            _service.SendBG(11009, [Enum0, Enum1, Enum2], _ip);
        }

        /// <summary>
        /// [Lat, Lon, ObstacleRadius (meters)]
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lon"></param>
        /// <param name="ObstacleRadius"></param>
        public void AddObstacle(double Lat, double Lon, double ObstacleRadius)
        {
            _service.SendBG(11010, [Lat, Lon, ObstacleRadius], _ip);
        }

        /// <summary>
        /// Clear queued permanent obstacles.
        /// </summary>
        /// <param name="Data0"></param>
        public void ClearObstacles(byte Data0)
        {
            _service.SendBG(11011, [Data0], _ip);
        }

        public byte CurrentState { get => _service.UDP._telemetryUInt8[11100][0]; }
        /// <summary>
        /// [State] (AUTONOMYSTATE)
        /// </summary>
        public void OnCurrentState(RoveCommCallback<byte> handler) { _service.On(11100, handler); }

        public byte StateDisplay { get => _service.UDP._telemetryUInt8[11101][0]; }
        /// <summary>
        /// [State] (0: Teleop 1: Autonomy 2: Reached Goal)
        /// </summary>
        public void OnStateDisplay(RoveCommCallback<byte> handler) { _service.On(11101, handler); }

        public uint[] ThreadFPS { get => _service.UDP._telemetryUInt32[11103]; }
        public uint ThreadFPS_Thread { get => _service.UDP._telemetryUInt32[11103][0]; }
        public uint ThreadFPS_FPS { get => _service.UDP._telemetryUInt32[11103][1]; }
        /// <summary>
        /// [Thread, FPS] (AUTONOMYTHREADS, fps)
        /// </summary>
        public void OnThreadFPS(RoveCommCallback<uint> handler) { _service.On(11103, handler); }

        public double[] PathWaypoints { get => _service.UDP._telemetryDouble[11104]; }
        /// <summary>
        /// [Lat, Lon, Lat, Lon, ...] (deg, deg, deg, deg, ...)
        /// </summary>
        public void OnPathWaypoints(RoveCommCallback<double> handler) { _service.On(11104, handler); }

        public double TimeRemaining { get => _service.UDP._telemetryDouble[11105][0]; }
        /// <summary>
        /// [EstimatedTimeToGoal] (s)
        /// </summary>
        public void OnTimeRemaining(RoveCommCallback<double> handler) { _service.On(11105, handler); }

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

        internal Camera1(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryUInt8[12100] = new byte[2];
            _service.UDP._telemetryUInt8[12102] = new byte[6];
        }
        /// <summary>
        /// [Camera, Restart]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Restart"></param>
        public void TakePicture(byte Camera, byte Restart)
        {
            _service.SendBG(12000, [Camera, Restart], _ip);
        }

        /// <summary>
        /// [Camera, Restart]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Restart"></param>
        public void ToggleStream(byte Camera, byte Restart)
        {
            _service.SendBG(12001, [Camera, Restart], _ip);
        }

        /// <summary>
        /// [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/ffmpeg_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
        /// </summary>
        /// <param name="Data"></param>

        public void SetFFMPEGArguments(char[] Data)
        {
            _service.SendBG(12002, Data, _ip);
        }

        /// <summary>
        /// [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/picture_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
        /// </summary>
        /// <param name="Data"></param>

        public void SetPictureArguments(char[] Data)
        {
            _service.SendBG(12003, Data, _ip);
        }

        /// <summary>
        /// [Command] (0x1f delimited, 0x00 terminated list of commands, first byte is camera index)
        /// </summary>
        /// <param name="Data"></param>

        public void ZMQCommands(char[] Data)
        {
            _service.SendBG(12004, Data, _ip);
        }

        /// <summary>
        /// [Command] (0x00 terminated argument passed to v4l2-ctl --set-ctrl, first byte is camera index)
        /// </summary>
        /// <param name="Data"></param>

        public void V4L2SetControls(char[] Data)
        {
            _service.SendBG(12005, Data, _ip);
        }

        public byte[] AvailableCameras { get => _service.UDP._telemetryUInt8[12100]; }
        public byte AvailableCameras_Connected { get => _service.UDP._telemetryUInt8[12100][0]; }
        public byte AvailableCameras_Streaming { get => _service.UDP._telemetryUInt8[12100][1]; }
        /// <summary>
        /// [Connected, Streaming] (bitmask indexes, bitmask indexes)
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(12100, handler); }

        /// <summary>
        /// Picture has been taken.
        /// </summary>
        public void OnPictureTaken(RoveCommCallback<byte> handler) { _service.On(12101, handler); }

        public byte[] Utilization { get => _service.UDP._telemetryUInt8[12102]; }
        public byte Utilization_cpu0 { get => _service.UDP._telemetryUInt8[12102][0]; }
        public byte Utilization_cpu1 { get => _service.UDP._telemetryUInt8[12102][1]; }
        public byte Utilization_cpu2 { get => _service.UDP._telemetryUInt8[12102][2]; }
        public byte Utilization_cpu3 { get => _service.UDP._telemetryUInt8[12102][3]; }
        public byte Utilization_mem { get => _service.UDP._telemetryUInt8[12102][4]; }
        public byte Utilization_storage { get => _service.UDP._telemetryUInt8[12102][5]; }
        /// <summary>
        /// [cpu0, cpu1, cpu2, cpu3, mem, storage] (% usage)
        /// </summary>
        public void OnUtilization(RoveCommCallback<byte> handler) { _service.On(12102, handler); }
    }

    public class Camera2
    {
        private RoveCommService _service;
        private static string _ip = "192.168.4.101";

        internal Camera2(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryUInt8[13100] = new byte[2];
            _service.UDP._telemetryUInt8[13102] = new byte[6];
        }
        /// <summary>
        /// [Camera, Restart]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Restart"></param>
        public void TakePicture(byte Camera, byte Restart)
        {
            _service.SendBG(13000, [Camera, Restart], _ip);
        }

        /// <summary>
        /// [Camera, Restart]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Restart"></param>
        public void ToggleStream(byte Camera, byte Restart)
        {
            _service.SendBG(13001, [Camera, Restart], _ip);
        }

        /// <summary>
        /// [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/ffmpeg_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
        /// </summary>
        /// <param name="Data"></param>

        public void SetFFMPEGArguments(char[] Data)
        {
            _service.SendBG(13002, Data, _ip);
        }

        /// <summary>
        /// [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/picture_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
        /// </summary>
        /// <param name="Data"></param>

        public void SetPictureArguments(char[] Data)
        {
            _service.SendBG(13003, Data, _ip);
        }

        /// <summary>
        /// [Command] (0x1f delimited, 0x00 terminated list of commands, first byte is camera index)
        /// </summary>
        /// <param name="Data"></param>

        public void ZMQCommands(char[] Data)
        {
            _service.SendBG(13004, Data, _ip);
        }

        /// <summary>
        /// [Command] (0x00 terminated argument passed to v4l2-ctl --set-ctrl, first byte is camera index)
        /// </summary>
        /// <param name="Data"></param>

        public void V4L2SetControls(char[] Data)
        {
            _service.SendBG(13005, Data, _ip);
        }

        public byte[] AvailableCameras { get => _service.UDP._telemetryUInt8[13100]; }
        public byte AvailableCameras_Connected { get => _service.UDP._telemetryUInt8[13100][0]; }
        public byte AvailableCameras_Streaming { get => _service.UDP._telemetryUInt8[13100][1]; }
        /// <summary>
        /// [Connected, Streaming] (bitmask indexes, bitmask indexes)
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(13100, handler); }

        /// <summary>
        /// Picture has been taken.
        /// </summary>
        public void OnPictureTaken(RoveCommCallback<byte> handler) { _service.On(13101, handler); }

        public byte[] Utilization { get => _service.UDP._telemetryUInt8[13102]; }
        public byte Utilization_cpu0 { get => _service.UDP._telemetryUInt8[13102][0]; }
        public byte Utilization_cpu1 { get => _service.UDP._telemetryUInt8[13102][1]; }
        public byte Utilization_cpu2 { get => _service.UDP._telemetryUInt8[13102][2]; }
        public byte Utilization_cpu3 { get => _service.UDP._telemetryUInt8[13102][3]; }
        public byte Utilization_mem { get => _service.UDP._telemetryUInt8[13102][4]; }
        public byte Utilization_storage { get => _service.UDP._telemetryUInt8[13102][5]; }
        /// <summary>
        /// [cpu0, cpu1, cpu2, cpu3, mem, storage] (% usage)
        /// </summary>
        public void OnUtilization(RoveCommCallback<byte> handler) { _service.On(13102, handler); }
    }

    public class CameraServer
    {
        private RoveCommService _service;
        private static string _ip = "192.168.4.102";

        internal CameraServer(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryUInt8[14100] = new byte[1];
            _service.UDP._telemetryUInt8[14101] = new byte[4];
            _service.UDP._telemetryUInt8[14200] = new byte[1];
        }
        /// <summary>
        /// [Camera]
        /// </summary>
        /// <param name="Camera"></param>
        public void TakePhoto(byte Camera)
        {
            _service.SendBG(14000, [Camera], _ip);
        }

        /// <summary>
        /// [Camera, Action] (id, 0: Shutdown 1: Startup 2: Restart)
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Action"></param>
        public void ToggleStream(byte Camera, byte Action)
        {
            _service.SendBG(14001, [Camera, Action], _ip);
        }

        /// <summary>
        /// [Camera, Brightness] (id, 0 - 255)
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Brightness"></param>
        public void AdjustBrightness(byte Camera, byte Brightness)
        {
            _service.SendBG(14002, [Camera, Brightness], _ip);
        }

        /// <summary>
        /// [Camera, Contrast] (id, 0 - 255)
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Contrast"></param>
        public void AdjustContrast(byte Camera, byte Contrast)
        {
            _service.SendBG(14003, [Camera, Contrast], _ip);
        }

        /// <summary>
        /// [Camera, Saturation] (id, 0 - 255)
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Saturation"></param>
        public void AdjustSaturation(byte Camera, byte Saturation)
        {
            _service.SendBG(14004, [Camera, Saturation], _ip);
        }

        /// <summary>
        /// [Camera, Hue] (id, 0 - 255)
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Hue"></param>
        public void AdjustHue(byte Camera, byte Hue)
        {
            _service.SendBG(14005, [Camera, Hue], _ip);
        }

        /// <summary>
        /// [Camera, Temperature]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Temperature"></param>
        public void SetWhiteBalance(byte Camera, byte Temperature)
        {
            _service.SendBG(14008, [Camera, Temperature], _ip);
        }

        /// <summary>
        /// [Camera, BacklightContrast]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="BacklightContrast"></param>
        public void AdjustBacklightContrast(byte Camera, byte BacklightContrast)
        {
            _service.SendBG(14009, [Camera, BacklightContrast], _ip);
        }

        /// <summary>
        /// [Camera, Exposure]
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Exposure"></param>
        public void SetExposure(int Camera, int Exposure)
        {
            _service.SendBG(14010, [Camera, Exposure], _ip);
        }

        public byte AvailableCameras { get => _service.UDP._telemetryUInt8[14100][0]; }
        /// <summary>
        /// [Camera0, Camera1, Camera2, Camera3, Camera4, Camera5, Camera6, Camera7] (bitmask able to stream)
        /// </summary>
        public void OnAvailableCameras(RoveCommCallback<byte> handler) { _service.On(14100, handler); }

        public byte[] StreamingCameras { get => _service.UDP._telemetryUInt8[14101]; }
        public byte StreamingCameras_Port0 { get => _service.UDP._telemetryUInt8[14101][0]; }
        public byte StreamingCameras_Port1 { get => _service.UDP._telemetryUInt8[14101][1]; }
        public byte StreamingCameras_Port2 { get => _service.UDP._telemetryUInt8[14101][2]; }
        public byte StreamingCameras_Port3 { get => _service.UDP._telemetryUInt8[14101][3]; }
        /// <summary>
        /// [Port0, Port1, Port2, Port3] (currently streaming on each port)
        /// </summary>
        public void OnStreamingCameras(RoveCommCallback<byte> handler) { _service.On(14101, handler); }

        /// <summary>
        /// Picture has been taken
        /// </summary>
        public void OnPictureTaken(RoveCommCallback<byte> handler) { _service.On(14102, handler); }

        public byte CameraUnavailable { get => _service.UDP._telemetryUInt8[14200][0]; }
        /// <summary>
        /// [Camera] (id) Camera has errored and stopped streaming
        /// </summary>
        public void OnCameraUnavailable(RoveCommCallback<byte> handler) { _service.On(14200, handler); }
    }

    public class Raman
    {
        private RoveCommService _service;
        private static string _ip = "192.168.3.105";

        internal Raman(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryFloat[16100] = new float[2];
            _service.UDP._telemetryUInt8[16101] = new byte[1];
            _service.UDP._telemetryUInt16[16102] = new ushort[512];
            _service.UDP._telemetryUInt16[16103] = new ushort[512];
            _service.UDP._telemetryUInt16[16104] = new ushort[512];
            _service.UDP._telemetryUInt16[16105] = new ushort[512];
            _service.UDP._telemetryUInt16[16106] = new ushort[512];
            _service.UDP._telemetryUInt16[16107] = new ushort[1];
        }
        /// <summary>
        /// [Speed] (-32768 - 32767) -> (-100% - 100%)
        /// </summary>
        /// <param name="Speed"></param>
        public void InstrumentsAxis(short Speed)
        {
            _service.SendBG(16000, [Speed], _ip);
        }

        /// <summary>
        /// [InstrumentsAxis+, InstrumentsAxis-] (bitmask override enabled)
        /// </summary>
        /// <param name="Data0"></param>
        public void LimitSwitchOverride(byte Data0)
        {
            _service.SendBG(16001, [Data0], _ip);
        }

        /// <summary>
        /// Request calibration of the InstrumentsAxis encoder
        /// </summary>
        public void CalibrateEncoder()
        {
            _service.SendBG<byte>(16002, [], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void WatchdogOverride(byte Enabled)
        {
            _service.SendBG(16003, [Enabled], _ip);
        }

        /// <summary>
        /// [Enabled]
        /// </summary>
        /// <param name="Enabled"></param>
        public void Laser(byte Enabled)
        {
            _service.SendBG(16004, [Enabled], _ip);
        }

        /// <summary>
        /// [Integration Time, Sample Count] (ms, n)
        /// </summary>
        /// <param name="IntegrationTime"></param>
        /// <param name="SampleCount"></param>
        public void RequestRamanReading(uint IntegrationTime, uint SampleCount)
        {
            _service.SendBG(16005, [IntegrationTime, SampleCount], _ip);
        }

        public float[] Position { get => _service.UDP._telemetryFloat[16100]; }
        public float Position_InstrumentsAxis { get => _service.UDP._telemetryFloat[16100][0]; }
        public float Position_TOF { get => _service.UDP._telemetryFloat[16100][1]; }
        /// <summary>
        /// [InstrumentsAxis, TOF] (mm, mm)
        /// </summary>
        public void OnPosition(RoveCommCallback<float> handler) { _service.On(16100, handler); }

        public byte LimitSwitch { get => _service.UDP._telemetryUInt8[16101][0]; }
        /// <summary>
        /// [InstrumentsAxis+, InstrumentsAxis-] (bitmask depressed)
        /// </summary>
        public void OnLimitSwitch(RoveCommCallback<byte> handler) { _service.On(16101, handler); }

        public ushort[] RamanReading_Part1 { get => _service.UDP._telemetryUInt16[16102]; }
        /// <summary>
        /// Raman CCD elements 0-511
        /// </summary>
        public void OnRamanReading_Part1(RoveCommCallback<ushort> handler) { _service.On(16102, handler); }

        public ushort[] RamanReading_Part2 { get => _service.UDP._telemetryUInt16[16103]; }
        /// <summary>
        /// Raman CCD elements 512-1023
        /// </summary>
        public void OnRamanReading_Part2(RoveCommCallback<ushort> handler) { _service.On(16103, handler); }

        public ushort[] RamanReading_Part3 { get => _service.UDP._telemetryUInt16[16104]; }
        /// <summary>
        /// Raman CCD elements 1024-1535
        /// </summary>
        public void OnRamanReading_Part3(RoveCommCallback<ushort> handler) { _service.On(16104, handler); }

        public ushort[] RamanReading_Part4 { get => _service.UDP._telemetryUInt16[16105]; }
        /// <summary>
        /// Raman CCD elements 1536-2047
        /// </summary>
        public void OnRamanReading_Part4(RoveCommCallback<ushort> handler) { _service.On(16105, handler); }

        public ushort[] RamanReading_Part5 { get => _service.UDP._telemetryUInt16[16106]; }
        /// <summary>
        /// Raman CCD elements 2048-2559
        /// </summary>
        public void OnRamanReading_Part5(RoveCommCallback<ushort> handler) { _service.On(16106, handler); }

        public ushort SMOCOPing { get => _service.UDP._telemetryUInt16[16107][0]; }
        /// <summary>
        /// [InstrumentsAxis] (ping time ms)
        /// </summary>
        public void OnSMOCOPing(RoveCommCallback<ushort> handler) { _service.On(16107, handler); }
    }

    public class RoveSoSimulator
    {
        private RoveCommService _service;

        internal RoveSoSimulator(RoveCommService service)
        {
            _service = service;

            _service.UDP._telemetryDouble[99100] = new double[10];
        }
        public double[] IMU { get => _service.UDP._telemetryDouble[99100]; }
        public double IMU_AccelX { get => _service.UDP._telemetryDouble[99100][0]; }
        public double IMU_AccelY { get => _service.UDP._telemetryDouble[99100][1]; }
        public double IMU_AccelZ { get => _service.UDP._telemetryDouble[99100][2]; }
        public double IMU_GyroX { get => _service.UDP._telemetryDouble[99100][3]; }
        public double IMU_GyroY { get => _service.UDP._telemetryDouble[99100][4]; }
        public double IMU_GyroZ { get => _service.UDP._telemetryDouble[99100][5]; }
        public double IMU_QuatX { get => _service.UDP._telemetryDouble[99100][6]; }
        public double IMU_QuatY { get => _service.UDP._telemetryDouble[99100][7]; }
        public double IMU_QuatZ { get => _service.UDP._telemetryDouble[99100][8]; }
        public double IMU_QuatW { get => _service.UDP._telemetryDouble[99100][9]; }
        /// <summary>
        /// [Accel X, Accel Y, Accel Z, Gyro X, Gyro Y, Gyro Z, Quat X, Quat Y, Quat Z, Quat W]
        /// </summary>
        public void OnIMU(RoveCommCallback<double> handler) { _service.On(99100, handler); }
    }
}
