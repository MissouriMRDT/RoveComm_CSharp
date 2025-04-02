cs_type_lookup = {
    "INT8_T"    : "sbyte",
    "UINT8_T"   : "byte",
    "INT16_T"   : "short",
    "UINT16_T"  : "ushort",
    "INT32_T"   : "int",
    "UINT32_T"  : "uint",
    "FLOAT_T"   : "float",
    "DOUBLE_T"  : "double",
    "CHAR"      : "char",
}

def run(manifest, file_path):
    with open(file_path, "w") as file:
        file.write("namespace RoveComm.Boards;\n\n")

        for board, board_desc in manifest["RovecommManifest"].items():
            board_has_content = "Commands" in board_desc and len(board_desc["Commands"].items()) > 0
            if board_has_content:
                file.write(f"""public static class {board}
{{
""")
            
                for command, packet_desc in board_desc["Commands"].items():
                    split_comment = ["".join(p[1:].split("(")[0].split(" ")) for p in packet_desc["comments"].split("]")[0].split(",")]
                    use_param_names = packet_desc["comments"].count("]") and packet_desc["dataCount"] == len(split_comment) and not "-" in "".join(split_comment) and not "." in "".join(split_comment) and len(split_comment) == len(set(split_comment))
                    params = split_comment if use_param_names else ([("arg"+str(i+1)) for i in range(packet_desc["dataCount"])] if packet_desc["dataCount"] < 10 else ["args"])
                    comment = f"""     
    /// <summary>
    /// {packet_desc["comments"]}
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    {"\n\t".join(f"""/// <param name="{params[i]}"></param>""" for i in range(len(params)))}""" 
                    file.write(f"""{comment}\n\tpublic static void {command if command is not board else "Run" + command}(RoveCommService service, {", ".join(f"""{cs_type_lookup[packet_desc["dataType"]]}{"" if packet_desc["dataCount"] < 10 else "[]"} {p}""" for p in params) }) 
    {{
        _ = Task.Run(() => service.SendAsync("Camera1", "SetSource", [{", ".join(p for p in params)}], reliable: false));
    }}
""")
            if "Enums" in board_desc:
                for enum, enum_desc in board_desc["Enums"].items():
                    file.write("\n\tpublic enum " + enum + " {")
                    for ename in enum_desc:
                        file.write("\n\t\t{0} = {1},".format(ename, enum_desc[ename]))
                    file.write("\n\t}")
                file.write("\n")
            if board_has_content: file.write("}\n\n")

def main():
    print("This file should be ran from parser.py!")

if __name__ == "__main__":
    main()