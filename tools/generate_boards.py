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

telemetry_type_lookup = {
    "INT8_T": "Int8",
    "UINT8_T": "UInt8",
    "INT16_T": "Int16",
    "UINT16_T": "UInt16",
    "INT32_T": "Int32",
    "UINT32_T": "UInt32",
    "FLOAT_T": "Float",
    "DOUBLE_T": "Double",
    "CHAR": "Char",
}


def create_identifier(source: str) -> str:
    p = "".join((c for c in source if c.isalnum()))
    if len(p) == 0 or not p[0].isalpha():
        return "_" + p
    return p


def deduplicate_identifiers(source: list[str]) -> list[str]:
    while len(set(source)) != len(source):
        for ident in source:
            duplicates = [i for i, x in enumerate(source) if ident == x]
            if len(duplicates) > 1:
                for i, duplicate in enumerate(duplicates):
                    source[duplicate] += str(i)
    return source


def get_params(packet_desc: dict) -> str:
    if packet_desc["dataCount"] > 12:
        return ["Data"]

    split_comment = [
        "".join(p[1:].split("(")[0].split(" "))
        for p in packet_desc["comments"].split("]")[0].split(",")
    ]

    if "]" not in packet_desc["comments"] or packet_desc["dataCount"] != len(
        split_comment
    ):
        return [("Data" + str(i)) for i in range(packet_desc["dataCount"])]

    return deduplicate_identifiers([create_identifier(p) for p in split_comment])


def run(manifest, file_path):
    with open(file_path, "w", newline="\n") as file:
        boards_with_content = [
            board
            for board, board_desc in manifest["RovecommManifest"].items()
            if (
                ("Commands" in board_desc and len(board_desc["Commands"].items()) > 0)
                or (
                    "Telemetry" in board_desc
                    and len(board_desc["Telemetry"].items()) > 0
                )
                or ("Error" in board_desc and len(board_desc["Error"].items()) > 0)
            )
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
        }}
    }}
}}

namespace RoveComm.Boards
{{"""
        )

        for board, board_desc in manifest["RovecommManifest"].items():
            board_has_content = (
                ("Commands" in board_desc and len(board_desc["Commands"].items()) > 0)
                or (
                    "Telemetry" in board_desc
                    and len(board_desc["Telemetry"].items()) > 0
                )
                or ("Error" in board_desc and len(board_desc["Error"].items()) > 0)
            )
            if board_has_content:
                file.write(
                    f"""
    public class {board}
    {{
        private RoveCommService _service;{f"\n        private static string _ip = \"{board_desc["Ip"]}\";" if "Commands" in board_desc and len(board_desc["Commands"].items()) > 0 else ""}

        internal {board}(RoveCommService service)
        {{
            _service = service;
"""
                )

                if "Error" not in board_desc:
                    board_desc["Error"] = {}
                if "Telemetry" not in board_desc:
                    board_desc["Telemetry"] = {}

                for packet_desc in (
                    board_desc["Telemetry"] | board_desc["Error"]
                ).values():
                    if packet_desc["dataCount"] > 0:
                        file.write(
                            f"\n            _service.UDP._telemetry{telemetry_type_lookup[packet_desc["dataType"]]}[{packet_desc["dataId"]}] = new {cs_type_lookup[packet_desc["dataType"]]}[{packet_desc["dataCount"]}];"
                        )
                file.write("\n        }")

                if "Commands" not in board_desc:
                    board_desc["Commands"] = {}
                for command, packet_desc in board_desc["Commands"].items():
                    params = get_params(packet_desc)
                    cs_type = cs_type_lookup[packet_desc["dataType"]]

                    comment = f"""
        /// <summary>
        /// {packet_desc["comments"]}
        /// </summary>
        {"\n        ".join(f"""/// <param name="{params[i]}"></param>""" for i in range(len(params)))}"""

                    file.write(
                        f"""{comment}
        public void {command if command is not board else "Run" + command}({", ".join(f"""{cs_type}{"" if packet_desc["dataCount"] <= 12 else "[]"} {p}""" for p in params) })
        {{
            _service.SendBG{"" if len(params) > 0 else f"<{cs_type}>"}({packet_desc["dataId"]}, [{", ".join(p for p in params)}], _ip);
        }}
"""
                    )

                for telemetry, packet_desc in (
                    board_desc["Telemetry"] | board_desc["Error"]
                ).items():
                    params = get_params(packet_desc)
                    cs_type = cs_type_lookup[packet_desc["dataType"]]
                    telemetry_type = telemetry_type_lookup[packet_desc["dataType"]]

                    if packet_desc["dataCount"] > 12:
                        file.write(
                            f"\n        public {cs_type}[] {telemetry} {{ get => _service.UDP._telemetry{telemetry_type}[{packet_desc["dataId"]}]; }}"
                        )
                    elif len(params) == 1:
                        file.write(
                            f"\n        public {cs_type} {telemetry} {{ get => _service.UDP._telemetry{telemetry_type}[{packet_desc["dataId"]}][0]; }}"
                        )
                    elif packet_desc["dataCount"] > 0:
                        file.write(
                            f"\n        public {cs_type}[] {telemetry} {{ get => _service.UDP._telemetry{telemetry_type}[{packet_desc["dataId"]}]; }}"
                        )
                        for i, param in enumerate(params):
                            file.write(
                                f"\n        public {cs_type} {telemetry}_{param} {{ get => _service.UDP._telemetry{telemetry_type}[{packet_desc["dataId"]}][{i}]; }}"
                            )
                    file.write(
                        f"""
        /// <summary>
        /// {packet_desc["comments"]}
        /// </summary>
        public void On{telemetry}(RoveCommCallback<{cs_type}> handler) {{ _service.On({packet_desc["dataId"]}, handler); }}
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
