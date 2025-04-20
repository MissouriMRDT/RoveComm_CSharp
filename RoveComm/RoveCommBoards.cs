namespace RoveComm.Boards;

public static class Core
{
     
    /// <summary>
    /// [LeftSpeed, RightSpeed] (-1, 1)-> (-100%, 100%)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="LeftSpeed"></param>
	/// <param name="RightSpeed"></param>
	public static void DriveLeftRight(RoveCommService service, float LeftSpeed, float RightSpeed) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "DriveLeftRight", [LeftSpeed, RightSpeed], reliable: false));
    }
     
    /// <summary>
    /// [LF, LM, LR, RF, RM, RR] (-1, 1)-> (-100%, 100%)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="LF"></param>
	/// <param name="LM"></param>
	/// <param name="LR"></param>
	/// <param name="RF"></param>
	/// <param name="RM"></param>
	/// <param name="RR"></param>
	public static void DriveIndividual(RoveCommService service, float LF, float LM, float LR, float RF, float RM, float RR) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "DriveIndividual", [LF, LM, LR, RF, RM, RR], reliable: false));
    }
     
    /// <summary>
    /// [0-override off, 1-override on]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void WatchdogOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "WatchdogOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Tilt"></param>
	public static void LeftDriveGimbalIncrement(RoveCommService service, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "LeftDriveGimbalIncrement", [Tilt], reliable: false));
    }
     
    /// <summary>
    /// [Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Tilt"></param>
	public static void RightDriveGimbalIncrement(RoveCommService service, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "RightDriveGimbalIncrement", [Tilt], reliable: false));
    }
     
    /// <summary>
    /// [Pan, Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Pan"></param>
	/// <param name="Tilt"></param>
	public static void LeftMainGimbalIncrement(RoveCommService service, short Pan, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "LeftMainGimbalIncrement", [Pan, Tilt], reliable: false));
    }
     
    /// <summary>
    /// [Pan, Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Pan"></param>
	/// <param name="Tilt"></param>
	public static void RightMainGimbalIncrement(RoveCommService service, short Pan, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "RightMainGimbalIncrement", [Pan, Tilt], reliable: false));
    }
     
    /// <summary>
    /// [Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Tilt"></param>
	public static void BackDriveGimbalIncrement(RoveCommService service, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "BackDriveGimbalIncrement", [Tilt], reliable: false));
    }
     
    /// <summary>
    /// [R, G, B] (0, 255)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="R"></param>
	/// <param name="G"></param>
	/// <param name="B"></param>
	public static void LEDRGB(RoveCommService service, byte R, byte G, byte B) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "LEDRGB", [R, G, B], reliable: false));
    }
     
    /// <summary>
    /// [Pattern] (Enum)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Pattern"></param>
	public static void LEDPatterns(RoveCommService service, byte Pattern) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "LEDPatterns", [Pattern], reliable: false));
    }
     
    /// <summary>
    /// [Teleop, Autonomy, Reached Goal] (enum)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void StateDisplay(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "StateDisplay", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Set Brightness (0-255)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Brightness(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "Brightness", [arg1], reliable: false));
    }
     
    /// <summary>
    /// 0: Teleop, 1: Autonomy
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void SetWatchdogMode(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "SetWatchdogMode", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Set the message to display on the lighting panel; null terminator ends string early
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="args"></param>
	public static void LEDText(RoveCommService service, char[] args) 
    {
        _ = Task.Run(() => service.SendAsync("Core", "LEDText", [args], reliable: false));
    }

	public enum DISPLAYSTATE {
		Teleop = 0,
		Autonomy = 1,
		Reached_Goal = 2,
	}
	public enum PATTERNS {
		MRDT = 0,
		BELGIUM = 1,
		MERICA = 2,
		DIRT = 3,
		DOTA = 4,
		MCD = 5,
		WINDOWS = 6,
	}
}

public static class PMS
{
     
    /// <summary>
    /// Power off all systems except network (PMS will stay on)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void EStop(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "EStop", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Power off all systems including network, cannot recover without physical reboot (PMS will stay on)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Suicide(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "Suicide", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Cycle all systems including network off and back on (PMS will stay on)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Reboot(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "Reboot", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Motor, Core, Aux] (bitmasked) [1-Enable, 0-No change]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void EnableBus(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "EnableBus", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Motor, Core, Aux] (bitmasked) [1-Disable, 0-No change]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void DisableBus(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "DisableBus", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Motor, Core, Aux] (bitmasked) [1-Enable, 0-Disable]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void SetBus(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("PMS", "SetBus", [arg1], reliable: false));
    }
}

public static class SignalStack
{
     
    /// <summary>
    /// Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void OpenLoop(RoveCommService service, short arg1) 
    {
        _ = Task.Run(() => service.SendAsync("SignalStack", "OpenLoop", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Heading] [0, 360)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Heading"></param>
	public static void SetAngleTarget(RoveCommService service, float Heading) 
    {
        _ = Task.Run(() => service.SendAsync("SignalStack", "SetAngleTarget", [Heading], reliable: false));
    }
     
    /// <summary>
    /// [Rover Lat, Rover Long, Basestation Lat, Basestation Long] [Lat:(-90, 90), Long:(-180, 180)] (deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="RoverLat"></param>
	/// <param name="RoverLong"></param>
	/// <param name="BasestationLat"></param>
	/// <param name="BasestationLong"></param>
	public static void SetGPSTarget(RoveCommService service, double RoverLat, double RoverLong, double BasestationLat, double BasestationLong) 
    {
        _ = Task.Run(() => service.SendAsync("SignalStack", "SetGPSTarget", [RoverLat, RoverLong, BasestationLat, BasestationLong], reliable: false));
    }
     
    /// <summary>
    /// [0-override off, 1-override on]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void WatchdogOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("SignalStack", "WatchdogOverride", [arg1], reliable: false));
    }
}

public static class Arm
{
     
    /// <summary>
    /// [X, J2, J3, J4, P, R] Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="X"></param>
	/// <param name="J2"></param>
	/// <param name="J3"></param>
	/// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void SetIndividualSpeeds(RoveCommService service, short X, short J2, short J3, short J4, short P, short R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetIndividualSpeeds", [X, J2, J3, J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [JointID, Decipercent] Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="JointID"></param>
	/// <param name="Decipercent"></param>
	public static void SetJointSpeed(RoveCommService service, short JointID, short Decipercent) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetJointSpeed", [JointID, Decipercent], reliable: false));
    }
     
    /// <summary>
    /// [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="X"></param>
	/// <param name="J2"></param>
	/// <param name="J3"></param>
	/// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void SetIndividualTargetAngles(RoveCommService service, float X, float J2, float J3, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetIndividualTargetAngles", [X, J2, J3, J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [JointID, Position] (in for id 0, deg otherwise)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="JointID"></param>
	/// <param name="Position"></param>
	public static void SetJointTargetAngle(RoveCommService service, float JointID, float Position) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetJointTargetAngle", [JointID, Position], reliable: false));
    }
     
    /// <summary>
    /// [X, J2, J3, J4, P, R] (in, deg, deg, deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="X"></param>
	/// <param name="J2"></param>
	/// <param name="J3"></param>
	/// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void IncrementIndividualTargetAngles(RoveCommService service, float X, float J2, float J3, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "IncrementIndividualTargetAngles", [X, J2, J3, J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [JointID, Angle] (in for id 0, deg otherwise)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="JointID"></param>
	/// <param name="Angle"></param>
	public static void IncrementJointTargetAngle(RoveCommService service, float JointID, float Angle) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "IncrementJointTargetAngle", [JointID, Angle], reliable: false));
    }
     
    /// <summary>
    /// [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="X"></param>
	/// <param name="Y"></param>
	/// <param name="Z"></param>
	/// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void SetIKPosition(RoveCommService service, float X, float Y, float Z, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetIKPosition", [X, Y, Z, J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [X, Y, Z, J4, P, R] (in, in, in, deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="X"></param>
	/// <param name="Y"></param>
	/// <param name="Z"></param>
	/// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void IncrementIKPosition(RoveCommService service, float X, float Y, float Z, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "IncrementIKPosition", [X, Y, Z, J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [J4, P, R] (deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void SetLockModePosition(RoveCommService service, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetLockModePosition", [J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [J4, P, R] (deg, deg, deg)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="J4"></param>
	/// <param name="P"></param>
	/// <param name="R"></param>
	public static void IncrementLockModePosition(RoveCommService service, float J4, float P, float R) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "IncrementLockModePosition", [J4, P, R], reliable: false));
    }
     
    /// <summary>
    /// [0-disable, 1-enable]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Laser(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "Laser", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [0-retract, 1-extend]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Solenoid(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "Solenoid", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void SetGripperSpeed(RoveCommService service, short arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SetGripperSpeed", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [0-override off, 1-override on] (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void WatchdogOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "WatchdogOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P] (0-override off, 1-override on) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void LimitSwitchOverride(RoveCommService service, ushort arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "LimitSwitchOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [X, J2, J3, J4, P, R] (0-override off, 1-override on) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void ClosedLoopOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "ClosedLoopOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [X, Roll] (1-calibrate, 0-no action) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void CalibrateEncoder(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "CalibrateEncoder", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [X+, X-, J2+, J2-, J3+, J3-, J4+, J4-, P+, P-] (0-override off, 1-override on) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void SoftLimitOverride(RoveCommService service, ushort arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "SoftLimitOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Shut off all motors (set decipercents to 0 and disable closed loop)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void EStop(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Arm", "EStop", [arg1], reliable: false));
    }

	public enum Joints {
		X = 0,
		J2 = 1,
		J3 = 2,
		J4 = 3,
		PITCH = 4,
		ROLL = 5,
	}
}

public static class Auger
{
     
    /// <summary>
    /// Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void AugerAxis_OpenLoop(RoveCommService service, short arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "AugerAxis_OpenLoop", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Absolute position (in)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void AugerAxis_SetPosition(RoveCommService service, float arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "AugerAxis_SetPosition", [arg1], reliable: false));
    }
     
    /// <summary>
    /// (in)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void AugerAxis_IncrementPosition(RoveCommService service, float arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "AugerAxis_IncrementPosition", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [AugerAxis+, AugerAxis-] (0-override off, 1-override on) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void LimitSwitchOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "LimitSwitchOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Request calibration of the AugerAxis encoder
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void CalibrateEncoder(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "CalibrateEncoder", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void RunAuger(RoveCommService service, short arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "Auger", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [0-override off, 1-override on]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void WatchdogOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "WatchdogOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Request a reading of the temperature at the end of the auger
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void RequestTemperature(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "RequestTemperature", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Request a reading of the humidity at the end of the auger
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void RequestHumidity(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "RequestHumidity", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Ultraviolet LED on AutoFluorescence (0-off, 1-on)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void UVLED(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "UVLED", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Pan, Tilt](degrees -180-180)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Pan"></param>
	/// <param name="Tilt"></param>
	public static void AugerGimbalIncrement(RoveCommService service, short Pan, short Tilt) 
    {
        _ = Task.Run(() => service.SendAsync("Auger", "AugerGimbalIncrement", [Pan, Tilt], reliable: false));
    }
}

public static class Autonomy
{
     
    /// <summary>
    /// Start Autonomy_Software
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void StartAutonomy(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "StartAutonomy", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Return Autonomy_Software to Idle state
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void DisableAutonomy(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "DisableAutonomy", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Lat, Lon]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Lat"></param>
	/// <param name="Lon"></param>
	public static void AddPositionLeg(RoveCommService service, double Lat, double Lon) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "AddPositionLeg", [Lat, Lon], reliable: false));
    }
     
    /// <summary>
    /// [Lat, Lon, MarkerID, MarkerRadius (meters)]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Lat"></param>
	/// <param name="Lon"></param>
	/// <param name="MarkerID"></param>
	/// <param name="MarkerRadius"></param>
	public static void AddMarkerLeg(RoveCommService service, double Lat, double Lon, double MarkerID, double MarkerRadius) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "AddMarkerLeg", [Lat, Lon, MarkerID, MarkerRadius], reliable: false));
    }
     
    /// <summary>
    /// [Lat, Lon, ObjectRadius (meters)]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Lat"></param>
	/// <param name="Lon"></param>
	/// <param name="ObjectRadius"></param>
	public static void AddObjectLeg(RoveCommService service, double Lat, double Lon, double ObjectRadius) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "AddObjectLeg", [Lat, Lon, ObjectRadius], reliable: false));
    }
     
    /// <summary>
    /// Clear queued positions, markers, and objects waypoints.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void ClearWaypoints(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "ClearWaypoints", [arg1], reliable: false));
    }
     
    /// <summary>
    /// A multiplier from 0.0 to 1.0 that will scale the max power effort of Autonomy
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void SetMaxSpeed(RoveCommService service, float arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "SetMaxSpeed", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [Enum (AUTONOMYLOG), Enum (AUTONOMYLOG), Enum (AUTONOMYLOG)] {Console, File, RoveComm}
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	/// <param name="arg3"></param>
	public static void SetLoggingLevels(RoveCommService service, byte arg1, byte arg2, byte arg3) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "SetLoggingLevels", [arg1, arg2, arg3], reliable: false));
    }
     
    /// <summary>
    /// [Lat, Lon, ObstacleRadius (meters)]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="Lat"></param>
	/// <param name="Lon"></param>
	/// <param name="ObstacleRadius"></param>
	public static void AddObstacle(RoveCommService service, double Lat, double Lon, double ObstacleRadius) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "AddObstacle", [Lat, Lon, ObstacleRadius], reliable: false));
    }
     
    /// <summary>
    /// Clear queued permanent obstacles.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void ClearObstacles(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Autonomy", "ClearObstacles", [arg1], reliable: false));
    }

	public enum AUTONOMYSTATE {
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
	public enum AUTONOMYLOG {
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
	public enum AUTONOMYTHREADS {
		MainProcess = 0,
		MainCam = 1,
		LeftCam = 2,
		RightCam = 3,
		GroundCam = 4,
		MainDetector = 5,
		LeftDetector = 6,
		RightDetector = 7,
		StateMachine = 8,
		RoveCommUDP = 9,
		RoveCommTCP = 10,
	}
}

public static class Camera1
{
     
    /// <summary>
    /// Change which camera a feed is looking at. [0] is the feed, [1] is the camera to view.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void ChangeCameras(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("Camera1", "ChangeCameras", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void TakePicture(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("Camera1", "TakePicture", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void ToggleStream1(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("Camera1", "ToggleStream1", [arg1, arg2], reliable: false));
    }
}

public static class Camera2
{
     
    /// <summary>
    /// Take a picture with the current camera. [0] is the camera to take a picture with. [1] tells the camera whether to restart the stream afterwards.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void TakePicture(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("Camera2", "TakePicture", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is whether to restart the stream.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void ToggleStream2(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("Camera2", "ToggleStream2", [arg1, arg2], reliable: false));
    }
}

public static class CameraServer
{
     
    /// <summary>
    /// Take a picture with the current camera. [0] is the camera to take a picture with.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void TakePhoto(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "TakePhoto", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Stop the current camera stream. [0] is the camera to stop streaming. [1] is the action (0 = Shutdown, 1 = Startup, 2 = Restart).
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void ToggleStream(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "ToggleStream", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Adjust brightness level (0-255). [0] is the camera ID, [1] is the brightness level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void AdjustBrightness(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "AdjustBrightness", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Adjust contrast level (0-255). [0] is the camera ID, [1] is the contrast level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void AdjustContrast(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "AdjustContrast", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Adjust saturation level (0-255). [0] is the camera ID, [1] is the saturation level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void AdjustSaturation(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "AdjustSaturation", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Adjust hue level (0-255). [0] is the camera ID, [1] is the hue level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void AdjustHue(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "AdjustHue", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Set white balance temperature. [0] is the camera ID, [1] is the white balance level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void SetWhiteBalance(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "SetWhiteBalance", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Adjust backlight contrast level (0-255). [0] is the camera ID, [1] is the backlight contrast level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void AdjustBacklightContrast(RoveCommService service, byte arg1, byte arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "AdjustBacklightContrast", [arg1, arg2], reliable: false));
    }
     
    /// <summary>
    /// Set exposure level. [0] is the camera ID, [1] is the exposure level.
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public static void SetExposure(RoveCommService service, int arg1, int arg2) 
    {
        _ = Task.Run(() => service.SendAsync("CameraServer", "SetExposure", [arg1, arg2], reliable: false));
    }
}

public static class Raman
{
     
    /// <summary>
    /// Motor decipercent [-1000, 1000]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void InstrumentsAxis_OpenLoop(RoveCommService service, short arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "InstrumentsAxis_OpenLoop", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Absolute position (in)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void InstrumentsAxis_SetPosition(RoveCommService service, float arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "InstrumentsAxis_SetPosition", [arg1], reliable: false));
    }
     
    /// <summary>
    /// (in)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void InstrumentsAxis_IncrementPosition(RoveCommService service, float arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "InstrumentsAxis_IncrementPosition", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [InstrumentsAxis+, InstrumentsAxis-] (0-override off, 1-override on) (bitmasked)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void LimitSwitchOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "LimitSwitchOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Request calibration of the InstrumentsAxis encoder
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void CalibrateEncoder(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "CalibrateEncoder", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [0-override off, 1-override on]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void WatchdogOverride(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "WatchdogOverride", [arg1], reliable: false));
    }
     
    /// <summary>
    /// [0-disable, 1-enable]
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void Laser(RoveCommService service, byte arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "Laser", [arg1], reliable: false));
    }
     
    /// <summary>
    /// Start a Raman reading, with the provided integration time (milliseconds)
    /// </summary> 
    /// <param name="service">The RoveComm service to use.</param>
    /// <param name="arg1"></param>
	public static void RequestRamanReading(RoveCommService service, uint arg1) 
    {
        _ = Task.Run(() => service.SendAsync("Raman", "RequestRamanReading", [arg1], reliable: false));
    }
}

