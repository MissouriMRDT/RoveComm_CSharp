import json

rovecomm_version = 3
file_path = "../RoveComm/RoveCommManifest.cs"
json_path = "../data/RoveComm/manifest.json"

data_type_lookup = {
    "INT8_T"    : "RoveCommDataType.INT8_T",
    "UINT8_T"   : "RoveCommDataType.UINT8_T",
    "INT16_T"   : "RoveCommDataType.INT16_T",
    "UINT16_T"  : "RoveCommDataType.UINT16_T",
    "INT32_T"   : "RoveCommDataType.INT32_T",
    "UINT32_T"  : "RoveCommDataType.UINT32_T",
    "FLOAT_T"   : "RoveCommDataType.FLOAT",
    "DOUBLE_T"  : "RoveCommDataType.DOUBLE",
    "CHAR"      : "RoveCommDataType.CHAR",
}

packet_type_lookup = {
    "Commands"  : "commands",
    "Telemetry" : "telemetry",
    "Error"     : "errors",
}

def main() -> None:
    with open(json_path, "r") as file:
        manifest = json.load(file)

    with open(file_path, "w") as file:
        file.write("""\
namespace RoveComm;
""")
        file.write("""
public static class RoveCommConsts
{
    public static readonly int RoveCommVersion = 3;
    public static readonly int UDPPort = 11000;
    public static readonly int TCPPort = 12000;
    public static readonly int HeaderSize = 6;
    public static readonly int MaxDataSize = 65535 / 3;
    public static readonly int UpdateRate = 100; // milliseconds
}
""")

        file.write("""
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
""")
        file.write("""
public class RoveCommDeviceDesc
{
    public string Ip { get; init; }

    public RoveCommDeviceDesc(string ip)
    {
        Ip = ip;
    }
}
""")
        file.write("""
public class RoveCommBoardDesc
{
    public string IP { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Commands { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Telemetry { get; init; }
    public IReadOnlyDictionary<string, RoveCommPacketDesc> Errors { get; init; }

    public RoveCommBoardDesc(string ip,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? commands = null,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? telemetry = null,
                             IReadOnlyDictionary<string, RoveCommPacketDesc>? errors = null)
    {
        IP = ip;
        Commands = commands ?? new Dictionary<string, RoveCommPacketDesc>();
        Telemetry = telemetry ?? new Dictionary<string, RoveCommPacketDesc>();
        Errors = errors ?? new Dictionary<string, RoveCommPacketDesc>();
    }
}
""")
        file.write("""
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
""")
        # :-D
        file.write(f"""
public static class RoveCommManifest
{{
    public static class SystemPackets
    {{
        public static readonly int PING = 1;
        public static readonly int PING_REPLY = 2;
        public static readonly int SUBSCRIBE = 3;
        public static readonly int UNSUBSCRIBE = 4;
        public static readonly int INVALID_VERSION = 5;
        public static readonly int NO_DATA = 6;
    }}

    public static readonly IReadOnlyDictionary<string, RoveCommDeviceDesc> Devices = new Dictionary<string, RoveCommDeviceDesc>
    {{
{",\n".join((f"""\
        ["{device}"] = new RoveCommDeviceDesc("{device_desc["Ip"]}")"""
        for device, device_desc in manifest["NetworkDevices"].items()))}
    }};

    public static readonly IReadOnlyDictionary<string, RoveCommBoardDesc> Boards = new Dictionary<string, RoveCommBoardDesc>
    {{
{",\n".join((f"""\
        ["{board}"] = new RoveCommBoardDesc
        (
            ip: "{board_desc["Ip"]}"\
{"".join((f""",
            {packet_type}: new Dictionary<string, RoveCommPacketDesc>
            {{
{",\n".join((f"""\
                // {packet_desc["comments"]}
                ["{command}"] = new RoveCommPacketDesc
                (
                    {packet_desc["dataId"]},
                    {packet_desc["dataCount"]},
                    {data_type_lookup[packet_desc["dataType"]]}
                )"""
            for command, packet_desc in board_desc[json_type].items()))}
            }}"""
        for json_type, packet_type in packet_type_lookup.items()
        if json_type in board_desc ))}
        )"""
        for board, board_desc in manifest["RovecommManifest"].items()))}
    }};
}}
""")

if __name__ == "__main__":
    main()