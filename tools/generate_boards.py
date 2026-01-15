cs_type_lookup = {
    "INT8_T": "sbyte",
    "UINT8_T": "byte",
    "INT16_T": "short",
    "UINT16_T": "ushort",
    "INT32_T": "int",
    "UINT32_T": "uint",
    "FLOAT_T": "float",
    "DOUBLE_T": "double",
    "CHAR": "char",
}


def get_params(packet_desc: dict) -> str:
    split_comment = [
        "".join(p[1:].split("(")[0].split(" "))
        for p in packet_desc["comments"].split("]")[0].split(",")
    ]

    if "]" not in packet_desc["comments"] or packet_desc["dataCount"] != len(
        split_comment
    ):
        return (
            [("arg" + str(i + 1)) for i in range(packet_desc["dataCount"])]
            if packet_desc["dataCount"] < 10
            else ["args"]
        )
    else:
        params = []
        for p in split_comment:
            p = "".join((c for c in p if c.isalnum()))
            if len(p) == 0 or not p[0].isalpha():
                p = "_" + p
            params.append(p)
        for param in params:
            duplicates = [i for i, x in enumerate(params) if param == x]
            if len(duplicates) > 1:
                for i, duplicate in enumerate(duplicates):
                    params[duplicate] += str(i)
        if len(set(params)) != len(params):
            # Deduplication didn't work
            return (
                [("arg" + str(i + 1)) for i in range(packet_desc["dataCount"])]
                if packet_desc["dataCount"] < 10
                else ["args"]
            )
    return params


def run(manifest, file_path):
    with open(file_path, "w") as file:
        boards_with_content = [
            board
            for board, board_desc in manifest["RovecommManifest"].items()
            if "Commands" in board_desc and len(board_desc["Commands"].items()) > 0
        ]
        file.write(
            f"""namespace RoveComm
{{
    public class _Boards
    {{
{"\n".join([f"        public Boards.{board} {board};" for board in boards_with_content])}

        internal _Boards(RoveCommService service)
        {{
{"\n".join([f"            {board} = new(service);" for board in boards_with_content])}
            Arm = new(service);
        }}
    }}
}}

namespace RoveComm.Boards
{{"""
        )

        for board, board_desc in manifest["RovecommManifest"].items():
            board_has_content = (
                "Commands" in board_desc and len(board_desc["Commands"].items()) > 0
            )
            if board_has_content:
                file.write(
                    f"""
    public class {board}
    {{
        private RoveCommService _service;
        private static string _ip = "{board_desc["Ip"]}";

        internal {board}(RoveCommService service) => _service = service;
"""
                )

                for command, packet_desc in board_desc["Commands"].items():
                    params = get_params(packet_desc)

                    comment = f"""
        /// <summary>
        /// {packet_desc["comments"]}
        /// </summary>
        {"\n        ".join(f"""/// <param name="{params[i]}"></param>""" for i in range(len(params)))}"""

                    file.write(
                        f"""{comment}
        public void {command if command is not board else "Run" + command}({", ".join(f"""{cs_type_lookup[packet_desc["dataType"]]}{"" if packet_desc["dataCount"] < 10 else "[]"} {p}""" for p in params) })
        {{
            _service.Send({packet_desc["dataId"]}, [{", ".join(p for p in params)}], _ip);
        }}
"""
                    )

                if "Error" not in board_desc:
                    board_desc["Error"] = {}

                for telemetry, packet_desc in (
                    board_desc["Telemetry"] | board_desc["Error"]
                ).items():
                    params = get_params(packet_desc)

                    file.write(
                        f"""
        /// <summary>
        /// {packet_desc["comments"]}
        /// </summary>
        public void On{telemetry}(RoveCommCallback<{cs_type_lookup[packet_desc["dataType"]]}> handler) {{ _service.On({packet_desc["dataId"]}, handler); }}
"""
                    )

            if "Enums" in board_desc:
                for enum, enum_desc in board_desc["Enums"].items():
                    file.write("\n        public enum " + enum + "\n        {")
                    for ename in enum_desc:
                        file.write(
                            "\n            {0} = {1},".format(ename, enum_desc[ename])
                        )
                    file.write("\n        }")
                file.write("\n")
            if board_has_content:
                file.write("    }\n")
        file.write("}\n")


def main():
    print("This file should be ran from parser.py!")


if __name__ == "__main__":
    main()
