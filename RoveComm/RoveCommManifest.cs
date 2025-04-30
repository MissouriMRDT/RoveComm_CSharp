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
                // [LeftSpeed, RightSpeed] (-1, 1)-> (-100%, 100%)
                ["DriveLeftRight"] = new RoveCommPacketDesc
                (
                    3000,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [LF, LM, LR, RF, RM, RR] (-1, 1)-> (-100%, 100%)
                ["DriveIndividual"] = new RoveCommPacketDesc
                (
                    3001,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [0-override off, 1-override on]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    3002,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Tilt](degrees -180-180)
                ["LeftDriveGimbalIncrement"] = new RoveCommPacketDesc
                (
                    3003,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [Tilt](degrees -180-180)
                ["RightDriveGimbalIncrement"] = new RoveCommPacketDesc
                (
                    3004,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt](degrees -180-180)
                ["LeftMainGimbalIncrement"] = new RoveCommPacketDesc
                (
                    3005,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Pan, Tilt](degrees -180-180)
                ["RightMainGimbalIncrement"] = new RoveCommPacketDesc
                (
                    3006,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [Tilt](degrees -180-180)
                ["BackDriveGimbalIncrement"] = new RoveCommPacketDesc
                (
                    3007,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [R, G, B] (0, 255)
                ["LEDRGB"] = new RoveCommPacketDesc
                (
                    3008,
                    3,
                    RoveCommDataType.UINT8_T
                ),
                // [Pattern] (Enum)
                ["LEDPatterns"] = new RoveCommPacketDesc
                (
                    3009,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Teleop, Autonomy, Reached Goal] (enum)
                ["StateDisplay"] = new RoveCommPacketDesc
                (
                    3010,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Set Brightness (0-255)
                ["Brightness"] = new RoveCommPacketDesc
                (
                    3011,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // 0: Teleop, 1: Autonomy
                ["SetWatchdogMode"] = new RoveCommPacketDesc
                (
                    3012,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Set the message to display on the lighting panel; null terminator ends string early
                ["LEDText"] = new RoveCommPacketDesc
                (
                    3013,
                    256,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [FL, ML, BL, FR, MR, BR] (-1, 1)-> (-100%, 100%)
                ["MotorSpeeds"] = new RoveCommPacketDesc
                (
                    3100,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [FL, ML, BL, FR, MR, BR] Motor current draw
                ["MotorCurrents"] = new RoveCommPacketDesc
                (
                    3101,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [FL, ML, BL, FR, MR, BR] VESC (battery side) current draw
                ["VESCCurrents"] = new RoveCommPacketDesc
                (
                    3102,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [Roll, Pitch, Yaw] degrees
                ["IMUData"] = new RoveCommPacketDesc
                (
                    3103,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [xAxis, yAxis, zAxis] Accel in m/s^2
                ["AccelerometerData"] = new RoveCommPacketDesc
                (
                    3104,
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
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
                ["Suicide"] = new RoveCommPacketDesc
                (
                    4001,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Cycle all systems including network off and back on (PMS will stay on)
                ["Reboot"] = new RoveCommPacketDesc
                (
                    4002,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmasked) [1-Enable, 0-No change]
                ["EnableBus"] = new RoveCommPacketDesc
                (
                    4003,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmasked) [1-Disable, 0-No change]
                ["DisableBus"] = new RoveCommPacketDesc
                (
                    4004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Motor, Core, Aux] (bitmasked) [1-Enable, 0-Disable]
                ["SetBus"] = new RoveCommPacketDesc
                (
                    4005,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // Total current draw from battery
                ["PackCurrent"] = new RoveCommPacketDesc
                (
                    4100,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // Pack voltage
                ["PackVoltage"] = new RoveCommPacketDesc
                (
                    4101,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // C1, C2, C3, C4, C5, C6
                ["CellVoltage"] = new RoveCommPacketDesc
                (
                    4102,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // Current draw by aux systems (before 12V buck)
                ["AuxCurrent"] = new RoveCommPacketDesc
                (
                    4103,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // Current draw from other devices (CS1, CS2, CS3)
                ["MiscCurrent"] = new RoveCommPacketDesc
                (
                    4104,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [Motor, Core, Aux, Network] (bitmasked) [1-Enabled, 0-Disabled]
                ["BusStatus"] = new RoveCommPacketDesc
                (
                    4105,
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
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // (bitmasked) [1-Undervolt, 0-OK]. Rover will EStop automatically
                ["CellUndervoltage"] = new RoveCommPacketDesc
                (
                    4201,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // (bitmasked) [1-Critical, 0-OK]. Rover will Suicide automatically
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
                    1,
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
                // [Lat, Long, Alt] [(-90, 90), (-180, 180)(deg), (0, 1000)]
                ["GPSLatLonAlt"] = new RoveCommPacketDesc
                (
                    6100,
                    3,
                    RoveCommDataType.DOUBLE
                ),
                // [Pitch, Yaw, Roll] [(-90, 90), (0, 360), (-90, 90)] (deg)
                ["IMUData"] = new RoveCommPacketDesc
                (
                    6101,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [Heading] [ 0, 360 ]
                ["CompassData"] = new RoveCommPacketDesc
                (
                    6102,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Number of satellites]
                ["SatelliteCountData"] = new RoveCommPacketDesc
                (
                    6103,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [xAxis, yAxis, zAxis] Accel in m/s^2
                ["AccelerometerData"] = new RoveCommPacketDesc
                (
                    6104,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [horizontal_accur, vertical_accur, heading_accur, fix_type, is_differentia] [meters, meters, degrees, ublox_navpvt fix type (http://docs.ros.org/en/noetic/api/ublox_msgs/html/msg/NavPVT.html), boolean]
                ["AccuracyData"] = new RoveCommPacketDesc
                (
                    6105,
                    5,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // 
                ["GPSLockError"] = new RoveCommPacketDesc
                (
                    6200,
                    1,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["BaseStationNav"] = new RoveCommBoardDesc
        (
            ip: "192.168.100.112"
        ),
        ["SignalStack"] = new RoveCommBoardDesc
        (
            ip: "192.168.100.101",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // Motor decipercent [-1000, 1000]
                ["OpenLoop"] = new RoveCommPacketDesc
                (
                    7000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [Heading] [0, 360)
                ["SetAngleTarget"] = new RoveCommPacketDesc
                (
                    7001,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Rover Lat, Rover Long, Basestation Lat, Basestation Long] [Lat:(-90, 90), Long:(-180, 180)] (deg)
                ["SetGPSTarget"] = new RoveCommPacketDesc
                (
                    7002,
                    4,
                    RoveCommDataType.DOUBLE
                ),
                // [0-override off, 1-override on]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    7003,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [Heading] [0, 360)
                ["CompassAngle"] = new RoveCommPacketDesc
                (
                    7100,
                    1,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // (1-Watchdog timeout, 0-OK)
                ["WatchdogStatus"] = new RoveCommPacketDesc
                (
                    7200,
                    1,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["Arm"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.107",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // [X, J2, J3, J4, P, R] Motor decipercent [-1000, 1000]
                ["SetIndividualSpeeds"] = new RoveCommPacketDesc
                (
                    8000,
                    6,
                    RoveCommDataType.INT16_T
                ),
                // [JointID, Decipercent] Motor decipercent [-1000, 1000]
                ["SetJointSpeed"] = new RoveCommPacketDesc
                (
                    8001,
                    2,
                    RoveCommDataType.INT16_T
                ),
                // [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
                ["SetIndividualTargetAngles"] = new RoveCommPacketDesc
                (
                    8002,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [JointID, Position] (in for id 0, deg otherwise)
                ["SetJointTargetAngle"] = new RoveCommPacketDesc
                (
                    8003,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
                ["IncrementIndividualTargetAngles"] = new RoveCommPacketDesc
                (
                    8004,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [JointID, Angle] (in for id 0, deg otherwise)
                ["IncrementJointTargetAngle"] = new RoveCommPacketDesc
                (
                    8005,
                    2,
                    RoveCommDataType.FLOAT
                ),
                // [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
                ["SetIKPosition"] = new RoveCommPacketDesc
                (
                    8006,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
                ["IncrementIKPosition"] = new RoveCommPacketDesc
                (
                    8007,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [J4, P, R] (deg, deg, deg)
                ["SetLockModePosition"] = new RoveCommPacketDesc
                (
                    8008,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [J4, P, R] (deg, deg, deg)
                ["IncrementLockModePosition"] = new RoveCommPacketDesc
                (
                    8009,
                    3,
                    RoveCommDataType.FLOAT
                ),
                // [0-disable, 1-enable]
                ["Laser"] = new RoveCommPacketDesc
                (
                    8010,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [0-retract, 1-extend]
                ["Solenoid"] = new RoveCommPacketDesc
                (
                    8011,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Motor decipercent [-1000, 1000]
                ["SetGripperSpeed"] = new RoveCommPacketDesc
                (
                    8012,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [0-override off, 1-override on] (bitmasked)
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    8013,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P] (0-override off, 1-override on) (bitmasked)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    8014,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // [X, J2, J3, J4, P, R] (0-override off, 1-override on) (bitmasked)
                ["ClosedLoopOverride"] = new RoveCommPacketDesc
                (
                    8015,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X, Roll] (1-calibrate, 0-no action) (bitmasked)
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    8016,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (0-override off, 1-override on) (bitmasked)
                ["SoftLimitOverride"] = new RoveCommPacketDesc
                (
                    8017,
                    1,
                    RoveCommDataType.UINT16_T
                ),
                // Shut off all motors (set decipercents to 0 and disable closed loop)
                ["EStop"] = new RoveCommPacketDesc
                (
                    8018,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [X, J2, J3, J4, P, R, AP] (in, deg, deg, deg, deg, deg, deg, deg)
                ["Positions"] = new RoveCommPacketDesc
                (
                    8100,
                    7,
                    RoveCommDataType.FLOAT
                ),
                // [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
                ["Coordinates"] = new RoveCommPacketDesc
                (
                    8101,
                    6,
                    RoveCommDataType.FLOAT
                ),
                // [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P] (0-off, 1-on) (bitmasked)
                ["LimitSwitchTriggered"] = new RoveCommPacketDesc
                (
                    8102,
                    1,
                    RoveCommDataType.UINT16_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // (1-Watchdog timeout, 0-OK)
                ["WatchdogStatus"] = new RoveCommPacketDesc
                (
                    8200,
                    1,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["Auger"] = new RoveCommBoardDesc
        (
            ip: "192.168.2.108",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // Motor decipercent [-1000, 1000]
                ["AugerAxis_OpenLoop"] = new RoveCommPacketDesc
                (
                    9000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // Absolute position (in)
                ["AugerAxis_SetPosition"] = new RoveCommPacketDesc
                (
                    9001,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // (in)
                ["AugerAxis_IncrementPosition"] = new RoveCommPacketDesc
                (
                    9002,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [AugerAxis+, AugerAxis-] (0-override off, 1-override on) (bitmasked)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    9003,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request calibration of the AugerAxis encoder
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    9004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Motor decipercent [-1000, 1000]
                ["Auger"] = new RoveCommPacketDesc
                (
                    9005,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // [0-override off, 1-override on]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    9006,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request a reading of the temperature at the end of the auger
                ["RequestTemperature"] = new RoveCommPacketDesc
                (
                    9007,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request a reading of the humidity at the end of the auger
                ["RequestHumidity"] = new RoveCommPacketDesc
                (
                    9008,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Ultraviolet LED on AutoFluorescence (0-off, 1-on)
                ["UVLED"] = new RoveCommPacketDesc
                (
                    9009,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Position](degrees -180-180)
                ["AugerMultiplexerServo"] = new RoveCommPacketDesc
                (
                    9010,
                    1,
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
                // (in/s)
                ["AugerSpeed"] = new RoveCommPacketDesc
                (
                    9101,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [AugerAxis+, AugerAxis-] (0-off, 1-on) (bitmasked)
                ["LimitSwitchTriggered"] = new RoveCommPacketDesc
                (
                    9102,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [Temperature] (degrees C)
                ["Temperature"] = new RoveCommPacketDesc
                (
                    9103,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [Humidity] (relative humidity %)
                ["Humidity"] = new RoveCommPacketDesc
                (
                    9104,
                    1,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // (1-Watchdog timeout, 0-OK)
                ["WatchdogStatus"] = new RoveCommPacketDesc
                (
                    9200,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // (1-Stalled, 0-OK)
                ["AugerStalled"] = new RoveCommPacketDesc
                (
                    9201,
                    1,
                    RoveCommDataType.UINT8_T
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
                // [Lat, Lon]
                ["AddPositionLeg"] = new RoveCommPacketDesc
                (
                    11002,
                    2,
                    RoveCommDataType.DOUBLE
                ),
                // [Lat, Lon, MarkerID, MarkerRadius (meters)]
                ["AddMarkerLeg"] = new RoveCommPacketDesc
                (
                    11003,
                    4,
                    RoveCommDataType.DOUBLE
                ),
                // [Lat, Lon, ObjectRadius (meters)]
                ["AddObjectLeg"] = new RoveCommPacketDesc
                (
                    11004,
                    3,
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
                // [Enum (AUTONOMYLOG), Enum (AUTONOMYLOG), Enum (AUTONOMYLOG)] {Console, File, RoveComm}
                ["SetLoggingLevels"] = new RoveCommPacketDesc
                (
                    11007,
                    3,
                    RoveCommDataType.UINT8_T
                ),
                // [Lat, Lon, ObstacleRadius (meters)]
                ["AddObstacle"] = new RoveCommPacketDesc
                (
                    11008,
                    3,
                    RoveCommDataType.DOUBLE
                ),
                // Clear queued permanent obstacles.
                ["ClearObstacles"] = new RoveCommPacketDesc
                (
                    11009,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // Enum (AUTONOMYSTATE)
                ["CurrentState"] = new RoveCommPacketDesc
                (
                    11100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // 
                ["ReachedGoal"] = new RoveCommPacketDesc
                (
                    11101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // String version of most current error log
                ["CurrentLog"] = new RoveCommPacketDesc
                (
                    11102,
                    255,
                    RoveCommDataType.CHAR
                ),
                // [Thread Enum ID, FPS Value]
                ["ThreadFPS"] = new RoveCommPacketDesc
                (
                    11103,
                    2,
                    RoveCommDataType.UINT32_T
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
                // Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
                ["TakePicture"] = new RoveCommPacketDesc
                (
                    12000,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    12001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/ffmpeg_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $ip: output ip, $port: output port.
                ["SetFFMPEGArguments"] = new RoveCommPacketDesc
                (
                    12002,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/picture_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $output: output file without extension.
                ["SetPictureArguments"] = new RoveCommPacketDesc
                (
                    12003,
                    16384,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // Number of detected cameras.
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    12100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Number of streaming cameras.
                ["StreamingCameras"] = new RoveCommPacketDesc
                (
                    12101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken.
                ["PictureTaken"] = new RoveCommPacketDesc
                (
                    12102,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [cpu0, cpu1, cpu2, cpu3, mem, storage], (% usage)
                ["Utilization"] = new RoveCommPacketDesc
                (
                    12103,
                    4,
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
                // Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
                ["TakePicture"] = new RoveCommPacketDesc
                (
                    13000,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    13001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/ffmpeg_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $ip: output ip, $port: output port.
                ["SetFFMPEGArguments"] = new RoveCommPacketDesc
                (
                    13002,
                    16384,
                    RoveCommDataType.CHAR
                ),
                // 0x1f delimited, 0x04 terminated list with maximum length of 16384 characters for RPi-Camera/config.toml/picture_arguments. Accepts the following substitutions: $index: camera index, $input: input device file, $output: output file without extension.
                ["SetPictureArguments"] = new RoveCommPacketDesc
                (
                    13003,
                    16384,
                    RoveCommDataType.CHAR
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // Number of detected cameras.
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    13100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Number of streaming cameras.
                ["StreamingCameras"] = new RoveCommPacketDesc
                (
                    13101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken.
                ["PictureTaken"] = new RoveCommPacketDesc
                (
                    13102,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [cpu0, cpu1, cpu2, cpu3, mem, storage], (% usage)
                ["Utilization"] = new RoveCommPacketDesc
                (
                    13103,
                    4,
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
                // Take a picture with the current camera. [0] is the camera to take a picture with.
                ["TakePhoto"] = new RoveCommPacketDesc
                (
                    14000,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Stop the current camera stream. [0] is the camera to stop streaming. [1] is the action (0 = Shutdown, 1 = Startup, 2 = Restart).
                ["ToggleStream"] = new RoveCommPacketDesc
                (
                    14001,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Adjust brightness level (0-255). [0] is the camera ID, [1] is the brightness level.
                ["AdjustBrightness"] = new RoveCommPacketDesc
                (
                    14002,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Adjust contrast level (0-255). [0] is the camera ID, [1] is the contrast level.
                ["AdjustContrast"] = new RoveCommPacketDesc
                (
                    14003,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Adjust saturation level (0-255). [0] is the camera ID, [1] is the saturation level.
                ["AdjustSaturation"] = new RoveCommPacketDesc
                (
                    14004,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Adjust hue level (0-255). [0] is the camera ID, [1] is the hue level.
                ["AdjustHue"] = new RoveCommPacketDesc
                (
                    14005,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Set white balance temperature. [0] is the camera ID, [1] is the white balance level.
                ["SetWhiteBalance"] = new RoveCommPacketDesc
                (
                    14008,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Adjust backlight contrast level (0-255). [0] is the camera ID, [1] is the backlight contrast level.
                ["AdjustBacklightContrast"] = new RoveCommPacketDesc
                (
                    14009,
                    2,
                    RoveCommDataType.UINT8_T
                ),
                // Set exposure level. [0] is the camera ID, [1] is the exposure level.
                ["SetExposure"] = new RoveCommPacketDesc
                (
                    14010,
                    2,
                    RoveCommDataType.INT32_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // Bitmask values for which cameras are able to stream. LSB is Camera 0, MSB is Camera 7.
                ["AvailableCameras"] = new RoveCommPacketDesc
                (
                    14100,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Which cameras the system is currently streaming on each port
                ["StreamingCameras"] = new RoveCommPacketDesc
                (
                    14101,
                    4,
                    RoveCommDataType.UINT8_T
                ),
                // Picture has been taken.
                ["PictureTaken1"] = new RoveCommPacketDesc
                (
                    14102,
                    1,
                    RoveCommDataType.UINT8_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // Camera has errored and stopped streaming. [0] is ID of camera as an integer (not bitmask).
                ["CameraUnavailable"] = new RoveCommPacketDesc
                (
                    14200,
                    1,
                    RoveCommDataType.UINT8_T
                )
            }
        ),
        ["IRSpectrometer"] = new RoveCommBoardDesc
        (
            ip: "192.168.3.104",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {

            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {

            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        ),
        ["Raman"] = new RoveCommBoardDesc
        (
            ip: "192.168.3.105",
            commands: new Dictionary<string, RoveCommPacketDesc>
            {
                // Motor decipercent [-1000, 1000]
                ["InstrumentsAxis_OpenLoop"] = new RoveCommPacketDesc
                (
                    16000,
                    1,
                    RoveCommDataType.INT16_T
                ),
                // Absolute position (in)
                ["InstrumentsAxis_SetPosition"] = new RoveCommPacketDesc
                (
                    16001,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // (in)
                ["InstrumentsAxis_IncrementPosition"] = new RoveCommPacketDesc
                (
                    16002,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [InstrumentsAxis+, InstrumentsAxis-] (0-override off, 1-override on) (bitmasked)
                ["LimitSwitchOverride"] = new RoveCommPacketDesc
                (
                    16003,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Request calibration of the InstrumentsAxis encoder
                ["CalibrateEncoder"] = new RoveCommPacketDesc
                (
                    16004,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [0-override off, 1-override on]
                ["WatchdogOverride"] = new RoveCommPacketDesc
                (
                    16005,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // [0-disable, 1-enable]
                ["Laser"] = new RoveCommPacketDesc
                (
                    16006,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Start a Raman reading, with the provided integration time (milliseconds)
                ["RequestRamanReading"] = new RoveCommPacketDesc
                (
                    16007,
                    1,
                    RoveCommDataType.UINT32_T
                ),
                // [Pan, Tilt](degrees -180-180)
                ["RamanGimbalIncrement"] = new RoveCommPacketDesc
                (
                    16008,
                    2,
                    RoveCommDataType.INT16_T
                )
            },
            telemetry: new Dictionary<string, RoveCommPacketDesc>
            {
                // [InstrumentsAxis] (in)
                ["Position"] = new RoveCommPacketDesc
                (
                    16100,
                    1,
                    RoveCommDataType.FLOAT
                ),
                // [InstrumentsAxis+, InstrumentsAxis-] (0-off, 1-on) (bitmasked)
                ["LimitSwitchTriggered"] = new RoveCommPacketDesc
                (
                    16101,
                    1,
                    RoveCommDataType.UINT8_T
                ),
                // Raman CCD elements 1-512
                ["RamanReading_Part1"] = new RoveCommPacketDesc
                (
                    16102,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 513-1024
                ["RamanReading_Part2"] = new RoveCommPacketDesc
                (
                    16103,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 1025-1536
                ["RamanReading_Part3"] = new RoveCommPacketDesc
                (
                    16104,
                    512,
                    RoveCommDataType.UINT16_T
                ),
                // Raman CCD elements 1537-2048
                ["RamanReading_Part4"] = new RoveCommPacketDesc
                (
                    16105,
                    512,
                    RoveCommDataType.UINT16_T
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {
                // (1-Watchdog timeout, 0-OK)
                ["WatchdogStatus"] = new RoveCommPacketDesc
                (
                    16200,
                    1,
                    RoveCommDataType.UINT8_T
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
                // Ultrasonic sensor distance reading in centimeters (cm). Value ranges from 0.00 to 500.00 cm
                ["Ultrasonic1"] = new RoveCommPacketDesc
                (
                    99100,
                    2,
                    RoveCommDataType.FLOAT
                )
            },
            error: new Dictionary<string, RoveCommPacketDesc>
            {

            }
        )
    };
}
