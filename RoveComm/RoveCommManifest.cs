namespace RoveComm;

public static class RoveCommConsts
{
    public static readonly int RoveCommVersion = 3;
    public static readonly int UDPPort = 11000;
    public static readonly int TCPPort = 12000;
    public static readonly int HeaderSize = 6;
    public static readonly int MaxDataSize = 65535 / 3;
    public static readonly int UpdateRate = 100; // milliseconds
}

public enum RoveCommDataType
{
    INT8_T = 0,
    UINT8_T = 1,
    INT16_T = 2,
    UINT16_T = 3,
    INT32_T = 4,
    UINT32_T = 5,
    FLOAT = 6,
    DOUBLE = 7,
    CHAR = 8,
}

public class RoveCommDeviceDesc
{
    public string Ip { get; init; }

    public RoveCommDeviceDesc(string ip)
    {
        Ip = ip;
    }
}

public class RoveCommBoardDesc
{
    public string IP { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Commands { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Telemetry { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Error { get; init; }

    public RoveCommBoardDesc(string ip,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? commands = null,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? telemetry = null,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? error = null)
    {
        IP = ip;
        Commands = commands ?? new Dictionary<string, RoveCommPacketDesc>();
        Telemetry = telemetry ?? new Dictionary<string, RoveCommPacketDesc>();
        Error = error ?? new Dictionary<string, RoveCommPacketDesc>();
    }
}

public class RoveCommPacketDesc
{
    public int DataID { get; init; }
    public int DataCount { get; init; }
    public RoveCommDataType DataType { get; init; }

    public RoveCommPacketDesc(int dataId, int dataCount, RoveCommDataType dataType)
    {
        DataID = dataId;
        DataCount = dataCount;
        DataType = dataType;
    }
}

public static class RoveCommManifest
{
    public static class SystemPackets
    {
        public static readonly int PING = 1;
        public static readonly int PING_REPLY = 2;
        public static readonly int SUBSCRIBE = 3;
        public static readonly int UNSUBSCRIBE = 4;
        public static readonly int INVALID_VERSION = 5;
        public static readonly int NO_DATA = 6;
    }

    public static readonly IReadOnlyDictionary<string, RoveCommDeviceDesc> Devices = new Dictionary<string, RoveCommDeviceDesc>
    {
        ["BasestationSwitch"] = new RoveCommDeviceDesc("192.168.254.2"),
        ["RoverSwitch"] = new RoveCommDeviceDesc("192.168.254.1"),
        ["Rover900MHzRocket"] = new RoveCommDeviceDesc("10.0.0.3"),
        ["Basestation900MHzRocket"] = new RoveCommDeviceDesc("10.0.0.4"),
        ["Rover5GHzRocket"] = new RoveCommDeviceDesc("10.0.0.19"),
        ["Basestation5GHzRocket"] = new RoveCommDeviceDesc("10.0.0.20"),
        ["Rover2_4GHzRocket"] = new RoveCommDeviceDesc("10.0.0.11"),
        ["Basestation2_4GHzRocket"] = new RoveCommDeviceDesc("10.0.0.12")
    };

    public static readonly IReadOnlyDictionary<string, RoveCommBoardDesc> Boards = new Dictionary<string, RoveCommBoardDesc>
    {
        ["Core"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.110",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [LeftSpeed, RightSpeed] (-1 - 1) -> (-100% - 100%)
                ["DriveLeftRight"] = new RoveCommPacketDesc
                (
                    3000,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [LF, LM, LR, RF, RM, RR] (-1 - 1) -> (-100% - 100%)
                ["DriveIndividual"] = new RoveCommPacketDesc
                (
                    3001,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [Enabled]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    3002,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["LeftGimbal"] = new RoveCommPacketDesc
                (
                    3003,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["RightGimbal"] = new RoveCommPacketDesc
                (
                    3004,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["BackGimbal"] = new RoveCommPacketDesc
                (
                    3005,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [R, G, B] (brightness 0 - 255)
                ["LEDRGB"] = new RoveCommPacketDesc
                (
                    3006,
                    3,
                    RoveCommDataType.UINT8_T
                ),
                // [Color] (RGBA)
                ["BackImage"] = new RoveCommPacketDesc
                (
                    3007,
                    256,
                    RoveCommDataType.UINT32_T
                ),
                // [R, G, B] (brightness 0 - 255)
                ["InternalRGB"] = new RoveCommPacketDesc
                (
                    3008,
                    3,
                    RoveCommDataType.UINT8_T
                ),
                // [Color] (RGBA)
                ["InternalImage"] = new RoveCommPacketDesc
                (
                    3009,
                    256,
                    RoveCommDataType.UINT32_T
                ),
                // [State] (DisplayState)
                ["StateDisplay"] = new RoveCommPacketDesc
                (
                    3010,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Brightness] (0 - 255)
                ["Brightness"] = new RoveCommPacketDesc
                (
                    3011,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Mode] (0: Teleop 1: Autonomy)
                ["SetWatchdogMode"] = new RoveCommPacketDesc
                (
                    3012,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Message] (null terminated string)
                ["LEDText"] = new RoveCommPacketDesc
                (
                    3013,
                    256,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [FL, ML, BL, FR, MR, BR] (-1 - 1) -> (-100% - 100%)
                ["MotorSpeeds"] = new RoveCommPacketDesc
                (
                    3100,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [FL, ML, BL, FR, MR, BR] (A)
                ["MotorCurrents"] = new RoveCommPacketDesc
                (
                    3101,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [FL, ML, BL, FR, MR, BR] (A battery side)
                ["VESCCurrents"] = new RoveCommPacketDesc
                (
                    3102,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [Roll, Pitch] (deg)
                ["IMUData"] = new RoveCommPacketDesc
                (
                    3103,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [X, Y, Z] (m/s2)
                ["AccelerometerData"] = new RoveCommPacketDesc
                (
                    3104,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [FanSpeed, BoardTemperature, OtherTemperature] (rpm, C, C)
                ["Thermal"] = new RoveCommPacketDesc
                (
                    3105,
                    3,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // [MotorID, FaultCode]
                ["VESCFault"] = new RoveCommPacketDesc
                (
                    3200,
                    2,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["PMS"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.102",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // Power off all systems except network (PMS will stay on)
                ["EStop"] = new RoveCommPacketDesc
                (
                    4000,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
                ["Suicide"] = new RoveCommPacketDesc
                (
                    4001,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // Cycle all systems including network off and back on (PMS will stay on)
                ["Reboot"] = new RoveCommPacketDesc
                (
                    4002,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmask enable)
                ["EnableBus"] = new RoveCommPacketDesc
                (
                    4003,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmask disable)
                ["DisableBus"] = new RoveCommPacketDesc
                (
                    4004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmask enabled)
                ["SetBus"] = new RoveCommPacketDesc
                (
                    4005,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [PackCurrent, AuxCurrent, LowCurrent, NetworkCurrent, RadioM2Current, RadioM9Current, Cell1Voltage, Cell2Voltage, Cell3Voltage, Cell4Voltage, Cell5Voltage, Cell6Voltage] (A, A, A, A, A, A, V, V, V, V, V, V)
                ["CurrentAndVoltage"] = new RoveCommPacketDesc
                (
                    4100,
                    12,
                    RoveCommDataType.FLOAT
                ),
                // [Motor, Core, Aux, RadioM2, RadioM9, Network] (bitmask enabled)
                ["BusStatus"] = new RoveCommPacketDesc
                (
                    4101,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // Higher current draw than the battery can support. Rover will Reboot automatically
                ["PackOvercurrent"] = new RoveCommPacketDesc
                (
                    4200,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [C1, C2, C3, C4, C5, C6] (bitmask undervolt) Rover will EStop automatically
                ["CellUndervoltage"] = new RoveCommPacketDesc
                (
                    4201,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [C1, C2, C3, C4, C5, C6] (bitmask critical) Rover will Suicide automatically
                ["CellCritical"] = new RoveCommPacketDesc
                (
                    4202,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Aux system current draw too high. Rover will disable Aux bus automatically
                ["AuxOvercurrent"] = new RoveCommPacketDesc
                (
                    4203,
                    0,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["Nav"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.104",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {

            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Lat, Lon, Alt, HorizontalAccuracy, VerticalAccuracy, HeadingAccuracy, FixType, IsDifferential] (deg, deg, m, m, m, deg, ublox_navpvt fix type http://docs.ros.org/en/noetic/api/ublox_msgs/html/msg/NavPVT.html, bool)
                ["GPSLatLonAlt"] = new RoveCommPacketDesc
                (
                    6100,
                    8,
                    RoveCommDataType.DOUBLE
                ),
                // [Heading] (0 - 360)
                ["CompassData"] = new RoveCommPacketDesc
                (
                    6102,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Satellites]
                ["SatelliteCountData"] = new RoveCommPacketDesc
                (
                    6103,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // 
                ["GPSLockError"] = new RoveCommPacketDesc
                (
                    6200,
                    0,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["SignalStack"] = new RoveCommBoardDesc
        (
            ip: "192.168.100.101",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Speed] (-1000 - 1000) -> (-100% - 100%)
                ["OpenLoop"] = new RoveCommPacketDesc
                (
                    7000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [Heading] (0 - 360)
                ["SetAngleTarget"] = new RoveCommPacketDesc
                (
                    7001,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Rover Lat, Rover Lon, Basestation Lat, Basestation Lon] (-90 - 90, -180 - 180, -90 - 90, -180 - 180)
                ["SetGPSTarget"] = new RoveCommPacketDesc
                (
                    7002,
                    4,
                    RoveCommDataType.DOUBLE
                ),
                // [Enabled]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    7003,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Heading] (0 - 360)
                ["CompassAngle"] = new RoveCommPacketDesc
                (
                    7100,
                    1,
                    RoveCommDataType.FLOAT
                )
            }
        ),
        ["Arm"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.107",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [X, J2, J3, J4, J5, J6] (-32768 - 32767) -> (-100% - 100%)
                ["OpenLoop"] = new RoveCommPacketDesc
                (
                    8000,
                    6,
                    RoveCommDataType.INT16_T
                ),
                // [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
                ["TargetAngle"] = new RoveCommPacketDesc
                (
                    8001,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
                ["TargetAngleIncrement"] = new RoveCommPacketDesc
                (
                    8002,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [Gripper] (-32768 - 32767) -> (-100% - 100%)
                ["GripperOpenLoop"] = new RoveCommPacketDesc
                (
                    8003,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [X, Y, Z, J4, J5, J6] (in, in, in, deg, deg, deg)
                ["IKPosition"] = new RoveCommPacketDesc
                (
                    8004,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [X, Y, Z, J4, J5, J6] (in, in, in, deg, deg, deg)
                ["IKWristIncrement"] = new RoveCommPacketDesc
                (
                    8005,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [TX, TY, TZ, RX, RY, RZ] (in, in, in, deg, deg, deg)
                ["IKWorldIncrement"] = new RoveCommPacketDesc
                (
                    8006,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [Enabled]
                ["Laser"] = new RoveCommPacketDesc
                (
                    8007,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Position] (0 - 180)
                ["LinearServo"] = new RoveCommPacketDesc
                (
                    8008,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Position] (0 - 180)
                ["Cache"] = new RoveCommPacketDesc
                (
                    8009,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Enabled]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    8010,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask override enabled)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    8011,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [X, J2, J3, J4, J5, J6] (bitmask override enabled)
                ["ClosedLoopOverride"] = new RoveCommPacketDesc
                (
                    8012,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X, Roll] (bitmask start calibration)
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    8013,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask override enabled)
                ["SoftLimitOverride"] = new RoveCommPacketDesc
                (
                    8014,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["ArmGimbal1"] = new RoveCommPacketDesc
                (
                    8015,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["ArmGimbal2"] = new RoveCommPacketDesc
                (
                    8016,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [TX, TY, TZ, RX, RY, RZ] (in, in, in, deg, deg, deg)
                ["IKToolIncrement"] = new RoveCommPacketDesc
                (
                    8017,
                    6,
                    RoveCommDataType.FLOAT
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [X, J2, J3, J4, J5, J6, GX, GY, GZ] (in, deg, deg, deg, deg, deg, in, in, in)
                ["Position"] = new RoveCommPacketDesc
                (
                    8100,
                    9,
                    RoveCommDataType.FLOAT
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask depressed)
                ["LimitSwitch"] = new RoveCommPacketDesc
                (
                    8101,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, J5+, J5-] (bitmask triggered)
                ["SoftLimit"] = new RoveCommPacketDesc
                (
                    8102,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [X, J2, J3, J4, J5, J6, G] (ping time ms)
                ["SMOCOPing"] = new RoveCommPacketDesc
                (
                    8103,
                    7,
                    RoveCommDataType.UINT16_T
                ),
                // [X, J2, J3, J4, J5, J6] (in, deg, deg, deg, deg, deg)
                ["Target"] = new RoveCommPacketDesc
                (
                    8104,
                    6,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        ),
        ["Auger"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.108",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Speed] (-32768 - 32767) -> (-100% - 100%)
                ["AugerAxis"] = new RoveCommPacketDesc
                (
                    9000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [AugerAxis+, AugerAxis-] (bitmask override enabled)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    9001,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request calibration of the AugerAxis encoder
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    9002,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [Speed] (-1000 - 1000) -> (-100% - 100%)
                ["Auger"] = new RoveCommPacketDesc
                (
                    9003,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [Enabled]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    9004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [White, 365, 405, 500] (brightness 0 - 255)
                ["LED"] = new RoveCommPacketDesc
                (
                    9005,
                    4,
                    RoveCommDataType.UINT8_T
                ),
                // [AFFilters, SoilTrapdoor] (0 - 180)
                ["AugerServo"] = new RoveCommPacketDesc
                (
                    9006,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt] (0 - 180)
                ["AugerGimbal"] = new RoveCommPacketDesc
                (
                    9007,
                    2,
                    RoveCommDataType.INT16_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [AugerAxis] (in)
                ["Position"] = new RoveCommPacketDesc
                (
                    9100,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [AugerSpeed] (rpm)
                ["AugerSpeed"] = new RoveCommPacketDesc
                (
                    9101,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [AugerAxis+, AugerAxis-] (bitmask depressed)
                ["LimitSwitch"] = new RoveCommPacketDesc
                (
                    9102,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Temperature, Humidity] (C, relative %)
                ["Environmental"] = new RoveCommPacketDesc
                (
                    9103,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [AugerCurrent] (A)
                ["AugerCurrent"] = new RoveCommPacketDesc
                (
                    9104,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [AugerAxis] (ping time ms)
                ["SMOCOPing"] = new RoveCommPacketDesc
                (
                    9105,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [LEDTimer] (ms)
                ["LEDStatus"] = new RoveCommPacketDesc
                (
                    9106,
                    1,
                    RoveCommDataType.INT32_T
                )
            }
        ),
        ["Autonomy"] = new RoveCommBoardDesc
        (
            ip: "192.168.3.100",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // Start Autonomy_Software
                ["StartAutonomy"] = new RoveCommPacketDesc
                (
                    11000,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Return Autonomy_Software to Idle state
                ["DisableAutonomy"] = new RoveCommPacketDesc
                (
                    11001,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Lat, Lon, AUTONOMYWAYPOINTTYPES]
                ["AddPositionLeg"] = new RoveCommPacketDesc
                (
                    11002,
                    3,
                    RoveCommDataType.DOUBLE
                ),
                // [Lat, Lon, AUTONOMYWAYPOINTTYPES, MarkerRadius (meters)]
                ["AddMarkerLeg"] = new RoveCommPacketDesc
                (
                    11003,
                    4,
                    RoveCommDataType.DOUBLE
                ),
                // [Lat, Lon, AUTONOMYWAYPOINTTYPES, ObjectRadius (meters)]
                ["AddObjectLeg"] = new RoveCommPacketDesc
                (
                    11004,
                    4,
                    RoveCommDataType.DOUBLE
                ),
                // Clear queued positions, markers, and objects waypoints.
                ["ClearWaypoints"] = new RoveCommPacketDesc
                (
                    11005,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // A multiplier from 0.0 to 1.0 that will scale the max power effort of Autonomy.
                ["SetMaxSpeed"] = new RoveCommPacketDesc
                (
                    11006,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // A multiplier from 0.0 to 1.0 that will filter points from the traversability map. Higher values will result in more conservative pathing.
                ["SetMinTravScore"] = new RoveCommPacketDesc
                (
                    11007,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // A multiplier from 0.0 to 1.0 that will bias the pathing algorithm towards shorter paths (lower values) or safer paths (higher values).
                ["SetBetaBias"] = new RoveCommPacketDesc
                (
                    11008,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Enum (AUTONOMYLOG), Enum (AUTONOMYLOG), Enum (AUTONOMYLOG)] {Console, File, RoveComm}
                ["SetLoggingLevels"] = new RoveCommPacketDesc
                (
                    11009,
                    3,
                    RoveCommDataType.UINT8_T
                ),
                // [Lat, Lon, ObstacleRadius (meters)]
                ["AddObstacle"] = new RoveCommPacketDesc
                (
                    11010,
                    3,
                    RoveCommDataType.DOUBLE
                ),
                // Clear queued permanent obstacles.
                ["ClearObstacles"] = new RoveCommPacketDesc
                (
                    11011,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [State] (AUTONOMYSTATE)
                ["CurrentState"] = new RoveCommPacketDesc
                (
                    11100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [State] (0: Teleop 1: Autonomy 2: Reached Goal)
                ["StateDisplay"] = new RoveCommPacketDesc
                (
                    11101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Thread, FPS] (AUTONOMYTHREADS, fps)
                ["ThreadFPS"] = new RoveCommPacketDesc
                (
                    11103,
                    2,
                    RoveCommDataType.UINT32_T
                ),
                // [Lat, Lon, Lat, Lon, ...] (deg, deg, deg, deg, ...)
                ["PathWaypoints"] = new RoveCommPacketDesc
                (
                    11104,
                    1000,
                    RoveCommDataType.DOUBLE
                ),
                // [EstimatedTimeToGoal] (s)
                ["TimeRemaining"] = new RoveCommPacketDesc
                (
                    11105,
                    1,
                    RoveCommDataType.DOUBLE
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        ),
        ["Camera1"] = new RoveCommBoardDesc
        (
            ip: "192.168.4.100",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Camera, Restart]
                ["TakePicture"] = new RoveCommPacketDesc
                (
                    12000,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Restart]
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    12001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/ffmpeg_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
                ["SetFFMPEGArguments"] = new RoveCommPacketDesc
                (
                    12002,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/picture_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
                ["SetPictureArguments"] = new RoveCommPacketDesc
                (
                    12003,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Command] (0x1f delimited, 0x00 terminated list of commands, first byte is camera index)
                ["ZMQCommands"] = new RoveCommPacketDesc
                (
                    12004,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Command] (0x00 terminated argument passed to v4l2-ctl --set-ctrl, first byte is camera index)
                ["V4L2SetControls"] = new RoveCommPacketDesc
                (
                    12005,
                    16384,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Connected, Streaming] (bitmask indexes, bitmask indexes)
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    12100,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken.
                ["PictureTaken"] = new RoveCommPacketDesc
                (
                    12101,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [cpu0, cpu1, cpu2, cpu3, mem, storage] (% usage)
                ["Utilization"] = new RoveCommPacketDesc
                (
                    12102,
                    6,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        ),
        ["Camera2"] = new RoveCommBoardDesc
        (
            ip: "192.168.4.101",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Camera, Restart]
                ["TakePicture"] = new RoveCommPacketDesc
                (
                    13000,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Restart]
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    13001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/ffmpeg_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
                ["SetFFMPEGArguments"] = new RoveCommPacketDesc
                (
                    13002,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Arguments] (0x1f delimited, 0x00 terminated list with maximum length of 16383 characters for RPi-Camera/config.toml/picture_arguments, first byte is camera index. See RPI-Camera/config.toml for substitutions)
                ["SetPictureArguments"] = new RoveCommPacketDesc
                (
                    13003,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Command] (0x1f delimited, 0x00 terminated list of commands, first byte is camera index)
                ["ZMQCommands"] = new RoveCommPacketDesc
                (
                    13004,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // [Command] (0x00 terminated argument passed to v4l2-ctl --set-ctrl, first byte is camera index)
                ["V4L2SetControls"] = new RoveCommPacketDesc
                (
                    13005,
                    16384,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Connected, Streaming] (bitmask indexes, bitmask indexes)
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    13100,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken.
                ["PictureTaken"] = new RoveCommPacketDesc
                (
                    13101,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [cpu0, cpu1, cpu2, cpu3, mem, storage] (% usage)
                ["Utilization"] = new RoveCommPacketDesc
                (
                    13102,
                    6,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        ),
        ["CameraServer"] = new RoveCommBoardDesc
        (
            ip: "192.168.4.102",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Camera]
                ["TakePhoto"] = new RoveCommPacketDesc
                (
                    14000,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Action] (id, 0: Shutdown 1: Startup 2: Restart)
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    14001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Brightness] (id, 0 - 255)
                ["AdjustBrightness"] = new RoveCommPacketDesc
                (
                    14002,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Contrast] (id, 0 - 255)
                ["AdjustContrast"] = new RoveCommPacketDesc
                (
                    14003,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Saturation] (id, 0 - 255)
                ["AdjustSaturation"] = new RoveCommPacketDesc
                (
                    14004,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Hue] (id, 0 - 255)
                ["AdjustHue"] = new RoveCommPacketDesc
                (
                    14005,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Temperature]
                ["SetWhiteBalance"] = new RoveCommPacketDesc
                (
                    14008,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, BacklightContrast]
                ["AdjustBacklightContrast"] = new RoveCommPacketDesc
                (
                    14009,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // [Camera, Exposure]
                ["SetExposure"] = new RoveCommPacketDesc
                (
                    14010,
                    2,
                    RoveCommDataType.INT32_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Camera0, Camera1, Camera2, Camera3, Camera4, Camera5, Camera6, Camera7] (bitmask able to stream)
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    14100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Port0, Port1, Port2, Port3] (currently streaming on each port)
                ["StreamingCameras"] = new RoveCommPacketDesc
                (
                    14101,
                    4,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken
                ["PictureTaken"] = new RoveCommPacketDesc
                (
                    14102,
                    0,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Camera] (id) Camera has errored and stopped streaming
                ["CameraUnavailable"] = new RoveCommPacketDesc
                (
                    14200,
                    1,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["Raman"] = new RoveCommBoardDesc
        (
            ip: "192.168.3.105",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Speed] (-32768 - 32767) -> (-100% - 100%)
                ["InstrumentsAxis"] = new RoveCommPacketDesc
                (
                    16000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [InstrumentsAxis+, InstrumentsAxis-] (bitmask override enabled)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    16001,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request calibration of the InstrumentsAxis encoder
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    16002,
                    0,
                    RoveCommDataType.UINT8_T
                ),
                // [Enabled]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    16003,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Enabled]
                ["Laser"] = new RoveCommPacketDesc
                (
                    16004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Integration Time, Sample Count] (ms, n)
                ["RequestRamanReading"] = new RoveCommPacketDesc
                (
                    16005,
                    2,
                    RoveCommDataType.UINT32_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [InstrumentsAxis, TOF] (mm, mm)
                ["Position"] = new RoveCommPacketDesc
                (
                    16100,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [InstrumentsAxis+, InstrumentsAxis-] (bitmask depressed)
                ["LimitSwitch"] = new RoveCommPacketDesc
                (
                    16101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Raman CCD elements 0-511
                ["RamanReading_Part1"] = new RoveCommPacketDesc
                (
                    16102,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 512-1023
                ["RamanReading_Part2"] = new RoveCommPacketDesc
                (
                    16103,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 1024-1535
                ["RamanReading_Part3"] = new RoveCommPacketDesc
                (
                    16104,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 1536-2047
                ["RamanReading_Part4"] = new RoveCommPacketDesc
                (
                    16105,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 2048-2559
                ["RamanReading_Part5"] = new RoveCommPacketDesc
                (
                    16106,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // [InstrumentsAxis] (ping time ms)
                ["SMOCOPing"] = new RoveCommPacketDesc
                (
                    16107,
                    1,
                    RoveCommDataType.UINT16_T
                )
            }
        ),
        ["DroneGPS"] = new RoveCommBoardDesc
        (
            ip: "192.168.100.102",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {

            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Lat, Lon, Alt, HorizontalAccuracy, VerticalAccuracy, HeadingAccuracy, FixType, Heading, Satellites] (deg, deg, m, m, m, deg, Ardupilot GPS fix type https://mavlink.io/en/messages/common.html#GPS_FIX_TYPE, 0 - 360, Satellite number)
                ["DronePose"] = new RoveCommPacketDesc
                (
                    17100,
                    9,
                    RoveCommDataType.DOUBLE
                )
            }
        ),
        ["RoveSoSimulator"] = new RoveCommBoardDesc
        (
            ip: "127.0.0.1",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {

            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Accel X, Accel Y, Accel Z, Gyro X, Gyro Y, Gyro Z, Quat X, Quat Y, Quat Z, Quat W]
                ["IMU"] = new RoveCommPacketDesc
                (
                    99100,
                    10,
                    RoveCommDataType.DOUBLE
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        )
    };
}
